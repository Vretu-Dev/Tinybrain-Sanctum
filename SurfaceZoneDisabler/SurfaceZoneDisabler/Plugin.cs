using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using UnityEngine;
using Random = UnityEngine.Random;
using MEC;

namespace SurfaceZoneDisabler
{ 
    public class SurfaceZoneDisablerPlugin : Plugin<Config>
    {
        public override string Author => "Vretu";
        public override string Name => "SurfaceZoneDisabler";
        public override string Prefix => "SurfaceZoneDisabler";
        public override Version Version => new Version(1, 0, 0);

        private CoroutineHandle escapeCoroutine;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.InteractingElevator += OnInteractingElevator;
            Exiled.Events.Handlers.Server.RespawnedTeam += OnRespawnedTeam;
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.InteractingElevator -= OnInteractingElevator;
            Exiled.Events.Handlers.Server.RespawnedTeam -= OnRespawnedTeam;
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnded;
            Timing.KillCoroutines(escapeCoroutine);
            base.OnDisabled();
        }

        private void OnRoundStarted()
        {
            escapeCoroutine = Timing.RunCoroutine(EscapeChecker());
        }

        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(escapeCoroutine);
        }

        private void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (ev.Lift.Type == ElevatorType.GateA || ev.Lift.Type == ElevatorType.GateB)
            {
                ev.IsAllowed = false;
            }
        }

        private static Vector3 GetRandomPositionInRoom(Room room, float radius = 2f, float yOffset = 2f)
        {
            var basePos = room.Position;

            float offsetX = Random.Range(-radius, radius);
            float offsetZ = Random.Range(-radius, radius);
            return new Vector3(basePos.x + offsetX, basePos.y + yOffset, basePos.z + offsetZ);
        }

        private void OnRespawnedTeam(RespawnedTeamEventArgs ev)
        {
            var mtfRoom = Room.Get(RoomType.EzGateB);
            var ciRoom = Room.Get(RoomType.EzGateA);

            foreach (var player in ev.Players)
            {
                if (player.Role.Team == Team.FoundationForces && mtfRoom != null)
                {
                    player.Position = GetRandomPositionInRoom(mtfRoom);
                }
                else if (player.Role.Team == Team.ChaosInsurgency && ciRoom != null)
                {
                    player.Position = GetRandomPositionInRoom(ciRoom);
                }
            }
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            var team = ev.NewRole.GetTeam();
            if (team == Team.FoundationForces || team == Team.ChaosInsurgency)
            {
                ev.SpawnFlags = RoleSpawnFlags.AssignInventory;

                Timing.CallDelayed(0.1f, () =>
                {
                    Room room = null;

                    if (team == Team.FoundationForces)
                        room = Room.Get(RoomType.EzGateB);
                    else if (team == Team.ChaosInsurgency)
                        room = Room.Get(RoomType.EzGateA);

                    if (room != null)
                        ev.Player.Position = GetRandomPositionInRoom(room);
                });
            }
        }

        private IEnumerator<float> EscapeChecker()
        {
            while (true)
            {
                foreach (var player in Player.List)
                {
                    if (!player.IsAlive) continue;

                    if (player.Role.Type == RoleTypeId.Scientist || player.Role.Type == RoleTypeId.ClassD)
                    {
                        var roomType = player.CurrentRoom?.Type ?? RoomType.Unknown;
                        if (roomType == RoomType.EzGateA || roomType == RoomType.EzGateB)
                        {
                            if (player.Role.Type == RoleTypeId.Scientist)
                            {
                                if (player.IsCuffed)
                                    player.Role.Set(RoleTypeId.ChaosRifleman, SpawnReason.Escaped, RoleSpawnFlags.AssignInventory);
                                else
                                    player.Role.Set(RoleTypeId.NtfSpecialist, SpawnReason.Escaped, RoleSpawnFlags.AssignInventory);
                            }
                            else if (player.Role.Type == RoleTypeId.ClassD)
                            {
                                if (player.IsCuffed)
                                    player.Role.Set(RoleTypeId.NtfSpecialist, SpawnReason.Escaped, RoleSpawnFlags.AssignInventory);
                                else
                                    player.Role.Set(RoleTypeId.ChaosRifleman, SpawnReason.Escaped, RoleSpawnFlags.AssignInventory);
                            }
                        }
                    }
                }
                yield return Timing.WaitForSeconds(2f);
            }
        }
    }
}