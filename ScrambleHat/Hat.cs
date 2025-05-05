using System.Linq;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Exiled.Events.EventArgs.Scp096;

namespace ScrambleHat
{
    [CustomItem(ItemType.SCP268)]
    public class ScrambleHat : CustomItem
    {
        public override ItemType Type { get; set; } = ItemType.SCP268;
        public override uint Id { get; set; } = 268;
        public override string Name { get; set; } = "Scramble Hat";
        public override string Description { get; set; }
        public override float Weight { get; set; } = 1f;
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            DynamicSpawnPoints = Main.Instance.Config.SpawnLocations
                .Select(entry => new DynamicSpawnPoint
                {
                    Location = entry.Key,
                    Chance = entry.Value
                }).ToList()
        };
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Scp096.AddingTarget += OnTriggering096;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.UsingItem += OnSCP268Used;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Scp096.AddingTarget -= OnTriggering096;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.UsingItem -= OnSCP268Used;
            base.UnsubscribeEvents();
        }
        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player.Role == RoleTypeId.Scp096 && HasScp268(ev.Attacker))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnTriggering096(AddingTargetEventArgs ev)
        {
            if (HasScp268(ev.Target))
            {
                ev.IsAllowed = false;
            }
        }
        private void OnSCP268Used(UsingItemEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem))
            {
                ev.IsAllowed = false;
            }
        }
        private bool HasScp268(Player player)
        {
            return CustomItem.TryGet(player, out IEnumerable<CustomItem>? customItems);
        }

    }
}