// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using TC.PostgresScaffold.Configuration;
using TC.PostgresScaffold.Scaffold;
using TC.PostgresScaffold.Scaffold.Models;

Console.WriteLine("Started.");
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: false);

IConfiguration appSettings = builder.Build();
var config = new ConfigReader().Read(appSettings);
var tableReader = new TableColumnsReader(new DbConnectionFactory(config.ConnectionStrings.Default));
var tableWriter = new FilesWriter();

foreach (var t in config.Scaffold.Tables)
{
    Console.WriteLine("Reading: "+t.Name);

    var table = new TableInfoModel
    {
        Name = t.Name
    };
    await tableReader.Read(table);
    Console.WriteLine("Columns: " + table.Columns.Length);

    if (table.Columns.Length > 0)
    {
        await tableWriter.Write(table);
    }
}