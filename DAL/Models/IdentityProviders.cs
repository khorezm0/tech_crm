using Microsoft.AspNetCore.Identity;

namespace DAL.Models;

public class UserClaim : IdentityUserClaim<int> { }

public class UserLogin : IdentityUserLogin<int> { }

public class UserRole : IdentityUserRole<int> { }
