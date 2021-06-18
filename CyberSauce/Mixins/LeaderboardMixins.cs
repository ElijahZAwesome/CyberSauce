using HarmonyLib;

namespace CyberSauce.Mixins
{
    [HarmonyPatch(typeof(Leaderboard), "LoadLeaderboardFromShortcode")]
    internal class Leaderboard_LoadLeaderboardFromShortcode
    {
        internal static bool Prefix() => false;
    }
}