using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using UnityEngine;

namespace ScatterGun
{
    
    [CustomItem(ItemType.GunShotgun)]
    public class ScatterGun : CustomWeapon
    {
        public override ItemType Type { get; set; } = ItemType.GunShotgun;
        public override float Damage { get; set; } = 20f;
        public override byte ClipSize { get; set; } = 24;
        public override bool FriendlyFire { get; set; } = true;
        public override uint Id { get; set; } = 102;
        public override string Name { get; set; } = "ScatterGun";
        public override string Description { get; set; } = null;
        public override float Weight { get; set; } = 1f;
        public override Vector3 Scale { get; set; } = new Vector3(1.5f, 1.5f, 1.5f);
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            DynamicSpawnPoints = new List<DynamicSpawnPoint>()
            {
                new DynamicSpawnPoint()
                {
                        Location = Exiled.API.Enums.SpawnLocationType.Inside079Secondary,
                        Chance = 100
                }
            }
        };
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shot += OnShot;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shot -= OnShot;
            base.UnsubscribeEvents();
        }
        protected override void OnShot(ShotEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
                return;
        }
    }
}
