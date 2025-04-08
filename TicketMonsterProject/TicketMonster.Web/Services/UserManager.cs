using System.Security.Claims;

namespace TicketMonster.Web.Services;

public class UserManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManager(IHttpContextAccessor httpContextAccessor) { _httpContextAccessor = httpContextAccessor; }

    public bool IsLoggingIn()
    {
        return GetCurrentUserId() is not 0;
    }

    public int GetCurrentUserId()
    {
        var userClaims = _httpContextAccessor.HttpContext.User.Claims;
        var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
        
        _ = int.TryParse(userIdClaim?.Value, out int userId);
        return userId;
    }
}