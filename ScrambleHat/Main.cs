using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using System;

namespace ScrambleHat
{
    public class Main : Plugin<Config>
    {
        public override string Name => "ScrambleHat";
        public override string Author => "Vretu";
        public override string Prefix { get; } = "ScrambleHat";
        public override Version RequiredExiledVersion { get; } = new Version(9, 4, 0);
        public override Version Version => new Version(1, 0, 0);
        public static Main Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            CustomItem.RegisterItems();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;
            CustomItem.UnregisterItems();
            base.OnDisabled();
        }
    }
}
