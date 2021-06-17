using HarmonyLib;

namespace CyberSauce.Mixins
{
    [HarmonyPatch(typeof(GameSparkManager), nameof(GameSparkManager.TestInternetCo))]
    internal class CheckInternetMixin
    {
        public static void Postfix(ref bool __result)
        {
            __result = false;
        }
    }
}