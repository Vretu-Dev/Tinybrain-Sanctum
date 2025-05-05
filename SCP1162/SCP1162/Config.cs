using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCP1162
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should debug messages be displayed?")]
        public bool Debug { get; set; } = false;

        [Description("Use Hints instead of Broadcast?")]
        public bool UseHints { get; set; } = true;
        [Description("Change the message that displays when you drop an item through SCP-1162.")]
        public string ItemDropMessage { get; set; } = "<i>You try to drop the item through <color=yellow>SCP-1162</color> to get another...</i>";
        public ushort ItemDropMessageDuration { get; set; } = 5;
        [Description("The list of item chances.")]
        public List<ItemType> Chances { get; set; } = new List<ItemType>
        {
            ItemType.KeycardO5,
            ItemType.SCP500,
            ItemType.MicroHID,
            ItemType.KeycardMTFCaptain,
            ItemType.KeycardContainmentEngineer,
            ItemType.SCP268,
            ItemType.GunCOM15,
            ItemType.SCP207,
            ItemType.Adrenaline,
            ItemType.GunCOM18,
            ItemType.KeycardFacilityManager,
            ItemType.Medkit,
            ItemType.KeycardMTFOperative,
            ItemType.KeycardGuard,
            ItemType.GrenadeHE,
            ItemType.KeycardZoneManager,
            ItemType.KeycardGuard,
            ItemType.Radio,
            ItemType.Ammo9x19,
            ItemType.Ammo12gauge,
            ItemType.Ammo44cal,
            ItemType.Ammo556x45,
            ItemType.Ammo762x39,
            ItemType.GrenadeFlash,
            ItemType.KeycardScientist,
            ItemType.KeycardJanitor,
            ItemType.Coin,
            ItemType.Flashlight
        };
    }
}