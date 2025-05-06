using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;

namespace NewUpdateFixes.EventHandlers
{
    public static class ColaHandler
    {
        public static void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
        }
        public static void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
        }
        private static readonly Dictionary<float, float> colaDamage1 = new Dictionary<float, float>
        {
            { 0.15f, 0.1f },
            { 0.225f, 0.15f },
            { 0.9f, 0.4f },
            { 1.5f, 1f },
        };
        private static readonly Dictionary<float, float> colaDamage2 = new Dictionary<float, float>
        {
            { 0.25f, 0.15f },
            { 0.375f, 0.23f },
            { 1.5f, 0.6f },
            { 2.5f, 1.5f },
        };
        private static readonly Dictionary<float, float> colaDamage3 = new Dictionary<float, float>
        {
            { 0.4f, 0.25f },
            { 0.6f, 0.38f },
            { 2.4f, 1f },
            { 4f, 2.5f }
        };
        private static void OnHurting(HurtingEventArgs ev)
        {
            if (NewUpdateFixes.Instance.Config.OldColaHealthDrain)
            {
                if (ev.DamageHandler.Type == DamageType.Scp207)
                {
                    byte intensity = ev.Player.GetEffect(EffectType.Scp207).Intensity;

                    if (intensity == 1)
                    {
                        colaDamage1.TryGetValue(ev.Amount, out float newDamage);
                        ev.Amount = newDamage;
                    }
                    if (intensity == 2)
                    {
                        colaDamage2.TryGetValue(ev.Amount, out float newDamage);
                        ev.Amount = newDamage;
                    }
                    if (intensity == 3)
                    {
                        colaDamage3.TryGetValue(ev.Amount, out float newDamage);
                        ev.Amount = newDamage;
                    }
                }
            }
        }
        private static void OnUsingItem(UsingItemEventArgs ev)
        {
            if (NewUpdateFixes.Instance.Config.OldColaSpeed)
            {
                byte intensity = ev.Player.GetEffect(EffectType.Scp207).Intensity;

                if (ev.Item.Type == ItemType.SCP207)
                {
                    if (intensity == 0)
                    {
                        ev.Player.EnableEffect(EffectType.MovementBoost, 4);
                    }
                    else if (intensity == 1)
                    {
                        ev.Player.DisableEffect(EffectType.MovementBoost);
                        ev.Player.EnableEffect(EffectType.MovementBoost, 6);
                    }
                    else if (intensity == 2)
                    {
                        ev.Player.DisableEffect(EffectType.MovementBoost);
                        ev.Player.EnableEffect(EffectType.MovementBoost, 3);
                    }
                }
            }
            if (ev.Item.Type == ItemType.SCP500)
            {
                if (NewUpdateFixes.Instance.Config.Scp500CuresTrauma)
                {
                    ev.Player.DisableEffect(EffectType.Traumatized);
                }
                if (NewUpdateFixes.Instance.Config.OldColaSpeed)
                {
                    ev.Player.DisableEffect(EffectType.MovementBoost);
                }
            }
        }
    }
}