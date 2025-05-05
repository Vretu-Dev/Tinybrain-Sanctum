using Exiled.API.Features;
using Exiled.Events.EventArgs.Warhead;
using System;

namespace DisableWarhead
{
    public class StopWarheadPlugin : Plugin<Config>
    {
        public override string Name => "DisableWarhead";
        public override string Author => "Vretu";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        public override void OnEnabled()
        {
            base.OnEnabled();
            Exiled.Events.Handlers.Warhead.Starting += OnWarheadStarting;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Exiled.Events.Handlers.Warhead.Starting -= OnWarheadStarting;
        }

        private void OnWarheadStarting(StartingEventArgs ev)
        {
            ev.IsAllowed = false;
        }
    }
}