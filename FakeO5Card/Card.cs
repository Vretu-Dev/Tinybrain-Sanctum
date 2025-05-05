using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace FakeO5Card
{
    [CustomItem(ItemType.KeycardO5)]
    public class CustomO5Keycard : CustomKeycard
    {
        public override uint Id { get; set; } = 155;
        public override string Name { get; set; } = "FakeO5Keycard";
        public override string Description { get; set; } = null;
        public override ItemType Type { get; set; } = ItemType.KeycardO5;
        public override SpawnProperties SpawnProperties { get; set; } = null;
        public override float Weight { get; set; } = 1f;
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;

            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;

            base.UnsubscribeEvents();
        }
        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (TryGet(ev.Pickup, out var customItem) && customItem.Id == 155)
            {
                ev.Player.Role.Set(RoleTypeId.Spectator);
                ev.Player.ShowHint("You picked up a custom O5 card and were killed!", 5f);
                ev.IsAllowed = false;
            }
        }
    }
}