using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace DAL.Data;

public abstract class BaseDal
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    protected BaseDal(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    
    protected async Task<string> LoadScriptFile(string fileName)
    {
        var fullName = GetType().FullName;
        if (fullName == null) return fileName;
        
        var fullPath = string.Join(".", fullName.Split(".").Skip(1));
        if (!File.Exists(fullPath))
        {
            return fileName;
        }

        return await File.ReadAllTextAsync(fullPath);
    }

    protected async Task<TResult> FirstOrDefaultAsync<TResult>(string fileName, object queryParams = null, CancellationToken cancellationToken = new CancellationToken())
    {
        using var connection = _dbConnectionFactory.Open();
        var sql = await LoadScriptFile(fileName);
        
        var commandDefinition = new CommandDefinition(
            sql,
            queryParams,
            transaction: null,
            commandTimeout: 20,
            commandType: CommandType.Text,
            cancellationToken: cancellationToken);

        var result = await connection.QueryFirstOrDefaultAsync<TResult>(commandDefinition).ConfigureAwait(false);
        return result;
    }

    protected async Task<IReadOnlyList<TResult>> QueryAsync<TResult>(string fileName, object queryParams = null, CancellationToken cancellationToken = new CancellationToken())
    {
        using var connection = _dbConnectionFactory.Open();
        var sql = await LoadScriptFile(fileName);
        
        var commandDefinition = new CommandDefinition(
            sql,
            queryParams,
            transaction: null,
            commandTimeout: 20,
            commandType: CommandType.Text,
            cancellationToken: cancellationToken);

        var result = await connection.QueryAsync<TResult>(commandDefinition).ConfigureAwait(false);
        return result as IReadOnlyList<TResult> ?? result.ToArray();
    }
}