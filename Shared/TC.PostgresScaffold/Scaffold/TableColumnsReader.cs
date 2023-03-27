using System.Globalization;
using System.Text;
using Dapper;
using Pluralize.NET;
using TC.PostgresScaffold.Scaffold.Models;

namespace TC.PostgresScaffold.Scaffold;

public class TableColumnsReader
{
    private readonly DbConnectionFactory _dbConnectionFactory;
    private readonly IPluralize _pluralization;

    public TableColumnsReader(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _pluralization = new Pluralizer();
    }

    public async Task Read(TableInfoModel table)
    {
        table.FormattedSingleName = SnakeCaseToPascal(table.Name);
        table.FormattedPluralName = _pluralization.Pluralize(table.FormattedSingleName);
        Console.WriteLine($"({table.FormattedSingleName}, {table.FormattedPluralName})");
        
        using var db = _dbConnectionFactory.Create();
        db.Open();
        try
        {
            var sql = $@"SELECT 
                    column_name as Name,
                    (is_nullable::bool) as IsNullable,
                    data_type as DbType
                    FROM information_schema.columns
                    WHERE table_name   = '{table.Name}';";

            var res = await db.QueryAsync<ColumnInfoModel>(sql);
            table.Columns = res.ToArray();

            foreach (var column in table.Columns)
            {
                column.DotNetType = TypeMappings.Map(column.DbType);
                column.FormattedName = SnakeCaseToPascal(column.Name);
                Console.WriteLine("\t"+column);
            }
        }
        finally
        {
            db.Close();
        }
    }

    private static string SnakeCaseToPascal(string str)
    {
        var res = new StringBuilder(str.Length);
        res.Append(str[0].ToString().ToUpper());

        for (var i = 1; i < str.Length; i++)
        {
            if (str[i] == '_')
            {
                var k = i;
                while (str[k] == '_' && k < str.Length) { k++; }
                
                res.Append(str[k].ToString().ToUpper());
                i = k;
                continue;
            }

            res.Append(str[i]);
        }

        return res.ToString();
    }
}