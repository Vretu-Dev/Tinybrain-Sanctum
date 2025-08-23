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
                var room = Room.Get(roomLight.RoomName);
                if (room == null)
                {
                    Log.Debug($"Room '{roomLight.RoomName}' not found.");
                    continue;
                }
                room.Color = new Color(roomLight.R / 255f, roomLight.G / 255f, roomLight.B / 255f, roomLight.Intensity);
                Log.Debug($"Set lights in room '{roomLight.RoomName}' to RGB({roomLight.R}, {roomLight.G}, {roomLight.B}) with intensity {roomLight.Intensity}.");
            }
        }
    }
}