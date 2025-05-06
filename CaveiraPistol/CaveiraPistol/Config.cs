using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace CaveiraPistol
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Notifies when rampage is activated.")]
        public bool Hint { get; set; } = true;
        public float HintDuration { get; set; } = 4f;
        public float Damage { get; set; } = 40f;
        public float RampageDamageMultiplier { get; set; } = 2f;
        public float RampageDuration { get; set; } = 10f;
        public float RampageWindowActivation { get; set; } = 30f;
        [Description("Spawn locations with their respective chances. Format: Location: Chance")]
        public Dictionary<SpawnLocationType, int> SpawnLocations { get; set; } = new Dictionary<SpawnLocationType, int>
        {
            { SpawnLocationType.Inside079Secondary, 100 },
            { SpawnLocationType.InsideHidChamber, 80 }
        };
        [Description("Should rampage be active when you affected by:")]
        public bool Scp207 { get; set; } = false;
        public bool Scp1853 { get; set; } = false;
        public bool Antiscp207 { get; set; } = false;
        [Description("Translations:")]
        public string RampageActivated { get; set; } = "Rampage mode active!";
        public string WindowTimeActive { get; set; } = "Press key to activate Rampage!";
        public string WindowTimeExpired { get; set; } = "The activation time expired.";
    }
}