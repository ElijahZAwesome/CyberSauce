using System.Reflection;
using HarmonyLib;

namespace CyberSauce.Mixins
{
    [HarmonyPatch(typeof(OptionPanel_Dynamic), "ScanGameData")]
    public class ScanGameDataMixin
    {
        internal static void Postfix(OptionPanel_Dynamic __instance)
        {
            var props = typeof(SauceData).GetFields();
            var createField =
                typeof(OptionPanel_Dynamic).GetMethod("CreateField", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var foundAttributes = prop.GetCustomAttributes<OptionDynamicAttribute>() as OptionDynamicAttribute[];

                foreach (var attr in foundAttributes)
                {
                    if (attr != null)
                    {
                        if (OptionManager.CheckConsoleIsValid(attr.ConsoleDependency))
                            createField?.Invoke(__instance, new object[] {prop, attr});
                    }
                }
            }
        }
    }
}