using System.ComponentModel.DataAnnotations;

namespace StudentTracking.Shared.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Необходимо заполнить поле для логина")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Необходимо заполнить поле для пароля")]
    public string Password { get; set; }
}