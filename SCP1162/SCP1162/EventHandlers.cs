using Exiled.API.Extensions;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using SCP1162.API;
using UnityEngine;

namespace SCP1162
{
    public class EventHandlers
    {
        private readonly Plugin _plugin;
        public EventHandlers(Plugin plugin) => _plugin = plugin;

        public void OnItemDropped(DroppingItemEventArgs ev)
        {
            if (!ev.IsAllowed) return;
            if (Vector3.Distance(ev.Player.Position, RoleTypeId.Scp173.GetRandomSpawnLocation().Position) <= 8.2f)
            {
                if (_plugin.Config.UseHints)
                    ev.Player.ShowHint(_plugin.Config.ItemDropMessage, _plugin.Config.ItemDropMessageDuration);
                else
                    ev.Player.Broadcast(_plugin.Config.ItemDropMessageDuration, _plugin.Config.ItemDropMessage, Broadcast.BroadcastFlags.Normal, true);
                ev.IsAllowed = false;
                var oldItem = ev.Item.Base.ItemTypeId;
                ev.Player.RemoveItem(ev.Item);
                var newItemType = _plugin.Config.Chances[Random.Range(0, _plugin.Config.Chances.Count)];
                var eventArgs = new UsingScp1162EventArgs(ev.Player, newItemType, oldItem);
                Scp1162Event.OnUsingScp1162(eventArgs);
                var newItem = Item.Create(eventArgs.ItemAfter);
                ev.Player.AddItem(newItem);
                ev.Player.DropItem(newItem);
            }
        }
    }
}