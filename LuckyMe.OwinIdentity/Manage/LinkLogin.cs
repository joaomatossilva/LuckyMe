using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LuckyMe.OwinIdentity.Manage
{
    public static class LinkLogin
    {
        public class Command : IAsyncRequest<IdentityResult>
        {
            public Guid UserId { get; set; }
            public string XsrfKey { get; set; }
        }

        internal class CommandHandler : IAsyncRequestHandler<Command, IdentityResult>
        {
            private readonly IAuthenticationManager _authenticationManager;
            private readonly ApplicationUserManager _userManager;

            public CommandHandler(IAuthenticationManager authenticationManager, ApplicationUserManager userManager)
            {
                _authenticationManager = authenticationManager;
                _userManager = userManager;
            }

            public async Task<IdentityResult> Handle(Command message)
            {
                var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync(message.XsrfKey, message.UserId.ToString());
                if (loginInfo == null)
                {
                    return IdentityResult.Failed();
                }
                return await _userManager.AddLoginAsync(message.UserId, loginInfo.Login);
            }
        }
    }
}
