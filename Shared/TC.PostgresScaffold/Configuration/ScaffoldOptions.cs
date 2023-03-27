namespace TC.PostgresScaffold.Configuration;

public class ScaffoldOptions
{
    public TableOptions[] Tables { get; set; }
    
    public class TableOptions
    {
        public string Name { get; set; }
    }
}