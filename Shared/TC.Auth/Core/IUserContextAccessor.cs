using TC.Auth.Models.Authentication;

namespace TC.Auth.Core;

public interface IUserContextAccessor
{
    UserInfoContext Info { get; }

    IDisposable SetContext(UserInfoContext infoContext);
}