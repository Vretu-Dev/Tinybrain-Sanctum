using System;
using Exiled.API.Features;

namespace SCP1162
{
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "scp1162";
        public override string Name => "SCP1162";
        public override string Author => "xRoier";
        public EventHandlers EventHandlers;
        public override Version Version { get; } = new Version(9, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override void OnEnabled()
        {
            EventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Player.DroppingItem += EventHandlers.OnItemDropped;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.DroppingItem -= EventHandlers.OnItemDropped;
            EventHandlers = null;
            base.OnDisabled();
        }
    }
}