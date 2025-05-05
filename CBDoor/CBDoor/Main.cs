using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using Exiled.API.Features.Doors;
using UnityEngine;

namespace CBDoor
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "Vretu";
        public override string Name => "CBDoor";
        public override string Prefix => "CBDoor";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        private KeybindSetting CloseDoorKeybind;
        private static readonly Dictionary<int, bool> KeyWasPressed = new Dictionary<int, bool>();
        public override void OnEnabled()
        {
            CloseDoorKeybind = new KeybindSetting(
            id: 6565,
            label: "Close nearest door",
            suggested: KeyCode.E,
            preventInteractionOnGUI: true,
            hintDescription: "Closes nearest door in range, works as in SCP:CB",
            header: null,
            onChanged: OnKeyPressed
        );
            SettingBase.Register(new[] { CloseDoorKeybind });

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            SettingBase.Unregister(settings: new[] { CloseDoorKeybind });
            base.OnDisabled();
        }
        private void OnKeyPressed(Player player, SettingBase setting)
        {
            if (setting is KeybindSetting keybind)
            {
                bool isPressed = keybind.IsPressed;
                bool wasPressed = KeyWasPressed.TryGetValue(player.Id, out bool prev) && prev;
                if (isPressed && !wasPressed)
                {
                    float maxDistance = Config.ExtendedHitboxDistance;
                    Vector3 playerPos = player.CameraTransform.position;
                    var nearest = Door.List
                        .Where(d => d.IsOpen)
                        .OrderBy(d => Vector3.Distance(d.Position, playerPos))
                        .FirstOrDefault(d => Vector3.Distance(d.Position, playerPos) < maxDistance);

                    if (nearest is null)
                    {
                        //player.ShowHint($"Brak drzwi w zasięgu {maxDistance} metrów.", 2f);
                        return;
                    }
                    if (!nearest.IsOpen)
                    {
                        //player.ShowHint("Drzwi już są zamknięte!", 1f);
                        return;
                    }
                    if (nearest.IsKeycardDoor && !player.HasKeycardPermission(nearest.KeycardPermissions))
                    {
                        //player.ShowHint("Brak Permisji", 1f);
                        return;
                    }
                    nearest.IsOpen = false;
                    //player.ShowHint($"Zamknięto drzwi: {nearest.Name}", 1.5f);
                }
                KeyWasPressed[player.Id] = isPressed;
            }
        }
    }
}