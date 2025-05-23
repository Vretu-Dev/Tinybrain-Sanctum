using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace CustomACC
{
    public class FirearmInaccuracySettings
    {
        public float? Inaccuracy { get; set; } = null;
        public float? AimingInaccuracy { get; set; } = null;
    }
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public Dictionary<string, FirearmInaccuracySettings> Weapons { get; set; } = new Dictionary<string, FirearmInaccuracySettings>()
        {
            ["Logicer"] = new FirearmInaccuracySettings
            {
                Inaccuracy = 40f,
                AimingInaccuracy = 0.01f,
            },
            ["AK"] = new FirearmInaccuracySettings
            {
                Inaccuracy = 40f,
                AimingInaccuracy = 0.01f,
            },
        };
    }
}