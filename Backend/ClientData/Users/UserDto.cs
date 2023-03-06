namespace Backend.ClientData.Users;

#nullable disable

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedTime { get; set; }

    public static UserDto Map(Business.Abstractions.User user)
    {
        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedTime = user.CreatedTime
        };
    }
}
