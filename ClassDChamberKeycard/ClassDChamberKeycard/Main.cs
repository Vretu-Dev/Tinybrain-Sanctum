using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace ClassDChamberKeycard
{
    public class ClassDChamberKeycard : Plugin<Config>
    {
        public override string Name => "ClassDChamberKeycard";
        public override string Author => "Vretu";
        public override string Prefix => "ClassDChamberKeycard";
        public override Version Version => new Version(1, 1, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);
        public static ClassDChamberKeycard Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            Instance = null;
            base.OnDisabled();
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Door.Type == DoorType.PrisonDoor)
            {
                ev.Door.KeycardPermissions = KeycardPermissions.ArmoryLevelOne;

                if (ev.Player.HasKeycardPermission(ev.Door.KeycardPermissions))
                {
                    ev.IsAllowed = true;
                }
                else
                {
                    ev.IsAllowed = false;
                }
            }
        }
    }
}
