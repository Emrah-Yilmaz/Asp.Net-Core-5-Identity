using System.ComponentModel.DataAnnotations;

namespace Asp.Net_Core_Identity.ViewModel
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Gereklidir.")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Telefon Numarası Gereklidir.")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mail Adresi gereklidir.")]
        [Display(Name = "Mail Adresi")]
        [EmailAddress(ErrorMessage ="Email Adresiniz Doğru Formatta Değil.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Gereklidir.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
