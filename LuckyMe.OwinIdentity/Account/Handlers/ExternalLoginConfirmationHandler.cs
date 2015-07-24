using System;
using System.Threading.Tasks;
using LuckyMe.Core.Data;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class ExternalLoginConfirmationHandler : IAsyncRequestHandler<ExternalLoginConfirmation, IdentityResult>
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public ExternalLoginConfirmationHandler(
            IAuthenticationManager authenticationManager,
            ApplicationUserManager userManager, 
            ApplicationSignInManager signInManager)
        {
            _authenticationManager = authenticationManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Handle(ExternalLoginConfirmation message)
        {
            // Get the information about the user from the external login provider
            var info = await _authenticationManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                throw new Exception("Unable to read external login information");
            }
            var user = new ApplicationUser { UserName = message.Email, Email = message.Email };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user.Id, info.Login);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
            }
            return result;
        }
    }
}
