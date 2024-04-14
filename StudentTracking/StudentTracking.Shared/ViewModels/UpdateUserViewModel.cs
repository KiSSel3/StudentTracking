using System.ComponentModel.DataAnnotations;

namespace StudentTracking.Shared.ViewModels;

public class UpdateUserViewModel
{
    [Required(ErrorMessage = "Необходимо заполнить поле для имя")]
    public string Name { get; set; }
    
    [MinLength(4, ErrorMessage = "Слишком короткий логин: минимальная длина 4 символа")]
    [Required(ErrorMessage = "Необходимо заполнить поле для логина")]
    public string Login { get; set; }
}