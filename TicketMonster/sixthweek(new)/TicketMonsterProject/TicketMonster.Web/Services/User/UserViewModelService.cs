using TicketMonster.ApplicationCore.Interfaces.User;

namespace TicketMonster.Web.Services.User;

public class UserViewModelService
{
    private readonly IUserService _userService;

    public UserViewModelService(IUserService userService) => _userService = userService;
}
