using System;
using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerEffects = CustomPlayerEffects;

namespace AdrenalinRush
{
    public class AdrenalinRush : Plugin<Config>
    {
        public override string Name => "Adrenalin Rush";
        public override string Author => "Vretu";
        public override string Prefix { get; } = "AR";
        public override Version Version => new Version(2, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.UsedItem += OnUsedItem;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.UsedItem -= OnUsedItem;
            base.OnDisabled();
        }
        // Handler for Using Adrenaline
        private void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.Adrenaline)
            {
                if (!ev.Player.IsEffectActive<Scp207>())
                {
                    Timing.RunCoroutine(ApplySmoothBoost(ev.Player));
                }
            }
        }
        private IEnumerator<float> ApplySmoothBoost(Player player)
        {
            float currentIntensity = 0f;
            float increment = Config.BoostIntensity / Config.SpeedIncrease; // Adjust how fast the boost grows
            float duration = Config.BoostDuration;
            float stepDuration = duration / Config.Steps; // Number of steps after speed is refreshed in the duration

            // Enable movement boost effect for full duration setting in config
            player.EnableEffect(EffectType.MovementBoost, duration);

            // Retrieve active movement boost effect
            var movementBoost = player.ReferenceHub.playerEffectsController.GetEffect<PlayerEffects.MovementBoost>();

            // Smooth increase intensity
            while (currentIntensity < Config.BoostIntensity)
            {
                currentIntensity += increment;
                movementBoost.Intensity = (byte)currentIntensity;
                yield return Timing.WaitForSeconds(stepDuration);
            }
            // Maximum intensity
            movementBoost.Intensity = Config.BoostIntensity;

            yield return Timing.WaitForSeconds(duration);

            player.DisableEffect(EffectType.MovementBoost);
        }
    }
}