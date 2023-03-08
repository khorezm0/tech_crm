using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TC.AspNetCore.DependencyInjection;

internal class DiInstaller
{
    private bool _isInitialized;

    private readonly IServiceCollection _services;
    private readonly Dictionary<Type, List<Action<IServiceCollection>>> _registerBehaviourActionDictionary;
    private readonly Dictionary<Type, bool> _multipleImplementationsPossibleDictionary;

    public DiInstaller(IServiceCollection services)
    {
        _services = services;
        _registerBehaviourActionDictionary = new Dictionary<Type, List<Action<IServiceCollection>>>();
        _multipleImplementationsPossibleDictionary = new Dictionary<Type, bool>();
    }

    public void Initialize()
    {
        if (_isInitialized)
        {
            return;
        }

        foreach (var action in _registerBehaviourActionDictionary.SelectMany(x => x.Value))
        {
            action(_services);
        }

        _isInitialized = true;
    }

    public void RegisterByDIAttribute(string assemblyPath, string assemblySearchPattern)
    {
        var assemblyByPatternList = GetAssemblyByPattern(assemblyPath, assemblySearchPattern);
        RegisterByDIAttribute(assemblyByPatternList);
    }

    public void RegisterByDIAttribute(params Assembly[] assemblyList)
    {
        var injectInfoList = assemblyList
            .SelectMany(a => a.GetTypes())
            .Where(x => x.IsDefined(typeof(InjectAsSingletonAttribute), false))
            .SelectMany(GetInterfaces);

        foreach (var (service, implementation, lifetime) in injectInfoList)
        {
            RegisterBehaviour(service, container => container.Add(new ServiceDescriptor(service, implementation, lifetime)));
        }
    }

    private static (Type Service, Type Implementation, ServiceLifetime Lifetime)[] GetInterfaces(Type type)
    {
        var result = Attribute.GetCustomAttributes(type, typeof(InjectAsSingletonAttribute), false)
            .Cast<InjectAsSingletonAttribute>()
            .Select(x => (Service: x.AbstractType, Implementation: type, Lifetime: GetLifetime(x)))
            .ToArray();

        if (result.Length == 1 && result[0].Service == null)
        {
            result = result
                .SelectMany(x => type.GetInterfaces()
                    .Where(i => i.Namespace.StartsWith("TC"))
                    .Select(service => (Service: service, x.Implementation, x.Lifetime)))
                .ToArray();
        }

        return result.Where(x => x.Service != null && x.Service.IsAssignableFrom(x.Implementation)).ToArray();
    }

    private static ServiceLifetime GetLifetime(InjectAsSingletonAttribute attr)
    {
        return ServiceLifetime.Singleton;
    }

    private void RegisterBehaviour(Type serviceType, Action<IServiceCollection> registerAction)
    {
        if (!CheckMultipleImplementationsPossible(serviceType))
        {
            UnregisterBehaviour(serviceType);
        }
        if (!_registerBehaviourActionDictionary.ContainsKey(serviceType))
        {
            _registerBehaviourActionDictionary.Add(serviceType, new List<Action<IServiceCollection>>());
        }
        _registerBehaviourActionDictionary[serviceType].Add(registerAction);
    }

    private void UnregisterBehaviour(Type serviceType)
    {
        if (_registerBehaviourActionDictionary.ContainsKey(serviceType))
        {
            _registerBehaviourActionDictionary.Remove(serviceType);
        }
    }

    private bool CheckMultipleImplementationsPossible(Type serviceType)
    {
        if (_multipleImplementationsPossibleDictionary.ContainsKey(serviceType))
        {
            return _multipleImplementationsPossibleDictionary[serviceType];
        }
        
        return false;
    }
    
    
    internal static Assembly[] GetAssemblyByPattern(string appDomainAppPath, string searchPattern)
    {
        var nameList = GetAssemblyNameListByPattern(appDomainAppPath, searchPattern);
        var assemblyList = GetAssemblyByName(nameList);

        return assemblyList;
    }

    internal static Assembly[] GetAssemblyByName(IReadOnlyCollection<string> assemblyNameList)
    {
        return assemblyNameList.Select(Assembly.Load).ToArray();
    }

    internal static string[] GetAssemblyNameListByPattern(string appDomainAppPath, string searchPattern)
    {
        var assembyList = Directory
            .GetFiles(appDomainAppPath, $@"{searchPattern}.dll", SearchOption.TopDirectoryOnly)
            .Select(f =>
                f.Replace(appDomainAppPath, string.Empty)
                    .Replace(".dll", string.Empty)
                    .Replace(Path.DirectorySeparatorChar.ToString(), string.Empty))
            .ToArray();

        return assembyList;
    }
}