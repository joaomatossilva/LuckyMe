using System;
using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class GetCodeVerifiyHandler : IAsyncRequestHandler<GetCodeVerify, VerifyCode>
    {
        private readonly ApplicationSignInManager _signInManager;

        public GetCodeVerifiyHandler(ApplicationSignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<VerifyCode> Handle(GetCodeVerify message)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await _signInManager.HasBeenVerifiedAsync())
            {
                throw new Exception("Account already verified");
            }
            return new VerifyCode { Provider = message.Provider, ReturnUrl = message.ReturnUrl, RememberMe = message.RememberMe };
        }
    }
}
