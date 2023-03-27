namespace TC.DAL.Abstractions.Users.Models;

public class UserDbFilterModel
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int[] UserIds { get; set; }
}