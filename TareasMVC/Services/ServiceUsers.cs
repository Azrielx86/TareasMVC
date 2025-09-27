using System.Security.Claims;

namespace TareasMVC.Services;

public class ServiceUsers(IHttpContextAccessor httpContextAccessor) : IServiceUsers
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public string GetUserId()
    {
        if (_httpContext.User.Identity is not { IsAuthenticated: true }) throw new Exception("User is not authenticated");
        var idClaim = _httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        return idClaim is not null ? idClaim.Value : throw new Exception("User is not authenticated");
    }
}