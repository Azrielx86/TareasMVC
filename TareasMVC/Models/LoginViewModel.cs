using System.ComponentModel.DataAnnotations;

namespace TareasMVC.Models;

public class LoginViewModel : RegisterViewModel
{
    [Display(Name = "RememberMe")]
    public bool RememberMe { get; set; }
}