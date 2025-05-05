using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Enums;

namespace ClassDChamberKeycard
{
    public static class Extensions
    {
        public static bool HasKeycardPermission(this Player player, KeycardPermissions permissions, bool requiresAllPermissions = false)
        {
            return requiresAllPermissions
                ? player.Items.Any(item => item is Keycard keycard && keycard.Permissions.HasFlag(permissions))
                : player.Items.Any(item => item is Keycard keycard && (keycard.Permissions & permissions) != 0);
        }
    }
}