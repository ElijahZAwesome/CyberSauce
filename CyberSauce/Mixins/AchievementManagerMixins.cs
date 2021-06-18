using HarmonyLib;

namespace CyberSauce.Mixins
{
    [HarmonyPatch(typeof(AchievementManager), "CheckAchievements")]
    internal class CheckAchievementsMixin
    {
        internal static bool Prefix() => false;
    }
    
    [HarmonyPatch(typeof(AchievementManager), "Init")]
    internal class AchievementManagerInit
    {
        internal static bool Prefix()
        {
            AchievementManager.Uploader = new AchievementUploader_None();
            return false;
        }
    }
}