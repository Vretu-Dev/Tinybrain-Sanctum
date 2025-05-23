using CameraShaking;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System;

namespace CustomACC
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "CustomACC";
        public override string Author => "Vretu";
        public override string Prefix => "CustomAcc";
        public override Version Version => new Version(1, 0, 0);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.AimingDownSight += OnAimingDownSight;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.AimingDownSight -= OnAimingDownSight;
            base.OnDisabled();
        }

        private void OnAimingDownSight(AimingDownSightEventArgs ev)
        {
            if (ev.Firearm == null)
                return;

            string firearmName = ev.Firearm.FirearmType.ToString();

            if (Config.Weapons.TryGetValue(firearmName, out var settings))
            {
                if (ev.AdsIn)
                {
                    if (settings.AimingInaccuracy.HasValue)
                        ev.Firearm.Inaccuracy = settings.AimingInaccuracy.Value;
                }
                else
                {
                    if (settings.Inaccuracy.HasValue)
                        ev.Firearm.Inaccuracy = settings.Inaccuracy.Value;
                }
            }
        }
    }
}