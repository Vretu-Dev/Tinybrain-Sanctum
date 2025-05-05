using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Map;
using System;
using UnityEngine;

namespace No_Goggles
{
    public class NoGoggles : Plugin<Config>
    {
        public override string Name => "NoGoggles";
        public override string Author => "Vretu";
        public override string Prefix { get; } = "NoGoggles";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Map.FillingLocker += OnFillingLocker;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Map.FillingLocker -= OnFillingLocker;
            base.OnDisabled();
        }
        private void OnFillingLocker(FillingLockerEventArgs ev)
        {
            if (ev.Pickup.Type == ItemType.SCP1344)
            {
                ev.IsAllowed = false;
                Pickup.CreateAndSpawn(ItemType.SCP500, ev.Pickup.Position, Quaternion.identity);
            }
        }
    }
}
