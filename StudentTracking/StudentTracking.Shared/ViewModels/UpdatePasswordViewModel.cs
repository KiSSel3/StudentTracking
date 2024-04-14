using System.ComponentModel.DataAnnotations;

namespace StudentTracking.Shared.ViewModels;

public class UpdatePasswordViewModel
{
    [Required(ErrorMessage = "Необходимо заполнить поле для старого пароля")]
    public string CurrentPassword { get; set; }
    
    [MinLength(5, ErrorMessage = "Слишком короткий пароль: минимальная длина 5 символов")]
    [Required(ErrorMessage = "Необходимо заполнить поле для нового пароля")]
    public string NewPassword { get; set; }
}