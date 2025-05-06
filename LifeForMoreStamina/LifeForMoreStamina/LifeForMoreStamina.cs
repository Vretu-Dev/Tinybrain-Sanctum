using System;
using System.Timers;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Player = Exiled.API.Features.Player;

namespace LifeForMoreStamina
{
    public class LifeForMoreStamina : Plugin<Config>
    {
        private Timer staminaCheckTimer;
        public override string Name => "LifeForMoreStamina";
        public override string Author => "Vretu";
        public override string Prefix { get; } = "LifeForMoreStamina";
        public override Version Version => new Version(1, 2, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RestartingRound += OnRestartingRound;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.RestartingRound -= OnRestartingRound;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            base.OnDisabled();
        }
        private void OnRoundStarted()
        {
            staminaCheckTimer = new Timer(100);
            staminaCheckTimer.Elapsed += OnStaminaCheck;
            staminaCheckTimer.Start();
        }
        private void OnRestartingRound()
        {
            if (staminaCheckTimer != null)
            {
                staminaCheckTimer.Stop();
                staminaCheckTimer.Dispose();
            }
        }
        private void OnStaminaCheck(object sender, ElapsedEventArgs e)
        {
            foreach (var player in Player.List)
            {
                if (player.Role.Side != Side.Scp)
                {
                    if (player.Stamina <= Config.StaminaThreshold)
                    {
                        player.Stamina = Math.Max(player.Stamina, Config.StaminaAdded);

                        if (player.Stamina == Config.StaminaAdded)
                        {
                            player.Health -= Config.HpRemoved;

                            if (Config.VisualEffect)
                            {
                                player.EnableEffect(EffectType.Bleeding, 1, 2);
                            }
                            if (!Config.VisualEffect && Config.KillOnZeroHp && player.Health < 0.8)
                            {
                                player.EnableEffect(EffectType.Bleeding, 1, 2);
                            }
                        }
                    }
                }
            }
        }
        private void OnHurting(HurtingEventArgs ev)
        {
            if (ev.DamageHandler.Type == DamageType.Bleeding)
            {
                if (Config.VisualEffect)
                {
                    ev.IsAllowed = false;
                }
                if (Config.KillOnZeroHp && ev.Player.Health < 0.8)
                {
                    ev.IsAllowed = true;
                }
            }
        }
    }
}