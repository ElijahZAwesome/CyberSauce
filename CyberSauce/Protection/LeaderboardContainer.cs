using HarmonyLib;

namespace CyberSauce.Protection
{
    [HarmonyPatch(typeof(LeaderboardContainer), "HighScoreListener")]
    internal class LeaderboardContainer_HighScoreListener
    {
        [HarmonyPrefix]
        internal static bool Prefix() => false;
    }
}