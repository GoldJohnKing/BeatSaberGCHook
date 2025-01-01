using IPA;
using UnityEngine.Scripting;
using IPALogger = IPA.Logging.Logger;

namespace GCHook
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
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
            Log.Info("Plugin initialized.");
        }

        [OnStart]
        public void OnApplicationStart()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
            Log.Info("OnApplicationStart: GC is disabled.");

            GarbageCollector.GCModeChanged += OnGCModeChanged;
            Log.Info("OnApplicationStart: GCModeChanged callback Injected.");
        }

        public void OnGCModeChanged(GarbageCollector.Mode mode)
        {
            if (GarbageCollector.GCMode != GarbageCollector.Mode.Disabled)
            {
                GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
                Log.Info("OnGCModeChanged: GC is enabled, forcefully disable it.");
            }
        }
    }
}
