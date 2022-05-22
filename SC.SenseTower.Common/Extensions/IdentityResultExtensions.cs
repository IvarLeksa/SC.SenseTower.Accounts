using Microsoft.AspNetCore.Identity;

namespace SC.SenseTower.Common.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string GetMessages(this IdentityResult identityResult)
        {
            return string.Join("\n", identityResult.Errors.Select(r => r.Description));
        }
    }
}
