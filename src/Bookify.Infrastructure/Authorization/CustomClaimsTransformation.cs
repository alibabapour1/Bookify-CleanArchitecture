using System.Security.Claims;
using Bookify.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Bookify.Infrastructure.Authorization;

internal sealed class CustomClaimsTransformation : IClaimsTransformation
{
    private readonly IServiceProvider _serviceProvide;

    public CustomClaimsTransformation(IServiceProvider serviceProvide)
    {
        _serviceProvide = serviceProvide;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(claim => claim.Type == ClaimTypes.Role) &&
            principal.HasClaim(claim => claim.Type == JwtRegisteredClaimNames.Sub))
        {
            return principal;
        }

        using var scope = _serviceProvide.CreateScope();
        var authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();
        var identityId = principal.GetIdentityId();

        var userRoles = await authorizationService.GetUserRolesAsync(identityId);
        var claimIdentity = new ClaimsIdentity();
        claimIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,userRoles.Id.ToString()));

        foreach (var role in userRoles.Roles)
        {
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role,role.Name));
        }

        principal.AddIdentity(claimIdentity);

        return principal;


    }
}