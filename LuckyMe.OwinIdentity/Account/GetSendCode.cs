using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account
{
    public class GetSendCode : IAsyncRequest<SendCode>
    {
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
