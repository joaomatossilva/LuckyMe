using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LuckyMe.OwinIdentity.Account
{
    public class SendCode : IAsyncRequest<bool>
    {
        public string SelectedProvider { get; set; }
        public IEnumerable<string> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
