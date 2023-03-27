namespace TC.PostgresScaffold.Scaffold.Models;

public record TableInfoModel
{
    public string Name { get; set; }
    public string FormattedSingleName { get; set; }
    public string FormattedPluralName { get; set; }
    public ColumnInfoModel[] Columns { get; set; }
}