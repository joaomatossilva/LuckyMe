using System;
using System.Security.Principal;

namespace LuckyMe.Core.Extensions
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