using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity.Owin;

namespace LuckyMe.OwinIdentity.Account
{
    public static class LogIn
    {
        public class Command : IAsyncRequest<SignInStatus>
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

        public class CommandHandler : IAsyncRequestHandler<Command, SignInStatus>
        {
            private readonly ApplicationSignInManager _signInManager;

            public CommandHandler(ApplicationSignInManager signInManager)
            {
                _signInManager = signInManager;
            }

            public async Task<SignInStatus> Handle(Command message)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await _signInManager.PasswordSignInAsync(message.Email, message.Password, message.RememberMe, shouldLockout: false);
                return result;
            }
        }
    }
}
