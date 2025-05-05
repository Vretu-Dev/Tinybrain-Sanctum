using Exiled.API.Interfaces;
using System.ComponentModel;

namespace FakeO5Card
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}