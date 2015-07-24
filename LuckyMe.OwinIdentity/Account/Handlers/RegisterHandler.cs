using System.Threading.Tasks;
using LuckyMe.Core.Data;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class RegisterHandler : IAsyncRequestHandler<Register, IdentityResult>
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public RegisterHandler(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Handle(Register message)
        {
            var user = new ApplicationUser { UserName = message.Email, Email = message.Email };
            var result = await _userManager.CreateAsync(user, message.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            }
            return result;
        }
    }
}
