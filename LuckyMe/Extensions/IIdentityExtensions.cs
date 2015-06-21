using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace LuckyMe.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid GetUserIdAsGuid(this IIdentity identity)
        {
            Guid result;
            string id = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(identity);
            Guid.TryParse(id, out result);
            return result;
        }
    }
}