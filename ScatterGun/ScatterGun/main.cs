using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using System;

namespace ScatterGun
{
    public class Main : Plugin<Config>
    {
        public override string Name => "ScatterGun";
        public override string Author => "Vretu";
        public override string Prefix => "ScatterGun";
        public override Version Version => new Version(1, 1, 0);

        public static Main Instance;

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