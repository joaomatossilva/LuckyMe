using System.Threading.Tasks;
using MediatR;
using Microsoft.Owin.Security;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class ExternalLoginHandler : IAsyncRequestHandler<ExternalLogin, ExternalLoginResult>
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ApplicationSignInManager _signInManager;

        public ExternalLoginHandler(IAuthenticationManager authenticationManager, ApplicationSignInManager signInManager)
        {
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
        }

        public async Task<ExternalLoginResult> Handle(ExternalLogin message)
        {
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return null;
            }
            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            return new ExternalLoginResult {LoginInfo = loginInfo, SignInStatus = result};
        }
    }
}
