﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TicketMonster.Admin.Enums;
using TicketMonster.Admin.Models;

namespace TicketMonster.Admin.Filters
{
    public class CustomApiExceptionServiceFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public CustomApiExceptionServiceFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomApiExceptionServiceFilter>();
        }
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "請求 {Path} 時發生例外", context.HttpContext.Request.Path);
            var result = new BaseApiResponse()
            {
                IsSuccess = false,
                Code = ApiStatusEnum.Exception
            };

            context.Result = new OkObjectResult(result);
        }
    }
}
