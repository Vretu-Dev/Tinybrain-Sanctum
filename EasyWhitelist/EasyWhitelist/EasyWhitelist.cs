using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace EasyWhitelist
{
    public class EasyWhitelist : Plugin<Config>
    {
        public override string Author => "Vretu";
        public override string Name => "EasyWhitelist";
        public override string Prefix => "easywl";
        public override Version Version => new Version(1, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);
        public static EasyWhitelist Instance { get; private set; }
        public bool WhitelistEnabled { get; set; } = EasyWhitelist.Instance.Config.EnableOnStartup;
        private static readonly string WhitelistFilePath = Path.Combine(Paths.Configs, "easywhitelist.txt");
        public readonly Dictionary<string, string> Whitelist = new Dictionary<string, string>();

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Player.Verified += OnPlayerVerified;
            LoadWhitelist();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= OnPlayerVerified;
            Instance = null;
            base.OnDisabled();
        }

        private void OnPlayerVerified(VerifiedEventArgs ev)
        {
            if (!WhitelistEnabled) return;

            if (Whitelist.TryGetValue(ev.Player.Nickname, out string userId))
            {
                if (userId == "pending")
                {
                    Whitelist[ev.Player.Nickname] = ev.Player.UserId;
                    SaveWhitelist();
                }
                else if (userId != ev.Player.UserId)
                {
                    ev.Player.Kick(EasyWhitelist.Instance.Config.KickMessage);
                }
            }
            else if (!Whitelist.ContainsValue(ev.Player.UserId))
            {
                ev.Player.Kick(EasyWhitelist.Instance.Config.KickMessage);
            }

            string joinUserId = ev.Player.UserId;
            string nick = ev.Player.Nickname;

            var entry = EasyWhitelist.Instance.Whitelist.FirstOrDefault(kvp => kvp.Value == joinUserId);
            if (!string.IsNullOrEmpty(entry.Key))
            {
                if (entry.Key != nick)
                {
                    EasyWhitelist.Instance.Whitelist.Remove(entry.Key);
                    EasyWhitelist.Instance.Whitelist[nick] = joinUserId;
                    EasyWhitelist.Instance.SaveWhitelist();
                }
            }
            else if (EasyWhitelist.Instance.Whitelist.ContainsKey(nick))
            {
                EasyWhitelist.Instance.Whitelist[nick] = joinUserId;
                EasyWhitelist.Instance.SaveWhitelist();
            }
        }

        public void LoadWhitelist()
        {
            Whitelist.Clear();
            if (File.Exists(WhitelistFilePath))
            {
                foreach (var line in File.ReadAllLines(WhitelistFilePath))
                {
                    var parts = line.Split('-');
                    if (parts.Length == 2)
                    {
                        Whitelist[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
        }

        public void SaveWhitelist()
        {
            File.WriteAllLines(WhitelistFilePath, Whitelist.Select(kvp => $"{kvp.Key} - {kvp.Value}"));
        }
    }
}