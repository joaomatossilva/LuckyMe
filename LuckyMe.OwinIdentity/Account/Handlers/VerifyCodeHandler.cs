using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity.Owin;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class VerifyCodeHandler : IAsyncRequestHandler<VerifyCode, SignInStatus>
    {
        private readonly ApplicationSignInManager _signInManager;

        public VerifyCodeHandler(ApplicationSignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInStatus> Handle(VerifyCode message)
        {
            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
           return await _signInManager.TwoFactorSignInAsync(message.Provider, message.Code, isPersistent: message.RememberMe, rememberBrowser: message.RememberBrowser);
        }
    }
}
