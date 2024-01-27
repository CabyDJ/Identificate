using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using IdentificateIdentificate.Managers;
using UnityEngine;

namespace Identificate
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "CabyDJ.IdentificateIdentificate";
        private const string modName = "Identificate Identificate";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static Plugin instance;
        internal static ManualLogSource mls;

        public GameObject netManagerPrefab;

        internal static AudioClip[] SoundFX;
        internal static AssetBundle Bundle;

        void Awake()
        {
            instance = this;

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("(º)< Waking up identificate mod");

            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }
            }

            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "netcodemod");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

            if (bundle != null)
            {
                netManagerPrefab = bundle.LoadAsset<GameObject>("Assets/Netcode/NetworkManagerIdentificate.prefab");
                netManagerPrefab.AddComponent<NetworkManagerIdentificate>();
                mls.LogInfo("(º)< Network bundle loaded");
            }
            else
            {
                mls.LogInfo("(º`)< Error loading NetworkBundle");
            }

            SoundFX = new AudioClip[1];
            string FolderLocation = instance.Info.Location;
            FolderLocation = FolderLocation.TrimEnd("IdentificateIdentificateBase.dll".ToCharArray());
            Bundle = AssetBundle.LoadFromFile(FolderLocation + "identificate");

            if (Bundle != null)
            {
                SoundFX = Bundle.LoadAllAssets<AudioClip>();
                mls.LogInfo("(º)< Sound asset bundle loaded");
            }
            else
            {
                mls.LogInfo("(º`)< Error loading Sound asset bundle");
            }

            harmony.PatchAll();
            mls.LogInfo("(º)< Patched Identificate network");
        }
    }
}
