﻿using MelonLoader;
using UnityEngine;

namespace PeakSweeper
{
    public class Broom : MelonMod
    {
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (buildIndex == 0 || buildIndex == 1 || buildIndex == 37)
            {
                return;
            }
            MelonLogger.Msg($"Scene loaded: {sceneName}");
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
                        MelonLogger.Msg($"Disabled: {obj.name}");
                        obj.SetActive(false);
                    }
                }
            }
        }
    }
}
