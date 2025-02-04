using System.ComponentModel.DataAnnotations;

namespace FluentFaster.ViewsModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
