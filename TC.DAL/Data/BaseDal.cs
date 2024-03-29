﻿using System.Data;
using Dapper;
using TC.DAL.Abstractions.Data;

namespace TC.DAL.Data;

public abstract class BaseDal
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    protected BaseDal(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    private async Task<string> LoadScriptFileAsync(string fileName)
    {
        var fullName = GetType().FullName;
        var dirPath = Directory.GetParent(GetType().Assembly.Location)?.FullName;
        if (fullName == null || dirPath == null) return fileName;

        var fullPath = Path.Combine(dirPath, string.Join(".", fullName.Split(".").Skip(2).SkipLast(1)), "Scripts",
            fileName);
        if (!File.Exists(fullPath))
        {
            throw new ArgumentException("File is not exists: " + fullPath);
        }

        return await File.ReadAllTextAsync(fullPath);
    }

    protected async Task<TResult> FirstOrDefaultAsync<TResult>(string fileName, object queryParams = null,
        CancellationToken cancellationToken = new CancellationToken(), bool checkForNullable = false)
    {
        using var connection = _dbConnectionFactory.Open();
        var commandDefinition = await BuildCommandAsync(fileName, queryParams, cancellationToken, checkForNullable);
        var result = await connection.QueryFirstOrDefaultAsync<TResult>(commandDefinition).ConfigureAwait(false);
        return result;
    }

    protected async Task ExecuteAsync(string fileName, object queryParams = null,
        CancellationToken cancellationToken = new CancellationToken(), bool checkForNullable = false)
    {
        using var connection = _dbConnectionFactory.Open();
        var commandDefinition = await BuildCommandAsync(fileName, queryParams, cancellationToken, checkForNullable);
        await connection.ExecuteAsync(commandDefinition).ConfigureAwait(false);
    }

    protected async Task<IReadOnlyList<TResult>> QueryAsync<TResult>(string fileName, object queryParams = null,
        CancellationToken cancellationToken = new CancellationToken(), bool checkForNullable = false)
    {
        using var connection = _dbConnectionFactory.Open();
        var commandDefinition = await BuildCommandAsync(fileName, queryParams, cancellationToken, checkForNullable);
        var result = await connection.QueryAsync<TResult>(commandDefinition).ConfigureAwait(false);
        return result as IReadOnlyList<TResult> ?? result.ToArray();
    }

    private async Task<CommandDefinition> BuildCommandAsync(string fileName, object queryParams,
        CancellationToken cancellationToken, bool checkForNullable)
    {
        var sql = await LoadScriptFileAsync(fileName);

        if (checkForNullable)
            sql = FixNullablesSql(sql, queryParams);

        return new CommandDefinition(
            sql,
            queryParams,
            transaction: null,
            commandTimeout: 20,
            commandType: null,
            cancellationToken: cancellationToken);
    }

    private string FixNullablesSql(string origSql, object queryParams)
    {
        if (queryParams == null)
            return origSql;

        var str = origSql;
        var props = queryParams.GetType().GetProperties();
        foreach (var property in props)
        {
            if ((Nullable.GetUnderlyingType(property.PropertyType) != null || property.PropertyType == typeof(string) ||
                 !property.PropertyType.IsValueType)
                && property.GetValue(queryParams) != null)
            {
                str = str.Replace($"--{property.Name}--", "");
            }
        }

        return str;
    }
}