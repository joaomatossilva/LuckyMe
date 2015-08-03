using System;
using System.Threading.Tasks;
using LuckyMe.Core.Business;
using MediatR;

namespace LuckyMe.OwinIdentity.Manage
{
    public static class TwoFactorAuthentication
    {
        public class Command : IAsyncRequest
        {
            [CurrentUserId]
            public Guid UserId { get; set; }
            public bool Enabled { get; set; }
        }

        internal class CommandHandler: IAsyncRequestHandler<Command, Unit>
        {
            private readonly ApplicationUserManager _userManager;
            private readonly ApplicationSignInManager _signInManager;

            public CommandHandler(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<Unit> Handle(Command message)
            {
                await _userManager.SetTwoFactorEnabledAsync(message.UserId, message.Enabled);
                var user = await _userManager.FindByIdAsync(message.UserId);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return Unit.Value;
            }
        }
    }
}
