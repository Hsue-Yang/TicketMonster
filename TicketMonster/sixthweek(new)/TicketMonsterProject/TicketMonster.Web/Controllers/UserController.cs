using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TicketMonster.Web.Services;
using TicketMonster.Web.ViewModels.User;
using TicketMonster.Web.ViewModels.User.DTOs;
using TicketMonster.ApplicationCore.Extensions;
using TicketMonster.ApplicationCore.Interfaces.User;
using EventsViewModel = TicketMonster.Web.ViewModels.User.EventsViewModel;

namespace TicketMonster.Web.Controllers;

[Authorize]
public class UserController : BaseController
{
    private readonly int _user;
    private readonly UserManager _userManager;
    private readonly IUserService _userService;

    public UserController(IUserService userService, UserManager userManager)
    {
        _userService = userService;
        _userManager = userManager;
        _user = _userManager.GetCurrentUserId();
    }

    public async Task<IActionResult> Account()
    {
        var vm = new AccountViewModel
        {
            Customer = await _userService.GetCustomerInfo(_user),
            Order = await _userService.GetNextEventInfo(_user),
            Events = await _userService.GetActiveEvents()
        };

        SetToastrMessage();
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> AccountSettings()
    {
        var info = await _userService.GetCustomerInfo(_user);
        return View(info);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AccountSettings(Customer input, IFormCollection collection)
    {
        try
        {
            if (ModelState.IsValid)
            {
                input.Id = _user;
                (input.FirstName, input.LastName, input.Address) = (input.FirstName.Trim(), input.LastName.Trim(), input.Address.Trim());
                input.Password = input.Password.ToSHA256();
                input.ModifyTime = DateTime.UtcNow;
                await _userService.UpdateCustomerInfo(input);

                TempData["ToastrMessage"] = $"<script>toastr.success('Account settings have been successfully modified')</script>";
                return RedirectToAction(nameof(Account));
            }
            else
            {
                ViewBag.ToastrMessage = $"<script>toastr.error('Please check your input and make necessary corrections...')</script>";
                return View(input);
            }
        }
        catch (Exception) { return NotFound(); }
    }

    [HttpGet]
    [Route("/changePassword")]
    public async Task<IActionResult> RenewPassword()
    {
        var curreentPassword = await _userService.GetCustomerInfo(_user);
        var vm = new RenewPasswordDTO {
            CurrentPassword = curreentPassword.Password,
            NewPassword = string.Empty 
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RenewPassword(RenewPasswordDTO input, IFormCollection collection)
    {
        if(input.NewPassword != input.RetypeNewPassword) { ModelState.AddModelError("RetypeNewPassword", "U must enter the same password twice to confirm it."); return View(input); }
        else if (!ModelState.IsValid) { return View(input); }

        var customer = await _userService.GetCustomerInfo(_user);
        customer.Password = input.NewPassword.ToSHA256();
        customer.ModifyTime = DateTime.UtcNow;
        await _userService.RenewPasssword(customer);

        TempData["ToastrMessage"] = $"<script>toastr.success('Password has been successfully updated')</script>";
        return RedirectToAction(nameof(Account));
    }

    public async Task<IActionResult> Events()
    {
        var vm = new EventsViewModel { Orders = await _userService.GetOrderEventsInfo(_user) };
        return View(vm);
    }

    [Route("/EventDetails/{id}")]
    public async Task<IActionResult> EventDetails(int id)
    {
        var vm = new OrderDetailsViewModel
        {
            Order = await _userService.GetCurrentEvent(id),
            OrderDetails = await _userService.GetCurrentEventDetails(id),
            
        };
        if (_user != vm.Order.CustomerId) { return RedirectToAction("AccessDenied", "Access"); }
        return View(vm);
    }

    [Route("/Receipt/{id}")]
    public async Task<IActionResult> Receipt(int id)
    {
        var vm = new OrderDetailsViewModel
        {
            Customer = await _userService.GetCustomerInfo(_user),
            Order = await _userService.GetCurrentEvent(id),
            OrderDetails = await _userService.GetCurrentEventDetails(id)
        };
        if (_user != vm.Order.CustomerId) { return RedirectToAction("AccessDenied", "Access"); }
        return View(vm);
    }
}