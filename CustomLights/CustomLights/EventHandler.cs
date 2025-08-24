using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using UnityEngine;

namespace CustomLights
{
    public class EventHandler
    {
        public Plugin Plugin;
        public EventHandler(Plugin plugin) => Plugin = plugin;
        public void OnRoundStarted()
        {
            foreach (var roomLight in Plugin.Config.RoomLights)
            {
                List<Room> matchingRooms = Room.List.Where(r => r.Type == roomLight.RoomName).ToList();

                if (matchingRooms.Count == 0)
                {
                    Log.Debug($"No rooms of type '{roomLight.RoomName}' found.");
                    continue;
                }
                foreach (var room in matchingRooms)
                {
                    room.Color = new Color(roomLight.R / 255f, roomLight.G / 255f, roomLight.B / 255f, roomLight.Intensity);
                }

                Log.Debug($"Set lights in {matchingRooms.Count} rooms of type '{roomLight.RoomName}' to RGB({roomLight.R}, {roomLight.G}, {roomLight.B}) with intensity {roomLight.Intensity}.");
            }
        }
    }
}