using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.Core.Business;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Manage
{
    public static class RemovePhoneNumber
    {
        public class Command : IAsyncRequest<IdentityResult>
        {
            [CurrentUserId]
            public Guid UserId { get; set; }
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
                var result = await _userManager.SetPhoneNumberAsync(message.UserId, null);
                if (!result.Succeeded)
                {
                    return result;
                }
                var user = await _userManager.FindByIdAsync(message.UserId);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return result;
            }
        }
    }
}
