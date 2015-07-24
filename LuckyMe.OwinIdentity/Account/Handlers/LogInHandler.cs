using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity.Owin;

namespace LuckyMe.OwinIdentity.Account.Handlers
{
    public class LogInHandler : IAsyncRequestHandler<LogIn, SignInStatus>
    {
        private readonly ApplicationSignInManager _signInManager;

        public LogInHandler(ApplicationSignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInStatus> Handle(LogIn message)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(message.Email, message.Password, message.RememberMe, shouldLockout: false);
            return result;
        }
    }
}
