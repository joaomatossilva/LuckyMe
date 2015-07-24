using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account
{
    public class GetCodeVerify : IAsyncRequest<VerifyCode>
    {
        public string Provider { get; set; }
        public string ReturnUrl { get; set; } 
        public bool RememberMe { get; set; }
    }
}
