using System.Collections;
using HarmonyLib;

namespace CyberSauce.Protection
{
    [HarmonyPatch(typeof(GameSparkManager), "TestInternetCo")]
    internal class GameSparkManager_TestInternetCo
    {
        [HarmonyPrefix]
        internal static bool Prefix() => false;
    }
    
    [HarmonyPatch(typeof(GameSparkManager), "CheckForInternetConnectionCoroutine")]
    internal class GameSparkManager_CheckForInternetConnectionCoroutine
    {
        [HarmonyPrefix]
        internal static bool Prefix() => false;
    }
    
    [HarmonyPatch(typeof(GameSparkManager), "TryAuthentification")]
    internal class GameSparkManager_TryAuthentification
    {
        [HarmonyPrefix]
        internal static bool Prefix() => false;
    }
}