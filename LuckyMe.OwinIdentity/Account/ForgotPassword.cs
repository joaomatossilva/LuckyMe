using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account
{
    public static class ForgotPassword
    {
        public class Command : IAsyncRequest<bool>
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            public string CallbackUrl { get; set; }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, bool>
        {
            private readonly ApplicationUserManager _userManager;

            public CommandHandler(ApplicationUserManager userManager)
            {
                _userManager = userManager;
            }

            public async Task<bool> Handle(Command message)
            {
                var user = await _userManager.FindByNameAsync(message.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return false;
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callback = message.CallbackUrl.Replace("{UserId}", user.Id).Replace("{Code}", code);
                // await _userManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return true;
            }
        }
    }
}
