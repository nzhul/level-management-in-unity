using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 1;

        public static void LoadLevel(string levelname)
        {
            if (Application.CanStreamedLevelBeLoaded(levelname))
            {
                SceneManager.LoadScene(levelname);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER LoadLevel Error: invalid scene specified!");
            }
        }

        public static void LoadLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                if (levelIndex == mainMenuIndex)
                {
                    MainMenu.Open();
                }

                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER LoadLevel Error: invalid scene specified!");
            }
        }

        public static void ReloadLevel()
        {
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            LoadLevel(currentLevelIndex);
        }

        public static void LoadNextLevel()
        {
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            int nextLevelIndex = currentLevelIndex + 1;
            int totalSceneCount = SceneManager.sceneCountInBuildSettings;

            if (nextLevelIndex == totalSceneCount)
            {
                nextLevelIndex = mainMenuIndex;
            }

            LoadLevel(nextLevelIndex);

        }

        public static void LoadMainMenuLevel()
        {
            LoadLevel(mainMenuIndex);
        }
    }
}
