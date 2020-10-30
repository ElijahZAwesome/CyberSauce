using HarmonyLib;

namespace CyberSauce.Protection
{
    [HarmonyPatch(typeof(Leaderboard), "LoadLeaderboardFromShortcode")]
    internal class Leaderboard_LoadLeaderboardFromShortcode
    {
        [HarmonyPrefix]
        internal static bool Prefix() => false;
    }
}