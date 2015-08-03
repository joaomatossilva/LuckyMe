using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.Core.Business;
using LuckyMe.OwinIdentity.Account;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Manage
{
    public static class RemoveLogin
    {
        public class Command : IAsyncRequest<IdentityResult>
        {
            [CurrentUserId]
            public Guid UserId { get; set; }
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
        }

        internal class CommandHandler : IAsyncRequestHandler<Command, IdentityResult>
        {
            private readonly ApplicationUserManager _userManager;
            private readonly ApplicationSignInManager _signInManager;

            public CommandHandler(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<IdentityResult> Handle(Command message)
            {
                var result = await _userManager.RemoveLoginAsync(message.UserId, new UserLoginInfo(message.LoginProvider, message.ProviderKey));
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(message.UserId);
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return result;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            }
        }
    }
}
