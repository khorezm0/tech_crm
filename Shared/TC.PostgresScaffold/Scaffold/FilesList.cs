namespace TC.PostgresScaffold.Scaffold;

public static class FilesList
{
    private const string BlName = "TC.Business";
    private const string BlAbstractName = "TC.Business.Abstractions";
    private const string DalName = "TC.DAL";
    private const string DalAbstractName = "TC.DAL.Abstractions";

    public static readonly Dictionary<string, string> Data = new()
    {
        ["Business/Mapper.cs.txt"] = BlName + "/{{PluralName}}/Mappers/{{SingleName}}Mapper.cs",
        ["Business/Service.cs.txt"] = BlName + "/{{PluralName}}/{{PluralName}}Service.cs",
        ["Business.Abstract/FilterModel.cs.txt"] =
            BlAbstractName + "/{{PluralName}}/Models/{{SingleName}}FilterModel.cs",
        ["Business.Abstract/IService.cs.txt"] = BlAbstractName + "/{{PluralName}}/I{{PluralName}}Service.cs",
        ["Business.Abstract/Model.cs.txt"] = BlAbstractName + "/{{PluralName}}/Models/{{SingleName}}Model.cs",
        ["DAL/Dal.cs.txt"] = DalName + "/{{PluralName}}/{{PluralName}}Dal.cs",
        ["DAL/Scripts.cs.txt"] = DalName + "/{{PluralName}}/Scripts.cs",
        ["DAL/CountByFilter.sql.txt"] = DalName + "/{{PluralName}}/Scripts/CountByFilter.sql",
        ["DAL/Delete.sql.txt"] = DalName + "/{{PluralName}}/Scripts/Delete.sql",
        ["DAL/Insert.sql.txt"] = DalName + "/{{PluralName}}/Scripts/Insert.sql",
        ["DAL/SelectById.sql.txt"] = DalName + "/{{PluralName}}/Scripts/SelectById.sql",
        ["DAL/SelectByFilter.sql.txt"] = DalName + "/{{PluralName}}/Scripts/SelectByFilter.sql",
        ["DAL/Update.sql.txt"] = DalName + "/{{PluralName}}/Scripts/Update.sql",
        ["DAL.Abstract/DbModel.cs.txt"] = DalAbstractName + "/{{PluralName}}/Models/{{SingleName}}DbModel.cs",
        ["DAL.Abstract/FilterModel.cs.txt"] = DalAbstractName + "/{{PluralName}}/Models/{{SingleName}}DbFilterModel.cs",
        ["DAL.Abstract/IDal.cs.txt"] = DalAbstractName + "/{{PluralName}}/I{{PluralName}}Dal.cs"
    };
}