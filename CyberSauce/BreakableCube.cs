using System.IO;
using CyberSauce.Helpers;
using HarmonyLib;
using UnityEngine;

namespace CyberSauce
{
    [HarmonyPatch(typeof(BreakableCube), "Awake")]
    internal class BreakableCube_Awake
    {
        [HarmonyPostfix]
        internal static void Postfix(BreakableCube __instance, ref Renderer ____targetRenderer, ref MaterialPropertyBlock ____matPropertyBlock)
        {
            var particles = __instance.ExplosionParticle.main;
            particles.startColor = Color.red;
            ____targetRenderer.GetPropertyBlock(____matPropertyBlock);
            ____matPropertyBlock.SetColor("_EmissiveColor", Color.red);
            ____matPropertyBlock.SetColor("_DepthColor", Color.red);
            ____targetRenderer.SetPropertyBlock(____matPropertyBlock);
        }
    }
    
    [HarmonyPatch(typeof(BreakableCube), "Update")]
    internal class BreakableCube_Update
    {
        [HarmonyPostfix]
        internal static void Postfix(BreakableCube __instance, ref Renderer ____targetRenderer, ref MaterialPropertyBlock ____matPropertyBlock)
        {
            var color = EntryPoint.RainbowGradient.Evaluate(Mathf.PingPong(Time.time, 1));
            //var color = HSBColor.ToColor(new HSBColor(Mathf.PingPong(Time.time, 1), 1, 1));
            ____targetRenderer.GetPropertyBlock(____matPropertyBlock);
            ____matPropertyBlock.SetColor("_EmissiveColor", color);
            ____matPropertyBlock.SetColor("_DepthColor", color + new Color(20, 20, 20));
            ____targetRenderer.SetPropertyBlock(____matPropertyBlock);
        }
    }
    
    [HarmonyPatch(typeof(BreakableCube), "CubeExplode")]
    internal class BreakableCube_CubeExplode
    {
        [HarmonyPostfix]
        internal static void Postfix(BreakableCube __instance)
        {
            //var color = EntryPoint.RainbowGradient.Evaluate(Mathf.PingPong(Time.time, 1));
            var cul = __instance.ExplosionParticle.colorOverLifetime;
            cul.enabled = true;
            cul.color = EntryPoint.RainbowGradient;
            //__instance.ExplosionParticle.time = 0;
            //__instance.ExplosionParticle.time = Mathf.PingPong(Time.time, 1);
        }
    }
}