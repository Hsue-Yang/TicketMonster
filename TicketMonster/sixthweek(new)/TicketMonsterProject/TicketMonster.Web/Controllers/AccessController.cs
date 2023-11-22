using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TicketMonster.Web.Services;
using TicketMonster.Web.ViewModels.User.DTOs;
using TicketMonster.ApplicationCore.Extensions;
using TicketMonster.ApplicationCore.Interfaces.User;
using TicketMonster.ApplicationCore.Interfaces.Access;

namespace TicketMonster.Web.Controllers;

public class AccessController : BaseController
{
    private readonly IAccess _access;
    private readonly IUserService _userService;
    private readonly UserManager _userManager;
    private readonly EmailSender _emailSender;
    private readonly Dictionary<string, string> _userInfoDic = new();

    public AccessController(IAccess access, IUserService userService, UserManager userManager, EmailSender emailSender)
    {
        _access = access;
        _userService = userService;
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        SetToastrMessage();
        if (!_userManager.IsLoggingIn()) { return View(); }
        return LocalRedirect("/");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp(Customer input)
    {
        try
        {
            if (!ModelState.IsValid) return View(input);
            if (_access.IsExistUser(input)) { ModelState.AddModelError("Email", "User with this email already exists."); return View(input); }

            (input.FirstName, input.LastName, input.Address) = (input.FirstName.Trim(), input.LastName.Trim(), input.Address.Trim());
            (input.CreateTime, input.ModifyTime) = (DateTime.Now, DateTime.Now);
            input.Phone = input.Phone.Replace("-", "");
            input.Password = input.Password.ToSHA256();

            string verificationCode = VerificationCodeGenerator.GenerateVerificationCode(6);
            _userInfoDic[input.Email] = verificationCode;
            await _emailSender.SendEmail(input.LastName, input.Email, "Email verification", $"<span>{verificationCode} is your verification code 🙂</span>");

            TempData["SignUpData"] = JsonConvert.SerializeObject(input);
            TempData["VerificationCode"] = verificationCode;
            TempData["ToastrMessage"] = $"<script>toastr.info('Verification email has been sent, <br>plz check {input.Email}')</script>";
            return RedirectToAction("EmailVerification");
        }
        catch { return NotFound(); }
    }

    [HttpGet]
    public IActionResult EmailVerification()
    {
        int allowErrorsCount = 3;
        var signUpDataJson = TempData["SignUpData"] as string;
        if (string.IsNullOrEmpty(signUpDataJson)) { return RedirectToAction("SignUp"); }

        var signUpData = JsonConvert.DeserializeObject<Customer>(signUpDataJson);
        var vm = new EmailVerificationDTO { Email = signUpData.Email, VerificationCode = string.Empty };
        TempData["SignUpData"] = JsonConvert.SerializeObject(signUpData);
        TempData["allowErrorsCount"] = allowErrorsCount;

        SetToastrMessage();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EmailVerification(EmailVerificationDTO input)
    {
        var signUpDataJson = TempData["SignUpData"] as string;
        var verificationCode = TempData["VerificationCode"] as string;
        int allowErrorsCount = TempData.ContainsKey("allowErrorsCount") ? (int)TempData["allowErrorsCount"] : 0;
        if (string.IsNullOrEmpty(signUpDataJson) || string.IsNullOrEmpty(verificationCode) || allowErrorsCount <= 1) 
        { TempData["ToastrMessage"] = $"<script>toastr.error('Verification error,<br> plz try to Sign Up again')</script>"; return RedirectToAction("SignUp"); }

        var signUpData = JsonConvert.DeserializeObject<Customer>(signUpDataJson);
        if (input.VerificationCode.ToUpper() == verificationCode.ToUpper())
        {
            signUpData.IsVerified = true;
            _access.SignUp(signUpData);
            TempData["ToastrMessage"] = $"<script>toastr.success('Congratulations on successful Sign Up,<br> plz Sign In')</script>";
            return RedirectToAction("SignIn");
        }
        else
        {
            allowErrorsCount--;
            TempData["SignUpData"] = JsonConvert.SerializeObject(signUpData);
            TempData["VerificationCode"] = verificationCode;
            TempData["allowErrorsCount"] = allowErrorsCount;
            TempData["ToastrMessage"] = $"<script>toastr.warning('Verification feiled,<br> u can try {allowErrorsCount} more time')</script>";
            ModelState.AddModelError("VerificationCode", "The code you entered is invalid. Plz double-check your entry and try again.");

            SetToastrMessage();
            return View(input);
        }
    }

    [HttpGet]
    public IActionResult SignIn()
    {
        SetToastrMessage();
        if (!_userManager.IsLoggingIn()) { return View(); }
        return LocalRedirect("/");
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(Customer input, [FromForm] string remember)
    {
        bool rememberMe = (remember == "on");
        try
        {
            if (!_access.IsExistUser(input)) { ModelState.AddModelError("Email", "User with this email does not Exist."); return View(input); }
            else if (input.Password.IsNullOrEmpty() || !_access.CanSignIn(input)) { ModelState.AddModelError(string.Empty, "SignIn failed"); return View(); }

            var currentUser = await _access.GetUser(input);
            var claims = new List<Claim> { new Claim(ClaimTypes.PrimarySid, currentUser.Id.ToString()), new Claim(ClaimTypes.Email, currentUser.Email) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = rememberMe };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            TempData["ToastrMessage"] = $"<script>toastr.success('welcome {currentUser.LastName} 🙇')</script>";
            return LocalRedirect("/");
        }
        catch { return NotFound(); }
    }

    [HttpGet]
    public IActionResult ForgotPassword_Email()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword_Email(EmailVerificationDTO input)
    {
        var user = new Customer() { Email = input.Email };
        if (!_access.IsExistUser(user)) { ModelState.AddModelError("Email", "User with this email does not Exist."); return View(input); }

        string verificationCode = VerificationCodeGenerator.GenerateVerificationCode(6);
        _userInfoDic[input.Email] = verificationCode;
        await _emailSender.SendEmail("", input.Email, "Email verification", $"<span>{verificationCode} is your verification code 🙂</span>");

        TempData["ForgotEmail"] = input.Email;
        TempData["VerificationCode"] = verificationCode;
        TempData["ToastrMessage"] = $"<script>toastr.info('Verification email has been sent, <br>plz check <b>{input.Email}')</script>";
        return RedirectToAction("ForgotPassword_Verification");
    }

    [HttpGet]
    public IActionResult ForgotPassword_Verification()
    {
        var forgotEmail = TempData["ForgotEmail"] as string;
        if (string.IsNullOrEmpty(forgotEmail)) { return RedirectToAction("SignIn"); }

        var vm = new EmailVerificationDTO { Email = forgotEmail, VerificationCode = string.Empty };
        TempData["ForgotEmail"] = forgotEmail;
        
        SetToastrMessage();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword_Verification(EmailVerificationDTO input)
    {
        var verificationCode = TempData["VerificationCode"] as string;

        if (input.VerificationCode.ToLower() == verificationCode.ToLower())
        {
            string resetPassword = VerificationCodeGenerator.GenerateVerificationCode(10);
            _userInfoDic[input.Email] = resetPassword;
            await _emailSender.SendEmail("", input.Email, "Reset Password", $"<span>{resetPassword} is your temporary new password, u can change it after Signing In 🙂</span>");

            var customer = new Customer { Email = input.Email };
            var target =  await _access.GetUserByEmail(customer);
            target.Password = resetPassword.ToSHA256() ;
            target.ModifyTime = DateTime.UtcNow;
            await _userService.RenewPasssword(target);

            TempData.Remove("ForgotEmail");
            TempData.Remove("VerificationCode");
            TempData["ToastrMessage"] = $"<script>toastr.success('Congratulations on successful verification.<br>Passsword has been reset, plz check {input.Email}')</script>";
            
            return RedirectToAction("ResetPassword");
        }
        else
        {
            TempData["VerificationCode"] = verificationCode;
            TempData["ToastrMessage"] = $"<script>toastr.warning('Invalid code. Plz check and try again.')</script>";
            ModelState.AddModelError("VerificationCode", "The code you entered is invalid. Plz double-check your entry and try again.");

            SetToastrMessage();
            return View(input);
        }
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        SetToastrMessage();
        return TempData.ContainsKey("ToastrMessage") ? View() : RedirectToAction("SignIn");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        TempData["ToastrMessage"] = $"<script>toastr.info('goodbye~ see u next time 👋')</script>";
        return LocalRedirect("/");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}