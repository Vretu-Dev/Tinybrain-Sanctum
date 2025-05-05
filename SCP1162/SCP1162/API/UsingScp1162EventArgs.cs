using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Interfaces;

namespace SCP1162.API
{
    public class UsingScp1162EventArgs : IExiledEvent
    {
        public UsingScp1162EventArgs(Player player, ItemType itemafter, ItemType itembefore)
        {
            Player = player;
            ItemAfter = itemafter;
            ItemBefore = itembefore;
        }
        
        public Player Player { get; }
        public ItemType ItemAfter { get; set; }
        public ItemType ItemBefore { get; }
    }
}