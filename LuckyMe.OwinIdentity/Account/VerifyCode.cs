using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNet.Identity.Owin;

namespace LuckyMe.OwinIdentity.Account
{
    public class VerifyCode : IAsyncRequest<SignInStatus>
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}
