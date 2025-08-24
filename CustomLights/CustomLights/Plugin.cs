using System;
using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;
using Warhead = Exiled.Events.Handlers.Warhead;

namespace CustomLights
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override string Name { get; } = "Custom Lights";
        public override string Prefix { get; } = "CustomLights";
        public override string Author { get; } = "Vicious Vikki";
        public override Version Version { get; } = new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(9, 8, 1);
        public EventHandler EventHandler;
        
        public override void OnEnabled()
        {
            Instance = this;
            EventHandler = new EventHandler(this);
            Server.RoundStarted += EventHandler.OnRoundStarted;
            Warhead.Starting += EventHandler.OnWarheadStart;
            Warhead.Stopping += EventHandler.OnWarheadStop;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= EventHandler.OnRoundStarted;
            Warhead.Starting -= EventHandler.OnWarheadStart;
            Warhead.Stopping -= EventHandler.OnWarheadStop;
            EventHandler = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}