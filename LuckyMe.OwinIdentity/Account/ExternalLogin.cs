using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace LuckyMe.OwinIdentity.Account
{
    public static class ExternalLogin
    {
        public class Command : IAsyncRequest<Result>
        {
            public string ReturnUrl { get; set; }
        }

        public class Result
        {
            public SignInStatus SignInStatus { get; set; }
            public ExternalLoginInfo LoginInfo { get; set; }
        }

        internal class CommandHandler : IAsyncRequestHandler<Command, Result>
        {
            private readonly IAuthenticationManager _authenticationManager;
            private readonly ApplicationSignInManager _signInManager;

            public CommandHandler(IAuthenticationManager authenticationManager, ApplicationSignInManager signInManager)
            {
                _authenticationManager = authenticationManager;
                _signInManager = signInManager;
            }

            public async Task<Result> Handle(Command message)
            {
                var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
                if (loginInfo == null)
                {
                    return null;
                }
                // Sign in the user with this external login provider if the user already has a login
                var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
                return new Result { LoginInfo = loginInfo, SignInStatus = result };
            }
        }
    }
}
