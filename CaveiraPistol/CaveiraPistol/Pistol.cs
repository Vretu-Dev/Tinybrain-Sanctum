using System.Linq;
using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Core.UserSettings;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using MEC;
using PlayerRoles;
using PlayerStatsSystem;
using UnityEngine;
using System;

namespace CaveiraPistol
{
    [CustomItem(ItemType.GunCOM15)]
    public class Com18BoostItem : CustomWeapon
    {
        public override ItemType Type { get; set; } = ItemType.GunCOM18;
        public override uint Id { get; set; } = 122;
        public override string Name { get; set; } = "Caveira Pistol";
        public override string Description { get; set; } = "Kill Everyone!";
        public override float Damage { get; set; } = Main.Instance.Config.Damage;
        public override byte ClipSize { get; set; } = 11;
        public override bool FriendlyFire { get; set; } = true;
        public override float Weight { get; set; } = 1f;
        public float DamageMultiplier { get; set; } = Main.Instance.Config.RampageDamageMultiplier;
        public HeaderSetting SettingsHeader { get; set; } = new HeaderSetting(110, "Caveira Pistol");

        private Dictionary<Player, CoroutineHandle> effectWindows = new Dictionary<Player, CoroutineHandle>();
        private Dictionary<Player, KeyCode> playerKeybinds = new Dictionary<Player, KeyCode>();
        private KeybindSetting rampageKeybind;
        
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            DynamicSpawnPoints = Main.Instance.Config.SpawnLocations
                .Select(entry => new DynamicSpawnPoint
                {
                    Location = entry.Key,
                    Chance = entry.Value
                }).ToList()
        };
        public override AttachmentName[] Attachments { get; set; } = new[]
        {
        AttachmentName.None,
        AttachmentName.SoundSuppressor,
        AttachmentName.StandardMagAP,
        AttachmentName.IronSights,
        };
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.ChangingItem += OnChangedItem;
            Exiled.Events.Handlers.Item.ChangingAttachments += ChangingAttachmentEvent;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;

            rampageKeybind = new KeybindSetting(
                id: 111,
                label: "Rampage Mode",
                suggested: KeyCode.B,
                hintDescription: "Press key to activate rampage.",
                onChanged: OnKeybindChanged
            );
            SettingBase.Register(new[] { SettingsHeader });
            SettingBase.Register(new[] { rampageKeybind });

            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.ChangingItem -= OnChangedItem;
            Exiled.Events.Handlers.Item.ChangingAttachments -= ChangingAttachmentEvent;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
            SettingBase.Unregister(settings: new[] { SettingsHeader });
            SettingBase.Unregister(settings: new[] { rampageKeybind });
            base.UnsubscribeEvents();
        }
        private void OnKeybindChanged(Player player, SettingBase setting)
        {
            if (setting is KeybindSetting keybind)
            {
                playerKeybinds[player] = keybind.KeyCode;

                if (effectWindows.ContainsKey(player) && keybind.KeyCode == playerKeybinds[player] && Check(player.CurrentItem))
                {
                    ActivateEffects(player);
                }
            }
        }
        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (TryGet(ev.Pickup, out var customItem) && customItem.Id == 122)
            {
                ev.IsAllowed = false;
                ev.Pickup.Destroy();
                customItem.Give(ev.Player);
            }
        }
        public void ChangingAttachmentEvent(ChangingAttachmentsEventArgs ev)
        {
            if (Check(ev.Item))
            {
                if (TryGet(ev.Player.CurrentItem, out var item) && item.Id == 122)
                {
                    ev.IsAllowed = false;
                }
            }
        }
        private void OnChangedItem(ChangingItemEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem))
            {
                if (ev.Player.Items.FirstOrDefault(item => item.Type == ItemType.SCP1344)?.Is<Exiled.API.Features.Items.Scp1344>(out var google) == true && google.IsWorn)
                {
                    ev.Player.DisableEffect(EffectType.MovementBoost);
                    ev.Player.DisableEffect(EffectType.SilentWalk);
                    ev.Player.DisableEffect(EffectType.Scanned);
                    ev.Player.DisableEffect(EffectType.Vitality);
                }
                else
                {
                    ev.Player.DisableEffect(EffectType.MovementBoost);
                    ev.Player.DisableEffect(EffectType.SilentWalk);
                    ev.Player.DisableEffect(EffectType.Scanned);
                    ev.Player.DisableEffect(EffectType.Vitality);
                    ev.Player.DisableEffect(EffectType.Scp1344);
                }
            }
        }
        protected override void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != ev.Player && ev.DamageHandler.Base is FirearmDamageHandler firearmDamageHandler && firearmDamageHandler.WeaponType == ev.Attacker.CurrentItem.Type && Check(ev.Attacker.CurrentItem))
            {
                ev.DamageHandler.Damage = Damage;

                if(ev.Attacker.IsEffectActive<SilentWalk>())
                    ev.DamageHandler.Damage = DamageMultiplier * Damage;

                if (!Main.Instance.Config.Scp207 && ev.Attacker.IsEffectActive<Scp207>())
                    ev.DamageHandler.Damage = Damage;

                if (!Main.Instance.Config.Scp1853 && ev.Attacker.IsEffectActive<Scp1853>())
                    ev.DamageHandler.Damage = Damage;

                if (!Main.Instance.Config.Antiscp207 && ev.Attacker.IsEffectActive<AntiScp207>())
                    ev.DamageHandler.Damage = Damage;

                if (ev.Player.Role.Team == Team.SCPs)
                    ev.DamageHandler.Damage = Damage;
            }

            if (!Check(ev.Player.CurrentItem))
                return;

            if (ev.Attacker != ev.Player)
            {
                if (ev.DamageHandler.Base is FirearmDamageHandler || ev.DamageHandler.Type == DamageType.Explosion)
                    {
                        if (!Main.Instance.Config.Scp207 && ev.Player.IsEffectActive<Scp207>())
                            return;
                        if (!Main.Instance.Config.Scp1853 && ev.Player.IsEffectActive<Scp1853>())
                            return;
                        if (!Main.Instance.Config.Antiscp207 && ev.Player.IsEffectActive<AntiScp207>())
                            return;
                        if (Main.Instance.Config.Hint)
                            ev.Player.ShowHint($"<color=yellow>{new string('\n', 5)}{string.Format(Main.Instance.Config.WindowTimeActive)}</color>", Main.Instance.Config.HintDuration);

                    if (Check(ev.Player.CurrentItem))
                    {
                        if (effectWindows.ContainsKey(ev.Player))
                        {
                            Timing.KillCoroutines(effectWindows[ev.Player]);
                            effectWindows.Remove(ev.Player);
                        }

                        effectWindows[ev.Player] = Timing.RunCoroutine(EffectWindow(ev.Player));
                    }
                }
            }
        }
        private IEnumerator<float> EffectWindow(Player player)
        {
            float duration = Main.Instance.Config.RampageWindowActivation;

            yield return Timing.WaitForSeconds(duration);

            if (effectWindows.ContainsKey(player))
            {
                effectWindows.Remove(player);
                if (Main.Instance.Config.Hint)
                    player.ShowHint($"<color=yellow>{new string('\n', 5)}{string.Format(Main.Instance.Config.WindowTimeExpired)}</color>", Main.Instance.Config.HintDuration);
            }
        }
        public void ActivateEffects(Player player)
        {
            if (player.Items.FirstOrDefault(item => item.Type == ItemType.SCP1344)?.Is<Exiled.API.Features.Items.Scp1344>(out var google) == true && google.IsWorn)
            {
                player.EnableEffect(EffectType.MovementBoost, 40, Main.Instance.Config.RampageDuration);
                player.EnableEffect(EffectType.SilentWalk, 10, Main.Instance.Config.RampageDuration);
                player.EnableEffect(EffectType.Scanned, 10, Main.Instance.Config.RampageDuration);
                player.EnableEffect(EffectType.Vitality, 10, Main.Instance.Config.RampageDuration);
            }
            else
            {
                player.EnableEffect(EffectType.MovementBoost, 40, Main.Instance.Config.RampageDuration);
                player.EnableEffect(EffectType.SilentWalk, 10, Main.Instance.Config.RampageDuration);
                player.EnableEffect(EffectType.Scanned, 10, Main.Instance.Config.RampageDuration);
                player.EnableEffect(EffectType.Vitality, 10, Main.Instance.Config.RampageDuration);
                player.EnableEffect(EffectType.Scp1344, 10, Main.Instance.Config.RampageDuration);
            }

            if (Main.Instance.Config.Hint)
                player.ShowHint($"<color=green>{new string('\n', 5)}{string.Format(Main.Instance.Config.RampageActivated)}</color>", Main.Instance.Config.HintDuration);

            if (effectWindows.ContainsKey(player))
            {
                Timing.KillCoroutines(effectWindows[player]);
                effectWindows.Remove(player);
            }
        }
    }
}