using TicketMonster.Admin.Enums;
using TicketMonster.ApplicationCore.Extensions;

namespace TicketMonster.Admin.Models
{
    public class BaseApiResponse
    {
        public BaseApiResponse()
        {

        }

        public BaseApiResponse(object body)
        {
            IsSuccess = true;
            Code = ApiStatusEnum.Success;
            Body = body;
        }
        public bool IsSuccess { get; set; }
        public object Body { get; set; }
        public string Message => EnumHelper<ApiStatusEnum>.GetDisplayValue(Code);
        public ApiStatusEnum Code { get; set; }
    }
}
