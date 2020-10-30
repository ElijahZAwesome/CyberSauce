using HarmonyLib;

namespace CyberSauce.Protection
{
    [HarmonyPatch(typeof(AchievementManager), "CheckAchievements")]
    internal class AchievementManager_CheckAchievements
    {
        [HarmonyPrefix]
        internal static bool Prefix() => false;
    }
    
    [HarmonyPatch(typeof(AchievementManager), "Init")]
    internal class AchievementManager_Init
    {
        [HarmonyPrefix]
        internal static bool Prefix(AchievementManager __instance)
        {
            AchievementManager.Uploader = new AchievementUploader_None();
            return false;
        }
    }
}