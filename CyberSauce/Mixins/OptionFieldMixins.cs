using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace CyberSauce.Mixins
{
    [HarmonyPatch]
    public class OptionFieldInitMixin
    {
        [HarmonyPatch(typeof(OptionFieldBase), "Init")]
        internal static bool Prefix(OptionFieldBase __instance, MethodInfo ___OnOptionFieldUpdated,
            OptionSubPanel parentPanel, FieldInfo fieldToUpdate, OptionDynamicAttribute attribute)
        {
            __instance.TargetFieldInfo = fieldToUpdate;
            __instance.AttributeInfo = attribute;
            __instance.ParentSubPanel = parentPanel;

            __instance.FieldName = GetFieldName();
            __instance.TitleStringParser.ForceText(GetTitleString());
            __instance.TitleStringParser.SetLocalisationKey(GetTitleString());

            if (!parentPanel.ChildOptionFields.Contains(__instance))
                parentPanel.ChildOptionFields.Add(__instance);
            if (__instance.TargetFieldInfo != null)
            {
                var callbackInfo =
                    __instance.TargetFieldInfo.GetCustomAttribute<OptionCallbackAttribute>();
                if (callbackInfo != null)
                {
                    ___OnOptionFieldUpdated =
                        __instance.TargetFieldInfo.GetType()
                            .GetMethod(callbackInfo.TargetCallback, new[] {typeof(FieldInfo)});
                    if (___OnOptionFieldUpdated == null)
                        Debug.LogErrorFormat(__instance,
                            "Found OptionCallbackAttribute for field {0} but couldn't manage to find target method \"{1}\"",
                            __instance.TargetFieldInfo.Name, callbackInfo.TargetCallback);
                }
            }

            __instance.OwnSelectable.onClick.AddListener(SelectTargetSelectable);
            __instance.UpdateDisplayedValue();
            return false;
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(OptionFieldBase), "GetFieldName")]
        internal static string GetFieldName()
        {
            throw new NotImplementedException("stubbed");
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(OptionFieldBase), "GetTitleString")]
        internal static string GetTitleString()
        {
            throw new NotImplementedException("stubbed");
        }

        [HarmonyReversePatch]
        [HarmonyPatch(typeof(OptionFieldBase), "SelectTargetSelectable")]
        internal static void SelectTargetSelectable()
        {
            throw new NotImplementedException("stubbed");
        }
    }
}