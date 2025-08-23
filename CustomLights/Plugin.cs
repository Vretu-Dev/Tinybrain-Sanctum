using System;
using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;

namespace CustomLights
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override string Name { get; } = "Custom Lights";
        public override string Prefix { get; } = "CustomLights";
        public override string Author { get; } = "Vicious Vikki";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 8, 1);
        public EventHandler EventHandler;
        
        public override void OnEnabled()
        {
            Instance = this;
            EventHandler = new EventHandler(this);
            Server.RoundStarted += EventHandler.OnRoundStarted;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= EventHandler.OnRoundStarted;
            EventHandler = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}