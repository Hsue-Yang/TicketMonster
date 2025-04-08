using System.ComponentModel.DataAnnotations;

namespace TicketMonster.Web.ViewModels.User.DTOs;

public class RenewPasswordDTO
{
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }
    [Required(ErrorMessage = "Your password must be 8 characters long and include one letter and one number.")]
    [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "Your password must be at least 8 characters long")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?!.*\s).+$", ErrorMessage = "Your password must include one letter and one number.")]
    public string NewPassword { get; set; }
    [Compare(nameof(NewPassword))]
    public string RetypeNewPassword { get; set; }
}

public class EmailVerificationDTO
{
    [Required(ErrorMessage = "This field is required.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Verification code is required")]
    public string VerificationCode { get; set; }
}
