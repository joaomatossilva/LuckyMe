using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNet.Identity.Owin;

namespace LuckyMe.OwinIdentity.Account
{
    public class LogIn : IAsyncRequest<SignInStatus>
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Memorizar-me?")]
        public bool RememberMe { get; set; }
    }
}
