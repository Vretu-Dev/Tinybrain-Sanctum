using HarmonyLib;
using System;

namespace NewUpdateFixes.EventHandlers
{
    public static class JailbirdHandler
    {
        public static Harmony Harmony { get; private set; }
        public static string HarmonyName { get; private set; }
        public static void RegisterEvents()
        {
            HarmonyName = $"com-half.uh-{DateTime.UtcNow.Ticks}";
            Harmony = new Harmony(HarmonyName);
            Harmony.PatchAll();
        }
        public static void UnregisterEvents()
        {
            Harmony.UnpatchAll(HarmonyName);
        }
    }
}