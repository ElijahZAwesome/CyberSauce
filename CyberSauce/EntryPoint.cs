using System;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using Reactor.API.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CyberSauce
{
    [GameSupportLibraryEntryPoint(CyberSauceNamespace)]
    public sealed class EntryPoint : MonoBehaviour
    {
        internal const string CyberSauceNamespace = "com.github.elijahzawesome/CyberSauce";

        public static Log Log = LogManager.GetForCurrentAssembly();
        public static Gradient RainbowGradient;
        public static AssetBundle Assets;

        private FileSystem _fs;
        private Dumper _dumper;

        public void Initialize(IManager manager)
        {
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;

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
                Log.Error(
                    "Failed to initialize one or more transpilers. Mods will still be loaded, but may not function correctly.");
                Log.Exception(e);
            }

            _fs = new FileSystem();
            _dumper = new Dumper(_fs);

            RainbowGradient = new Gradient();
            int nbOfColors = 8;
            var keys = new GradientColorKey[nbOfColors];
            for (int i = 0; i < nbOfColors; i++)
                keys[i] = new GradientColorKey(Color.HSVToRGB(i / (float) nbOfColors, 1, 1), i / (float) nbOfColors);
            RainbowGradient.SetKeys(keys, RainbowGradient.alphaKeys);
        }

        public void OnGUI()
        {
            var style = new GUIStyle()
            {
                fontSize = 24,
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }
            };
            //GUI.Label(new Rect(100, 30, 100, 20), "CyberSauce", style);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                _dumper.DumpCurrentScene(false);
            if (Input.GetKeyDown(KeyCode.LeftBracket))
                _dumper.DumpCurrentScene(true);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (Assets == null)
                Assets = new Assets("cybersauce").Bundle as AssetBundle;

            if (scene.buildIndex != 1) // The loading screen scene i think?
                return;

            var internetDetector = GameObject.Find("InternetDetector").GetComponent<InternetConditionnal>();
            internetDetector.OnConnectionLost.AddListener(() => { InternetWarn(internetDetector.gameObject); });
        }

        private void InternetWarn(GameObject internetDetector)
        {
            // Change no internet icon
            var wifiLostSprite = GameObject.Find("No Internet Icon");

            var icon = Assets.LoadAsset<Sprite>("TerribleIcon");
            wifiLostSprite.GetComponent<Image>().sprite = icon;

            // Change no Internet text
            var mask = internetDetector.transform.GetChild(0).GetChild(0).GetChild(0); // Mask child of ButtonHit
            var header = mask.Find("Text");
            header.GetComponent<TextMeshProUGUI>().text
                = "You are running CyberSauce";
            header.GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
            var sub = mask.Find("Goto options");
            sub.GetComponent<TextMeshProUGUI>().text
                = "Internet capabilities are disabled";
        }
    }
}