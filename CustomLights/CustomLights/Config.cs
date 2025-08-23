using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace CustomLights
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public List<RoomLightConfig> RoomLights { get; set; } = new List<RoomLightConfig>
        {
            new RoomLightConfig
            {
                RoomName = RoomType.EzCafeteria,
                R = 255,
                G = 255,
                B = 255,
                Intensity = 1f
            },
        };
    }

    public class RoomLightConfig
    {
        [Description("The name of the room.")]
        public RoomType RoomName { get; set; }

        [Description("Red value (0-255).")]
        public byte R { get; set; }

        [Description("Green value (0-255).")]
        public byte G { get; set; }

        [Description("Blue value (0-255).")]
        public byte B { get; set; }

        [Description("Light intensity multiplier (0-1).")]
        public float Intensity { get; set; }
    }
}