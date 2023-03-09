using System.Data;

namespace TC.DAL.Abstractions.Data;

public interface IDbConnectionFactory
{ 
    IDbConnection Open();
}