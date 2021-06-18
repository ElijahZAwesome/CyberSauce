using HarmonyLib;

namespace CyberSauce.Mixins
{
    [HarmonyPatch(typeof(LeaderboardContainer), "HighScoreListener")]
    internal class LeaderboardContainer_HighScoreListener
    {
        internal static bool Prefix() => false;
    }
}