using HarmonyLib;

namespace CyberSauce.Mixins
{
    [HarmonyPatch(typeof(GameSparkManager), "Init")]
    internal class InitMixin
    {
        internal static bool Prefix() => false;
    }
    
    // None of these are needed but i'm gonna leave them in for the sake of bulletproofing
    // In case the game ever runs these things elsewhere
    [HarmonyPatch(typeof(GameSparkManager), nameof(GameSparkManager.TestInternetCo))]
    internal class TestInternetCoMixin
    {
        internal static bool Prefix() => false;
    }
    
    [HarmonyPatch(typeof(GameSparkManager), "CheckForInternetConnectionCoroutine")]
    internal class CheckForInternetConnectionCoroutineMixin
    {
        internal static bool Prefix() => false;
    }
    
    [HarmonyPatch(typeof(GameSparkManager), "TryAuthentification")]
    internal class TryAuthentificationMixin
    {
        internal static bool Prefix() => false;
    }
}