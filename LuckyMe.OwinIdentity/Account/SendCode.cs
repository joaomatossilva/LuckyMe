using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account
{
    public static class SendCode {

        public class Query : IAsyncRequest<Command>
        {
            public string ReturnUrl { get; set; }
            public bool RememberMe { get; set; }
        }

        public class Command : IAsyncRequest<bool>
        {
            public string SelectedProvider { get; set; }
            public IEnumerable<string> Providers { get; set; }
            public string ReturnUrl { get; set; }
            public bool RememberMe { get; set; }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, Command>
        {
            private readonly ApplicationSignInManager _signInManager;
            private readonly ApplicationUserManager _userManager;

            public QueryHandler(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
            {
                _signInManager = signInManager;
                _userManager = userManager;
            }

            public async Task<Command> Handle(Query message)
            {
                var userId = await _signInManager.GetVerifiedUserIdAsync();
                if (userId == null)
                {
                    throw new Exception("no user found");
                }
                var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
                return new Command
                {
                    Providers = userFactors,
                    RememberMe = message.RememberMe,
                    ReturnUrl = message.ReturnUrl
                };
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command, bool>
        {
            private readonly ApplicationSignInManager _signInManager;

            public CommandHandler(ApplicationSignInManager signInManager)
            {
                _signInManager = signInManager;
            }

            public async Task<bool> Handle(Command message)
            {
                return await _signInManager.SendTwoFactorCodeAsync(message.SelectedProvider);
            }
        }
    }
}
