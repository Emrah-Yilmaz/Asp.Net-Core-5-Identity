using System.ComponentModel.DataAnnotations;

namespace Asp.Net_Core_Identity.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "Email Adresiniz")]
        [Required(ErrorMessage = "Email Alanı Boş Geçilemez.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
