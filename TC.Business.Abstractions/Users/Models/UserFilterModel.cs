namespace TC.Business.Abstractions.Users.Models;

public class UserFilterModel
{
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int[] UserIds { get; set; }
}