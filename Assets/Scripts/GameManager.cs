using System.Collections;
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
            SceneManager.sceneUnloaded += (_) =>
            {
                StopAllCoroutines();
                Unpause();
            };
            DontDestroyOnLoad(this);
            LoadLevel("Menu");
        }

        public void LoadLevel(int level)
        {
            LoadLevel($"Level{level}");
        }

        public void LoadLevel(string level)
        {
            SceneManager.LoadScene(level);
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex + 1
            );
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
            Instance.transform.Find("Pause Menu/Canvas/Buttons/Resume")
                .gameObject.SetActive(true);
            Instance.transform.Find("Pause Menu/Canvas/Buttons/Continue")
                .gameObject.SetActive(false);
        }

        public void Unpause()
        {
            Instance.IsPaused = false;
            Time.timeScale = 1;
            transform.Find("Pause Menu").gameObject.SetActive(false);
        }

        public void KillPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.SetActive(false);
            StartCoroutine(OpenMenuCoroutine());
        }

        public void OpenDeathMenu()
        {
            IsPaused = true;
            Time.timeScale = 0;
            Instance.transform.Find("Pause Menu").gameObject.SetActive(true);
            Instance.transform.Find("Pause Menu/Canvas/Buttons/Resume")
                .gameObject.SetActive(false);
            Instance.transform.Find("Pause Menu/Canvas/Buttons/Continue")
                .gameObject.SetActive(false);
        }

        public void OpenFinishMenu()
        {
            IsPaused = true;
            Time.timeScale = 0;
            Instance.transform.Find("Pause Menu").gameObject.SetActive(true);
            Instance.transform.Find("Pause Menu/Canvas/Buttons/Resume")
                .gameObject.SetActive(false);
            Instance.transform.Find("Pause Menu/Canvas/Buttons/Continue")
                .gameObject.SetActive(true);
        }

        private IEnumerator OpenMenuCoroutine()
        {
            yield return new WaitForSeconds(2);

            OpenDeathMenu();
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