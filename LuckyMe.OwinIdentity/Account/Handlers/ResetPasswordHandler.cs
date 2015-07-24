using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class ResetPasswordHandler : IAsyncRequestHandler<ResetPassword, IdentityResult>
    {
        private readonly ApplicationUserManager _userManager;

        public ResetPasswordHandler(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(ResetPassword message)
        {
            var user = await _userManager.FindByNameAsync(message.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return IdentityResult.Success;
            }
            return await _userManager.ResetPasswordAsync(user.Id, message.Code, message.Password);
        }
    }
}
