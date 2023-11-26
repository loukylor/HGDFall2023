using UnityEngine;
using UnityEngine.SceneManagement;

namespace HGDFall2023
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public bool IsPaused { get; private set; } = false;
     
        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
            SceneManager.sceneUnloaded += (_) => Unpause();
            DontDestroyOnLoad(this);
        }

        public void LoadLevel(int level)
        {
            LoadLevel($"Level{level}");
        }

        public void LoadLevel(string level)
        {
            SceneManager.LoadScene(level);
        }

        public void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void Pause()
        {
            Instance.IsPaused = true;
            Time.timeScale = 0;
            Instance.transform.Find("Pause Menu").gameObject.SetActive(true);
        }

        public void Unpause()
        {
            Instance.IsPaused = false;
            Time.timeScale = 1;
            transform.Find("Pause Menu").gameObject.SetActive(false);
        }

        private void Update()
        {
            // Make sure we're actually in a level and not in the menus
            if (SceneManager.GetActiveScene().buildIndex != 0
                && Input.GetKeyDown(KeyCode.Escape))
            {
                if (Instance.IsPaused)
                {
                    Unpause();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
}