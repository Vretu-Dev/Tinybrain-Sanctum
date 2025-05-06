using Exiled.API.Interfaces;
using System.ComponentModel;

namespace EasyWhitelist
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enable.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Should whitelist be enabled at server startup")]
        public bool EnableOnStartup { get; set; } = false;
        [Description("Message sent to player when kicked")]
        public string KickMessage { get; set; } = "You are not on whitelist!";
    }
}
