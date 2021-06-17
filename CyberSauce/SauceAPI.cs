using System;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using UnityEngine;

namespace CyberSauce
{
    [GameSupportLibraryEntryPoint("com.elijahzawesome.cybersauce", AwakeAfterInitialize = true)]
    internal sealed class SauceAPI : MonoBehaviour
    {
        private Log Log => LogManager.GetForCurrentAssembly();

        public void Initialize(IManager manager)
        {
            Log.Warning("We got da sauce");
            
            try
            {
                RuntimePatcher.AutoPatch();
            }
            catch (Exception e)
            {
                Log.Error("Failed to initialize mix-ins. Mods will still be loaded, but may not function correctly.");
                Log.Exception(e);
            }

            try
            {
                RuntimePatcher.RunTranspilers();
            }
            catch (Exception e)
            {
                Log.Error("Failed to initialize one or more transpilers. Mods will still be loaded, but may not function correctly.");
                Log.Exception(e);
            }
        }
    }
}