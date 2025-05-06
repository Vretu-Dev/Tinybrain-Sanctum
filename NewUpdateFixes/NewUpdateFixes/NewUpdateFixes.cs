using System;
using Exiled.API.Features;

namespace NewUpdateFixes
{
    public class NewUpdateFixes : Plugin<Config>
    {
        public override string Name => "NewUpdateFixes";
        public override string Author => "Half";
        public override string Prefix { get; } = "NUF";
        public override Version Version => new Version(1, 5, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);
        public static NewUpdateFixes Instance { get; private set; }
        public override void OnEnabled()
        {
            Instance = this;
            EventHandlers.ColaHandler.RegisterEvents();
            if(Config.EnableCustomJailbirdSettings) { EventHandlers.JailbirdHandler.RegisterEvents(); }
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;
            EventHandlers.ColaHandler.UnregisterEvents();
            if(Config.EnableCustomJailbirdSettings) { EventHandlers.JailbirdHandler.UnregisterEvents(); }
            base.OnDisabled();
        }
    }
}