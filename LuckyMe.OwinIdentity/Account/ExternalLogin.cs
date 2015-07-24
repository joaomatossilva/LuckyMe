using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity.Owin;

namespace LuckyMe.OwinIdentity.Account
{
    public class ExternalLogin : IAsyncRequest<ExternalLoginResult>
    {
        public string ReturnUrl { get; set; }
    }

    public class ExternalLoginResult
    {
        public SignInStatus SignInStatus { get; set; }
        public ExternalLoginInfo LoginInfo { get; set; }
    }
}
