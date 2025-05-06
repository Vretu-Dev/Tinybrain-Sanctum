using Exiled.API.Interfaces;
using System.ComponentModel;

namespace AdrenalinRush
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Duration time of effect in seconds.")]
        public int BoostDuration { get; set; } = 4;
        [Description("It's effect Intensity. Same as Remote Admin.")]
        public byte BoostIntensity { get; set; } = 30;
        [Description("Adjust how fast the boost grows. It's BoostIntensity / SpeedIncrease. For example [30 Intensity / 4s Duration = 7,5 IntensityBoost per second].")]
        public int SpeedIncrease { get; set; } = 4;
        [Description("Number of steps after speed is refreshed. Higher value = refreshing more frequently.")]
        public int Steps { get; set; } = 10;
    }
}