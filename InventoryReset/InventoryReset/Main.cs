using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace InventoryReset
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "Vretu";
        public override string Name => "InventoryReset";
        public override string Prefix => "InventoryReset";
        public override Version Version => new Version(1, 0, 0);
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Escaping += OnPlayerEscaping;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Escaping -= OnPlayerEscaping;
            base.OnDisabled();
        }
        private void OnPlayerEscaping(EscapingEventArgs ev)
        {
            if (ev.Player.IsHuman && ev.Player != null && ev.IsAllowed)
            {
                ev.Player.ClearInventory();
            }
        }
    }
}