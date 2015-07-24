using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class SendCodeHandler : IAsyncRequestHandler<SendCode, bool>
    {
        private readonly ApplicationSignInManager _signInManager;

        public SendCodeHandler(ApplicationSignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(SendCode message)
        {
            return await _signInManager.SendTwoFactorCodeAsync(message.SelectedProvider);
        }
    }
}
