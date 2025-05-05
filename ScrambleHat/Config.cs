using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScrambleHat
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Spawn locations with their respective chances. Format: Location: Chance")]
        public Dictionary<SpawnLocationType, int> SpawnLocations { get; set; } = new Dictionary<SpawnLocationType, int>
        {
            { SpawnLocationType.Inside079Secondary, 100 },
            { SpawnLocationType.InsideHidChamber, 80 }
        };
    }
}