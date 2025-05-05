using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bleeding
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enable.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public int BleedingDuration { get; set; } = 10;
        public float DamagePerSecond { get; set; } = 2f;
        public int BleedingBelowHealth { get; set; } = 20;
        public string StartBleeding { get; set; } = "<color=red>You are bleeding!</color>";
        public string StopBleeding { get; set; } = "<color=green>Bleeding stopped!</color>";
        public ushort MessageDuration { get; set; } = 3;
        public List<ItemType> StopBleedingItems { get; set; } = new List<ItemType>
        {
            ItemType.SCP500,
            ItemType.Medkit
        };
    }
}
