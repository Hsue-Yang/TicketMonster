﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using static Pipelines.Sockets.Unofficial.Threading.MutexSlim;
using System.Net.Http.Headers;
using TicketMonster.ApplicationCore.Interfaces;
using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.Admin.Filters
{
    public class AdminAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly IRepository<BlockToken> _blockToken;

        public AdminAuthorize(IRepository<BlockToken> blockToken)
        {
            _blockToken = blockToken;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }


            if (context.HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                //登出後Token失效Filter(不能再登入，要取得新的)
                var authorization = context.HttpContext.Request.Headers[HeaderNames.Authorization];
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var scheme = headerValue.Scheme;
                    var parameter = headerValue.Parameter;
                    if (!_blockToken.Any(x => x.Token.ToLower() == parameter.ToLower()))
                    {
                        return;
                    }
                }
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
