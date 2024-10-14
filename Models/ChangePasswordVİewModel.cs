using System.ComponentModel.DataAnnotations;

namespace DiabetWebSite.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Mevcut parola gereklidir.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yeni parola gereklidir.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni parolayı tekrar giriniz.")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
