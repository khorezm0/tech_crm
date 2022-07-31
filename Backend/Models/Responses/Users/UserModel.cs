namespace Backend.Models.Responses.Users;

#nullable disable

public class UserModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedTime { get; set; }

    public static UserModel FromEntity(Entities.User user)
    {
        return new UserModel()
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
