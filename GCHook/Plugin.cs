using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using IPALogger = IPA.Logging.Logger;

namespace GCHook
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        private bool isInGameCore = false;
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("GCHook initialized.");
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            //Log.Debug("OnApplicationStart");
            //new GameObject("GCHookController").AddComponent<GCHookController>();

            GarbageCollector.GCModeChanged += GCModeChanged;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;

            Log.Info("GCHook Plugin Injected.");
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            //Log.Debug("OnApplicationQuit");
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            //if (nextScene.name == "MenuViewControllers")
            //{
            //    isInGameCore = false;
            //    GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;

            //    Log.Info("Changing from GameCore to MenuViewControllers, Enabled GC.");
            //}
            if (nextScene.name == "GameCore") // GC will be automatically enabled when exit GameCore
            {
                isInGameCore = true;
                GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;

                Log.Info("Game Start, Disable GC.");
            }
        }

        public void GCModeChanged(GarbageCollector.Mode mode)
        {
            if (/*isInGameCore && */mode != GarbageCollector.Mode.Disabled)
            {
                GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
                Log.Info("External source has enabled GC during GameCore, Disable GC.");
            }
            else
            {
                Log.Info("External source has disabled GC during GameCore");
            }
        }
    }
}
