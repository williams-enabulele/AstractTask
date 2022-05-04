using Microsoft.AspNetCore.Authorization;

namespace AstractTask.Api.MiddleWares
{
    public static class Policies
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string AdminAndUser = "AdminAndUser";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }

        public static AuthorizationPolicy AdminAndUserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin, User).Build();
        }
    }
}