namespace Entities;

public class User
{
    public string Id { get; set; }
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

    public User(
        string id,
        string userName,
        string phoneNumber,
        bool phoneNumberConfirmed,
        string email,
        bool emailConfirmed,
        string passwordHash,
        string firstName,
        string lastName,
        DateTime createdTime,
        DateTime? deletedTime
    )
    {
        Id = id;
        UserName = userName;
        PhoneNumber = phoneNumber;
        PhoneNumberConfirmed = phoneNumberConfirmed;
        Email = email;
        EmailConfirmed = emailConfirmed;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        CreatedTime = createdTime;
        DeletedTime = deletedTime;
    }
}
