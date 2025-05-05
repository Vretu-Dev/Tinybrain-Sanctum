using Exiled.API.Features;
using System.Timers;
using System;

namespace AutoSR
{
    public class AutoSR : Plugin<Config>
    {
        public override string Name => "AutoSR";
        public override string Author => "Vretu";
        public override string Prefix { get; } = "AutoSR";
        public override Version Version => new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(9, 0, 0);

        private Timer restartTimer;
        private Timer broadcastTimer;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;

            if (restartTimer != null)
            {
                restartTimer.Stop();
                restartTimer.Dispose();
            }
            if (broadcastTimer != null)
            {
                broadcastTimer.Stop();
                broadcastTimer.Dispose();
            }
            base.OnDisabled();
        }
        private void OnRoundStarted()
        {
            restartTimer = new Timer(Config.SecondsUntilRestart * 1000);
            restartTimer.Elapsed += OnTimerElapsed;
            restartTimer.AutoReset = false;
            restartTimer.Start();

            if (Config.SecondsUntilRestart > 30)
            {
                broadcastTimer = new Timer((Config.SecondsUntilRestart - 30) * 1000);
                broadcastTimer.Elapsed += OnBroadcastElapsed;
                broadcastTimer.AutoReset = false;
                broadcastTimer.Start();
            }
            Log.Info($"Timer set for {Config.SecondsUntilRestart} seconds to restart the round.");
        }
        private void OnBroadcastElapsed(object sender, ElapsedEventArgs e)
        {
            broadcastTimer.Stop();
            broadcastTimer.Dispose();
            Log.Info("Server will be restarted within 30 seconds.");
            Map.Broadcast(10, Config.BroadcastMessage, Broadcast.BroadcastFlags.Normal, true);
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            restartTimer.Stop();
            restartTimer.Dispose();
            Log.Info("Restarting round.");
            Server.ExecuteCommand(Config.Command);
        }
    }
}