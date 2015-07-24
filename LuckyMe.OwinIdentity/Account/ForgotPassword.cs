using System.ComponentModel.DataAnnotations;
using MediatR;

namespace LuckyMe.OwinIdentity.Account
{
    public class ForgotPassword : IAsyncRequest<bool>
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string CallbackUrl { get; set; }
    }
}
