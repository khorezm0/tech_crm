using Authentication.Models.Authentication;

namespace Authentication.Core;

public interface IUserContextAccessor
{
    UserInfoContext Info { get; }

    IDisposable SetContext(UserInfoContext infoContext);
}