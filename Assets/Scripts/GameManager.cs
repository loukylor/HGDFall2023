using UnityEditor;
using UnityEngine.SceneManagement;

namespace HGDFall2023
{
    public static class GameManager
    {
        public static void LoadLevel(int level)
        {
            LoadLevel($"Level{level}");
        }

        public static void LoadLevel(string level)
        {
            SceneManager.LoadScene(level);
        }

        [MenuItem("Debug/Reset Level")]
        public static void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}