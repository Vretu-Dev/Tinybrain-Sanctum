using Exiled.API.Interfaces;
using System.ComponentModel;

namespace AutoSR
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Time after server will be restarted in seconds")]
        public int SecondsUntilRestart { get; set; } = 1800;
        [Description("Command to be executed eg: 'sofrestart','restart','roundrestart'.")]
        public string Command { get; set; } = "softrestart";
        [Description("Broadcast to be displayed.")]
        public string BroadcastMessage { get; set; } = "Server will be restarted within 30 seconds";
    }
}