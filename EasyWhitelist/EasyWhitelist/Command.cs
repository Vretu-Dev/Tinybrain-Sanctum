using CommandSystem;
using System;
using System.Linq;
using Exiled.API.Features;
using System.Text.RegularExpressions;
using Exiled.Events.EventArgs.Player;
using Exiled.Permissions.Extensions;

namespace EasyWhitelist
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class EasyWhitelistCommand : ICommand
    {
        public string Command => "easywl";
        public string[] Aliases => new[] { "easwl" };
        public string Description => "Whitelist management commands";

        private static readonly Regex SteamIdRegex = new Regex(@"^\d{17}@steam$", RegexOptions.Compiled);

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("easywl.whitelist"))
            {
                response = "Missing permission easywl.whitelist";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = "Usage: easywl <on|off|list|add|remove|reload>";
                return false;
            }

            switch (arguments.At(0).ToLower())
            {
                case "on":
                    EasyWhitelist.Instance.WhitelistEnabled = true;
                    response = "Whitelist enabled.";
                    return true;
                case "off":
                    EasyWhitelist.Instance.WhitelistEnabled = false;
                    response = "Whitelist disabled.";
                    return true;
                case "list":
                    response = string.Join("\n", EasyWhitelist.Instance.Whitelist.Select(kvp => $"{kvp.Key} - {kvp.Value}"));
                    return true;
                case "add":
                    if (arguments.Count < 2)
                    {
                        response = "Usage: easywl add <SteamID64@steam/Nick>";
                        return false;
                    }
                    string entry = string.Join(" ", arguments.Skip(1));
                    if (SteamIdRegex.IsMatch(entry))
                    {
                        if (!EasyWhitelist.Instance.Whitelist.ContainsValue(entry))
                        {
                            string nickPlaceholder = $"User{EasyWhitelist.Instance.Whitelist.Count + 1}";
                            EasyWhitelist.Instance.Whitelist[nickPlaceholder] = entry;
                            EasyWhitelist.Instance.SaveWhitelist();
                            response = $"Added SteamID {entry} to the whitelist with placeholder nickname {nickPlaceholder}.";
                            return true;
                        }
                        response = "This SteamID is already whitelisted.";
                        return false;
                    }
                    else
                    {
                        if (!EasyWhitelist.Instance.Whitelist.ContainsKey(entry))
                        {
                            EasyWhitelist.Instance.Whitelist[entry] = "pending";
                            EasyWhitelist.Instance.SaveWhitelist();
                            response = $"Added {entry} to the whitelist. SteamID will be assigned on first join.";
                            return true;
                        }
                        response = "User already on the whitelist.";
                        return false;
                    }
                case "remove":
                    if (arguments.Count < 2)
                    {
                        response = "Usage: easywl remove <SteamID64@steam/Nick>";
                        return false;
                    }
                    string removeEntry = string.Join(" ", arguments.Skip(1));

                    if (SteamIdRegex.IsMatch(removeEntry))
                    {
                        // Delete via SteamID
                        var key = EasyWhitelist.Instance.Whitelist.FirstOrDefault(kvp => kvp.Value == removeEntry).Key;
                        if (!string.IsNullOrEmpty(key))
                        {
                            EasyWhitelist.Instance.Whitelist.Remove(key);
                            EasyWhitelist.Instance.SaveWhitelist();
                            response = $"Removed SteamID {removeEntry} from the whitelist.";
                            return true;
                        }
                    }
                    else
                    {
                        // Delete via nicku
                        if (EasyWhitelist.Instance.Whitelist.Remove(removeEntry))
                        {
                            EasyWhitelist.Instance.SaveWhitelist();
                            response = $"Removed {removeEntry} from the whitelist.";
                            return true;
                        }
                    }
                    response = "User not found on the whitelist.";
                    return false;
                case "reload":
                    EasyWhitelist.Instance.LoadWhitelist();
                    response = "Whitelist reloaded.";
                    return true;
                default:
                    response = "Invalid command.";
                    return false;
            }
        }
    }
}
