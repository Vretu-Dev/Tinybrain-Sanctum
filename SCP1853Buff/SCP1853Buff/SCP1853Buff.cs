using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SCP1853Buff
{
    public class SCP1853Buff : Plugin<Config>
    {
        public override string Name => "SCP1853Buff";
        public override string Author => "Vretu";
        public override string Prefix { get; } = "SCP1853Buff";
        public override Version Version => new Version(1, 3, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        private Timer staminaCheckTimer;
        private HashSet<Player> playersWithEffect;
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RestartingRound += OnRestartingRound;
            Exiled.Events.Handlers.Player.ReceivingEffect += OnReceivingEffect;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            playersWithEffect = new HashSet<Player>();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.RestartingRound -= OnRestartingRound;
            Exiled.Events.Handlers.Player.ReceivingEffect -= OnReceivingEffect;
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
        private void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            if (ev.Effect.GetEffectType() == EffectType.Scp1853)
            {
                if (ev.Intensity > 0)
                {
                    ev.Player.EnableEffect(EffectType.MovementBoost, Config.MovementBoostIntensity);
                    playersWithEffect.Add(ev.Player);
                }
                else
                {
                    playersWithEffect.Remove(ev.Player);
                    ev.Player.DisableEffect(EffectType.MovementBoost);
                }
            }
            else if (Config.IgnoreAnotherMovementBoost)
            {
                if (playersWithEffect.Contains(ev.Player) && ev.Effect.GetEffectType() == EffectType.MovementBoost)
                {
                    ev.IsAllowed = false;
                }
            }
        }
        private void OnStaminaCheck(object sender, ElapsedEventArgs e)
        {
            foreach (var player in playersWithEffect.ToList())
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