
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TicketMonster.Admin.Helpers;
using TicketMonster.Admin.Models;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;

namespace TicketMonster.Admin.WebApi
{
      public class AuthController : BaseApiController
    {
        private readonly JwtHelpers _jwthelpers; 
        readonly IRepository<BlockToken> _blockTokenRepo;

        public AuthController(JwtHelpers jwthelpers, IRepository<BlockToken> blockTokenRepo)
        {
            _jwthelpers = jwthelpers;
            _blockTokenRepo = blockTokenRepo;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequestDto request)
        {

            if (IsValidUser(request))
            {

                var generateTokenResult = _jwthelpers.GenerateToken(request.UserName, "Admin", 30);
                return Ok(new BaseApiResponse(generateTokenResult));
            }
            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserName()
        {
            var userName = User.Identity.Name;
            return Ok(new BaseApiResponse(userName));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new BaseApiResponse(claims));
        }

        //如果是驗證的用戶 會進這邊判斷
        private bool IsValidUser(LoginRequestDto request)
        {
            return request.UserName == "admin" && request.Password == "1234";
        }


        [HttpPost]
        public IActionResult Logout([FromBody] LogoutDTO request)
        {
            _blockTokenRepo.Add(new BlockToken
            {
                Token = request.Token,
                ExpireTime = DateTimeOffset.UtcNow.ToUniversalTime()
            });
            return Ok();
        }


        [HttpGet]
        [Authorize]
        public IActionResult Check()
        {
            return Ok();
        }


    }

    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LogoutDTO
    {
        public string Token { get; set; }
    }
}
