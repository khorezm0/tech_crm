namespace TC.AspNetCore.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class InjectAsSingletonAttribute : Attribute
{
    public readonly Type AbstractType;
    
    public InjectAsSingletonAttribute(Type abstractType)
    {
        AbstractType = abstractType;
    }
    
    public InjectAsSingletonAttribute()
    {
        AbstractType = null;
    }
}