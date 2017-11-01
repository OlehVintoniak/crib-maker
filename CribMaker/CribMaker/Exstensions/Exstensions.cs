using System.Linq;
using System.Security.Principal;

namespace CribMaker.Exstensions
{
    public static class Exstensions
    {
        public static bool IsInAnyRole(this IPrincipal principal, params string[] roles)
        {
            return roles.Any(principal.IsInRole);
        }
    }
}