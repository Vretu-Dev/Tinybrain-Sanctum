using System.Collections.Generic;
using Exiled.API.Interfaces;
using Exiled.API.Enums;

namespace ClearItems
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public bool LogMessage { get; set; } = true;
        public float ClearInterval { get; set; } = 10f;
        public List<ItemType> ClearedItems { get; set; } = new List<ItemType>
        {
            ItemType.Medkit,
            ItemType.Adrenaline,
            ItemType.Painkillers,
            ItemType.ArmorCombat,
            ItemType.ArmorHeavy,
            ItemType.ArmorLight,
            ItemType.GunCOM15,
            ItemType.GunCOM18
        };
    }
}