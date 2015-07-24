using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class GetSendCodeHandler : IAsyncRequestHandler<GetSendCode, SendCode>
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;

        public GetSendCodeHandler(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<SendCode> Handle(GetSendCode message)
        {
            var userId = await _signInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                throw new Exception("no user found");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            return new SendCode
                   {
                       Providers = userFactors,
                       RememberMe = message.RememberMe,
                       ReturnUrl = message.ReturnUrl
                   };
        }
    }
}
