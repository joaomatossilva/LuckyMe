using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.Identity;

namespace LuckyMe.OwinIdentity.Account
{
    public class ConfirmEmail : IAsyncRequest<IdentityResult>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
