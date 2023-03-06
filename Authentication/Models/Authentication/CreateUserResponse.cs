using Business.Abstractions;

namespace Authentication.Models.Authentication;
public class CreateUserResponse : BaseResponse
{
    public User User { get; private set; }

    public CreateUserResponse(bool success, string message, User user) : base(success, message)
    {
        User = user;
    }
}