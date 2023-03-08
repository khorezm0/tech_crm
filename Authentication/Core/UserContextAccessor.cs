using Authentication.Models.Authentication;

namespace Authentication.Core;
public class UserContextAccessor : IUserContextAccessor
{
    private readonly AsyncLocal<UserInfoContextHolder> _current = new();
    public UserInfoContext Info => _current.Value?.InfoContext;
    
    public IDisposable SetContext(UserInfoContext infoContext)
    {
        ClearContext();

        if (infoContext != null)
        {
            _current.Value = new UserInfoContextHolder
            {
                InfoContext = infoContext,
            };
        }

        return new UserContextScope(this);
    }

    private void ClearContext()
    {
        var holder = _current.Value;

        if (holder != null)
        {
            holder.InfoContext = null;
        }
    }
    
    private class UserContextScope : IDisposable
    {
        private readonly UserContextAccessor _accessor;

        public UserContextScope(UserContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void Dispose()
        {
            _accessor.ClearContext();
        }
    }

    private class UserInfoContextHolder
    {
        public UserInfoContext InfoContext;
    }
}