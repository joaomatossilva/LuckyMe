using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class ForgotPasswordHandler : IAsyncRequestHandler<ForgotPassword, bool>
    {
        private readonly ApplicationUserManager _userManager;

        public ForgotPasswordHandler(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ForgotPassword message)
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
