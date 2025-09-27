using System.ComponentModel.DataAnnotations;

namespace TareasMVC.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Error.Required")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Error.Email")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Error.Required")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}