using System;
using System.Reflection;
using CyberSauce.Debugging;
using Reactor.API.Attributes;
using Reactor.API.Configuration;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using Reactor.API.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace CyberSauce
{
    [GameSupportLibraryEntryPoint("com.elijahzawesome.cybersauce", AwakeAfterInitialize = true)]
    internal sealed class SauceAPI : MonoBehaviour
    {
        internal static Log Log => LogManager.GetForCurrentAssembly();
        
        private Settings _settings;
        private FileSystem _fileSystem;
        private Dumper _dumper;

        public void Initialize(IManager manager)
        {
            Log.Warning("We got da sawce");
            
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

            _settings = new Settings("config");
            _fileSystem = new FileSystem();
            _dumper = new Dumper(_fileSystem);
            SetUpKeyBinds(manager.Hotkeys);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == -1 && scene.isSubScene) // Pause Menu Scene
            {
                LoadDisclaimer();
                LoadSettingsMenu();
            }
        }

        private void SetUpKeyBinds(IHotkeyManager hotkeys)
        {
            hotkeys.Bind("F7", () => _dumper.DumpCurrentScene(false));
            hotkeys.Bind("F8", () => _dumper.DumpCurrentScene(true));
            hotkeys.Bind("F9", LevelImporter.LoadLevelFromBundle);
        }

        private void LoadDisclaimer()
        {
            var internetDetector = GameObject.Find("InternetDetector");
            // Change no internet icon
            //var wifiLostSprite = GameObject.Find("No Internet Icon");

            //var icon = Assets.LoadAsset<Sprite>("TerribleIcon");
            //wifiLostSprite.GetComponent<Image>().image = icon;

            // Change no Internet text
            var mask = internetDetector.transform.GetChild(0).GetChild(0).GetChild(0); // Mask child of ButtonHit
            var header = mask.Find("Text");
            header.GetComponent<TextMeshProUGUI>().text = "You are running CyberSauce";
            var sub = mask.Find("Goto options");
            sub.GetComponent<TextMeshProUGUI>().text = "Internet capabilities are disabled";
        }

        private void LoadSettingsMenu()
        {
            
        }
    }
}