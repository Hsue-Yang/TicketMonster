using System.ComponentModel.DataAnnotations;

namespace TicketMonster.ApplicationCore.Entities;

public partial class Customer : BaseEntity
{
    [Required(ErrorMessage = "Please enter a first name, cannot leave blank.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please enter a last name, cannot leave blank.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please enter an email address, cannot leave blank.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please enter a phone number, cannot leave blank.")]
    [RegularExpression(@"^09\d{2}[-\s]?\d{3}[-\s]?\d{3}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Your password must be 8 characters long and include one letter and one number.")]
    [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "Your password must be at least 8 characters long")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?!.*\s).+$", ErrorMessage = "Your password must include one letter and one number.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please write your Birthday.")]
    public DateTime Birthday { get; set; }

    [Required(ErrorMessage = "Please choose a country.")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Please enter an address, cannot leave blank.")]
    public string Address { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public bool IsVerified { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}