using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity.Owin;

namespace LuckyMe.OwinIdentity.Account
{
    public static class VerifyCode
    {
        public class Query : IAsyncRequest<Command>
        {
            public string Provider { get; set; }
            public string ReturnUrl { get; set; }
            public bool RememberMe { get; set; }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Command>
        {
            private readonly ApplicationSignInManager _signInManager;

            public QueryHandler(ApplicationSignInManager signInManager)
            {
                _signInManager = signInManager;
            }

            public async Task<Command> Handle(Query message)
            {
                // Require that the user has already logged in via username/password or external login
                if (!await _signInManager.HasBeenVerifiedAsync())
                {
                    throw new Exception("Account already verified");
                }
                return new Command { Provider = message.Provider, ReturnUrl = message.ReturnUrl, RememberMe = message.RememberMe };
            }
        }


        public class Command : IAsyncRequest<SignInStatus>
        {
            [Required]
            public string Provider { get; set; }

            [Required]
            [Display(Name = "Code")]
            public string Code { get; set; }

            public string ReturnUrl { get; set; }

            [Display(Name = "Remember this browser?")]
            public bool RememberBrowser { get; set; }

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
                // The following code protects for brute force attacks against the two factor codes. 
                // If a user enters incorrect codes for a specified amount of time then the user account 
                // will be locked out for a specified amount of time. 
                // You can configure the account lockout settings in IdentityConfig
                return await _signInManager.TwoFactorSignInAsync(message.Provider, message.Code, isPersistent: message.RememberMe, rememberBrowser: message.RememberBrowser);
            }
        }
    }
}
