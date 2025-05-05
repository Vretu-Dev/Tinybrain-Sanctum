using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Cassie;

namespace MuteCassie
{
    public class EasyWhitelist : Plugin<Config>
    {
        public override string Author => "Vretu";
        public override string Name => "MuteCassie";
        public override string Prefix => "MuteCassie";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Cassie.SendingCassieMessage += OnSendingCassieMessage;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Cassie.SendingCassieMessage -= OnSendingCassieMessage;
            base.OnDisabled();
        }

        public void OnSendingCassieMessage(SendingCassieMessageEventArgs ev)
        {
            ev.IsAllowed = false;
        }
    }
}