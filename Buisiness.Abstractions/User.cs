namespace Business.Abstractions;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? DeletedTime { get; set; }
    
    public Enum[] Roles { get; set; }
}
