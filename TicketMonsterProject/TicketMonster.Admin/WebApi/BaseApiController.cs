using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketMonster.Admin.Filters;

namespace TicketMonster.Admin.WebApi
{
    [Route("api/[controller]/[action]")]
    //[ServiceFilter(typeof(CustomApiExceptionServiceFilter))]
    //[ServiceFilter(typeof(AdminAuthorize))]
    //[AllowAnonymous]
    [ApiController]   
    public abstract class BaseApiController : ControllerBase
    {
    }
}
