﻿using UnityEngine;

#if BEPINEX
using BepInEx;
using UnityEngine.SceneManagement;

#elif MELONLOADER
using MelonLoader;

[assembly: MelonInfo(typeof(PeakSweeper.Broom), "Peak Sweeper", PluginInfo.PLUGIN_VERSION, "Kalico")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

#endif

namespace PeakSweeper
{
#if BEPINEX
    [BepInPlugin("com.github.Elvonia.PeakSweeper", "Peak Sweeper", PluginInfo.PLUGIN_VERSION)]
    public class Broom : BaseUnityPlugin
    {
        public void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CommonSceneLoad(scene.buildIndex, scene.name);
        }

        public void Log(string message)
        {
            Logger.LogInfo(message);
        }

#elif MELONLOADER
    public class Broom : MelonMod
    {
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            CommonSceneLoad(buildIndex, sceneName);
        }

        public void Log(string message)
        {
            MelonLogger.Msg(message);
        }

#endif

        public void CommonSceneLoad(int buildIndex, string sceneName)
        {
            if (buildIndex == 0 || buildIndex == 1 || buildIndex == 37)
            {
                return;
            }
            Log($"Scene loaded: {sceneName}");
            CleanSweep();
        }

        private void CleanSweep()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.layer == 22 || obj.tag == "Snow")
                {
                    if (obj.name.Contains("Snow") || obj.name.Contains("ShrubberyObstacle"))
                    {
                        Log($"Disabled: {obj.name}");
                        obj.SetActive(false);
                    }
                }
            }
        }
    }
}
