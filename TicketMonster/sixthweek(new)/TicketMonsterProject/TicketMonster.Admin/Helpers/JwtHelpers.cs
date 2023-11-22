using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TicketMonster.Admin.Helpers
{

public class JwtHelpers
{
    private readonly IConfiguration _configuration;

    public JwtHelpers(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthResultDto GenerateToken(string userName, string Role, int expireMinute)
    {

        // 取得設定檔內的 Issuer 及 Key
        var issuer = _configuration.GetSection("JwtSettings:Issuer").Value;
        var signKey = _configuration.GetSection("JwtSettings:SignKey").Value;

        // 聲明的設定(Claim)
        var claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        // 自訂(非JWT定義): 設定角色
        claims.Add(new Claim(ClaimTypes.Role, Role));


        // 建立 Identity
        var userClaimsIdentity = new ClaimsIdentity(claims);

        // 產生對稱式加密的 SecurityKey
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));

        // 建立簽章(Signature)  
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // 建立 Token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Subject = userClaimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(expireMinute),
            SigningCredentials = signingCredentials
        };
        // 產生 JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var serializeToken = tokenHandler.WriteToken(securityToken);

        // 回傳 結果

        return new AuthResultDto
        {
            Token = serializeToken,
            ExpireTime = new DateTimeOffset(tokenDescriptor.Expires.Value).ToUnixTimeSeconds()

        };
    }
}

public class AuthResultDto
{
    public string Token { get; set; }
    public long ExpireTime { get; set; }
}
}
