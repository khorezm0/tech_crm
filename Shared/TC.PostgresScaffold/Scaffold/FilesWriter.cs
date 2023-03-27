using System.Text;
using TC.PostgresScaffold.Scaffold.Models;

namespace TC.PostgresScaffold.Scaffold;

public class FilesWriter
{
    private string ReadLocation => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scaffold", "Templates");
    private string WriteLocation => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");

    public async Task Write(TableInfoModel model)
    {
        var mappers = BuildMappers(model.Columns);
        var usings = BuildUsings(model.Columns);
        var properties = BuildProperties(model.Columns);
        var sqlInsertParams = BuildSqlInsertParams(model.Columns);
        var sqlInsertColumns = BuildSqlInsertColumns(model.Columns);
        var sqlUpdateMapping = BuildSqlUpdateMapping(model.Columns);
        var sqlMappings = BuildSqlMappings(model.Columns);

        foreach (var file in FilesList.Data)
        {
            var rawTxt = await File.ReadAllTextAsync(Path.Combine(ReadLocation, file.Key));
            var txt = rawTxt
                    .Replace("{{TableName}}", model.Name)
                    .Replace("{{PluralName}}", model.FormattedPluralName)
                    .Replace("{{SingleName}}", model.FormattedSingleName)
                    .Replace("{{SqlMapping}}", sqlMappings)
                    .Replace("{{SqlInsertColumns}}", sqlInsertColumns)
                    .Replace("{{SqlInsertParams}}", sqlInsertParams)
                    .Replace("{{SqlUpdateMapping}}", sqlUpdateMapping)
                    .Replace("{{Properties}}", properties)
                    .Replace("{{Usings}}", usings)
                    .Replace("{{Mappings}}", mappers);
            
            var fileName = file.Value
                .Replace("{{TableName}}", model.Name)
                .Replace("{{PluralName}}", model.FormattedPluralName)
                .Replace("{{SingleName}}", model.FormattedSingleName);

            var fullPath = Path.Combine(WriteLocation, fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            await File.WriteAllTextAsync(fullPath, txt);
        }
    }
    
    
    /*
    {{TableName}}
    {{PluralName}}
    {{SingleName}}

    {{SqlMapping}}
    {{SqlInsertColumns}}
    {{SqlInsertParams}}
    {{SqlUpdateMapping}}
    {{Properties}}
    {{Usings}}
    {{Mappings}}
     */
    private string BuildMappers(IEnumerable<ColumnInfoModel> cols)
    {
        var str = new StringBuilder();
        foreach (var col in cols)
        {
            str.Append($"\t{col.FormattedName} = model.{col.FormattedName},\n");
        }

        return str.ToString();
    }
    
    private string BuildUsings(IEnumerable<ColumnInfoModel> cols)
    {
        var str = new StringBuilder();
        foreach (var col in cols)
        {
            if(col.DotNetType.Namespace != "System")
                str.Append($"using {col.DotNetType.Namespace};\n");
        }

        return str.ToString();
    }
    
    private string BuildProperties(IEnumerable<ColumnInfoModel> cols)
    {
        var str = new StringBuilder();
        foreach (var col in cols)
        {
            var isNullable = col.IsNullable && col.DotNetType != typeof(string) ? "?" : "";
            str.Append($"\tpublic {col.DotNetType.Name}{isNullable} {col.FormattedName} {{ get; set; }};\n");
        }

        return str.ToString();
    }
    
    private string BuildSqlInsertParams(IEnumerable<ColumnInfoModel> cols)
    {
        return string.Join(", ", cols.Where(i => i.Name != "id")
            .Select(i => ":"+i.FormattedName));
    }

    private string BuildSqlInsertColumns(IEnumerable<ColumnInfoModel> cols)
    {
        return string.Join(", ", cols.Where(i => i.Name != "id")
            .Select(i => i.Name));
    }
    
    private string BuildSqlUpdateMapping(IEnumerable<ColumnInfoModel> cols)
    {
        var str = new StringBuilder();
        foreach (var col in cols.OrderBy(i => !i.IsNullable))
        {
            if(col.Name == "id")
                continue;
            
            if (str.Length > 0)
                str.Append(",\n");
            
            var nullable = "";
            
            if (col.IsNullable && col.DotNetType != typeof(string))
                nullable = $"--{col.FormattedName}--";
            str.Append($"{nullable}\t{col.Name} = :{col.FormattedName}");
        }

        return str.ToString();
    }
    
    private string BuildSqlMappings(IEnumerable<ColumnInfoModel> cols)
    {
        var str = new StringBuilder();
        foreach (var col in cols)
        {
            if (str.Length > 0)
                str.Append(",\n");
            
            str.Append($"\t{col.Name} as {col.FormattedName}");
        }

        return str.ToString();
    }
}