using System.Runtime.Serialization;

namespace TC.PostgresScaffold.Scaffold.Models;

public record ColumnInfoModel
{
    public string Name { get; set; }
    public string FormattedName { get; set; }
    public string DbType { get; set; }
    public Type DotNetType { get; set; }
    public bool IsNullable { get; set; }

    public override string ToString()
    {
        return $"{Name} [{FormattedName}, {DbType}, {DotNetType}, {(IsNullable ? "" : "NOT")} NULL]";
    }
}