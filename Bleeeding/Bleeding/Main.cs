using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;

namespace Bleeding
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "Bleeding";
        public override string Author => "Vretu";
        public override string Prefix => "Bleeding";
        public override Version RequiredExiledVersion => new Version(9, 0, 0);
        public override Version Version => new Version(1, 0, 0);

        private readonly Dictionary<Player, CoroutineHandle> bleedingCoroutines = new Dictionary<Player, CoroutineHandle>();

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Hurting += OnPlayerHurting;
            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWaitingForPlayers;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnPlayerHurting;
            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnWaitingForPlayers;
            ClearAllCoroutines();
            base.OnDisabled();
        }

        private void OnWaitingForPlayers()
        {
            ClearAllCoroutines();
        }

        private void ClearAllCoroutines()
        {
            foreach (var handle in bleedingCoroutines.Values)
            {
                Timing.KillCoroutines(handle);
            }
            bleedingCoroutines.Clear();
        }

        private void OnPlayerHurting(HurtingEventArgs ev)
        {
            float RemainingHealth = ev.Player.Health - ev.Amount;

            if (RemainingHealth < 20 && ev.DamageHandler.Type == DamageType.Falldown)
            {
                StartBleeding(ev.Player);
            }

            if (!ev.IsAllowed || ev.Attacker == null || ev.Player == null)
                return;

            if (ev.Attacker.Role == RoleTypeId.Scp096 || ev.Attacker.Role == RoleTypeId.Scp939)
            {
                StartBleeding(ev.Player);
            }

            if (RemainingHealth < 20)
            {
                StartBleeding(ev.Player);
            }
        }

        private void OnUsedItem(UsedItemEventArgs ev)
        {
            if (Config.StopBleedingItems.Contains(ev.Item.Type))
            {
                StopBleeding(ev.Player);
            }
        }

        private void StartBleeding(Player player)
        {
            if (bleedingCoroutines.ContainsKey(player))
                return;

            bleedingCoroutines[player] = Timing.RunCoroutine(ApplyBleedingEffect(player));
            player.ShowHint(Config.StartBleeding, Config.MessageDuration);
        }

        private void StopBleeding(Player player)
        {
            if (bleedingCoroutines.TryGetValue(player, out CoroutineHandle handle))
            {
                Timing.KillCoroutines(handle);
                bleedingCoroutines.Remove(player);
                player.ShowHint(Config.StopBleeding, Config.MessageDuration);
            }
        }

        private IEnumerator<float> ApplyBleedingEffect(Player player)
        {
            int bleedingDuration = Config.BleedingDuration;
            float damagePerSecond = Config.DamagePerSecond;

            for (int i = 0; i < bleedingDuration; i++)
            {
                if (player.IsAlive && player.IsHuman)
                {
                    player.Hurt(damagePerSecond, DamageType.Bleeding);
                }
                yield return Timing.WaitForSeconds(1f);
            }

            if (bleedingCoroutines.ContainsKey(player))
                bleedingCoroutines.Remove(player);
        }
    }
}