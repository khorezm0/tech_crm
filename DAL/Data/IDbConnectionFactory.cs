using System.Data;

namespace DAL.Data;

public interface IDbConnectionFactory
{ 
    IDbConnection Open();
}