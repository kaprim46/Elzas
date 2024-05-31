using System.Security.Claims;

namespace API.Extensions
{
    public static class CliamsPricipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            // return user?.Claims?.FirstOrDefault(q => q.Type == ClaimTypes.Email)?.Value;
            // now is simplified to
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}