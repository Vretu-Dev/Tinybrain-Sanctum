using Exiled.API.Interfaces;
using System.ComponentModel;

namespace InventoryReset
{
    public class Config : IConfig
    {
        [Description("Whether the plugin is enable.")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
