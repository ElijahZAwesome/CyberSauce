using HarmonyLib;
using UnityEngine;

namespace CyberSauce
{
    public class SpeedHack
    {
        [HarmonyPatch(typeof(PlayerController), "Init")]
        internal class PlayerController_Init
        {
            [HarmonyPostfix]
            internal static void Postfix(PlayerController __instance)
            {
                __instance.PlayerData.MaxHookJumpForwardSpeed = float.MaxValue;
                __instance.PlayerData.MaxHookJumpYSpeed = float.MaxValue;
                __instance.PlayerData.GlobalMaxSpeed = float.MaxValue;
                //__instance.PlayerData.Drag = Vector2.zero;
                __instance.PlayerData.HookJumpSpeed_Forward = float.MaxValue;
                __instance.PlayerData.HookJumpSpeed = float.MaxValue;
            }
        }
        
        [HarmonyPatch(typeof(PlayerController), "Update")]
        internal class PlayerController_Update
        {
            [HarmonyPostfix]
            internal static void Postfix(PlayerController __instance)
            {
            }
        }
    }
}