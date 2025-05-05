using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Enums;
using MEC;
using UnityEngine;
using Exiled.API.Features.Pickups;
using Exiled.CreditTags;

namespace ClearItems
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "Vretu";
        public override string Name => "ClearItems";
        public override string Prefix => "ClearItems";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        private CoroutineHandle clearCoroutine;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWaitingForPlayers;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnWaitingForPlayers;
            base.OnDisabled();
        }

        private void OnRoundStarted()
        {
            clearCoroutine = Timing.RunCoroutine(ClearItemsCoroutine());
        }

        private void OnWaitingForPlayers()
        {
            KillCoroutine();
        }

        private void KillCoroutine()
        {
            if (clearCoroutine.IsRunning)
                Timing.KillCoroutines(clearCoroutine);
        }

        private IEnumerator<float> ClearItemsCoroutine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(Config.ClearInterval);

                int removed = 0;
                foreach (Pickup pickup in Pickup.List)
                {
                    if (Config.ClearedItems.Contains(pickup.Type))
                    {
                        pickup.Destroy();
                        removed++;
                    }
                }
                if (Config.LogMessage) Log.Warn($"Removed {removed} items from the ground");
            }
        }
    }
}