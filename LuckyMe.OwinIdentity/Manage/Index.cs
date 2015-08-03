using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.Core.Business;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LuckyMe.OwinIdentity.Manage
{
    public static class Index
    {
        public class Query : IAsyncRequest<Model>
        {
            [CurrentUserId]
            public Guid UserId { get; set; }
        }

        public class Model
        {
            public bool HasPassword { get; set; }
            public IList<UserLoginInfo> Logins { get; set; }
            public string PhoneNumber { get; set; }
            public bool TwoFactor { get; set; }
            public bool BrowserRemembered { get; set; }
        }


        internal class QueryHandler : IAsyncRequestHandler<Query, Model>
        {
            private readonly ApplicationUserManager _userManager;
            private readonly IAuthenticationManager _authenticationManager;

            public QueryHandler(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            {
                _userManager = userManager;
                _authenticationManager = authenticationManager;
            }

            public async Task<Model> Handle(Query message)
            {
                var hasPassword = false;
                var user = _userManager.FindById(message.UserId);
                if (user != null)
                {
                    hasPassword = user.PasswordHash != null;
                }
                var model = new Model
                {
                    HasPassword = hasPassword,
                    PhoneNumber = await _userManager.GetPhoneNumberAsync(message.UserId),
                    TwoFactor = await _userManager.GetTwoFactorEnabledAsync(message.UserId),
                    Logins = await _userManager.GetLoginsAsync(message.UserId),
                    BrowserRemembered = await _authenticationManager.TwoFactorBrowserRememberedAsync(message.UserId.ToString())
                };
                return model;
            }
        }
    }
}
