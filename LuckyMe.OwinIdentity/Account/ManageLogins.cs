using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.Core.Business;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LuckyMe.OwinIdentity.Account
{
    public static class ManageLogins
    {
        public class Query : IAsyncRequest<Model>
        {
            [CurrentUserId]
            public Guid UserId { get; set; }
        }

        public class Model
        {
            public IList<UserLoginInfo> CurrentLogins { get; set; }
            public IList<AuthenticationDescription> OtherLogins { get; set; }
            public bool CanDelete { get; set; }
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
                var user = await _userManager.FindByIdAsync(message.UserId);
                if (user == null)
                {
                    return null;
                }
                var userLogins = await _userManager.GetLoginsAsync(message.UserId);
                var otherLogins = _authenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
                return new Model
                       {
                           CurrentLogins = userLogins,
                           OtherLogins = otherLogins,
                           CanDelete = user.PasswordHash != null || userLogins.Count > 1
                       };
            }
        }
    }
}
