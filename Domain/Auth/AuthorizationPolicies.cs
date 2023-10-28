namespace Domain.Auth;

using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
            options.AddPolicy("MustBeLoggedIn", a => a.RequireAuthenticatedUser()));
    }
}