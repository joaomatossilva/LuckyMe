using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class ConfirmEmailHandler : IAsyncRequestHandler<ConfirmEmail, IdentityResult>
    {
        private readonly ApplicationUserManager _userManager;

        public ConfirmEmailHandler(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(ConfirmEmail message)
        {
            return await _userManager.ConfirmEmailAsync(message.UserId, message.Code);
        }
    }
}
