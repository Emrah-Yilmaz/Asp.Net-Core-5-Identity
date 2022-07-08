using System.ComponentModel.DataAnnotations;

namespace Asp.Net_Core_Identity.ViewModel
{
    public class PasswordConfirmViewModel
    {
        [Display(Name = "Şifreniz")]
        [Required(ErrorMessage = "Şifre Alanı Boş Geçilemez.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakterden oluşmalıdır.")]
        public string Password { get; set; }
    }
}
