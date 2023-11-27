using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HGDFall2023
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public bool IsPaused { get; private set; } = false;
        public AudioClip[] clips = new AudioClip[0];

        private readonly HashSet<string> sounds = new();
     
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

                foreach (AudioSource source in GetComponents<AudioSource>())
                {
                    Destroy(source);
                }
                sounds.Clear();
            };
            DontDestroyOnLoad(this);
            LoadMenu();
        }

        public void LoadLevel(int level)
        {
            SceneManager.LoadScene($"Level{level}");
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex + 1
            );
        }

        public bool HasUnlockedLevel(int level)
        {
            if (level == 1)
            {
                return true;
            }
            else
            {
                // Decrement because you unlock the level after the latest level
                // you've played
                level--;
            }

            return PlayerPrefs.GetString("levels").Split(',')
                .ToHashSet().Contains(level.ToString());
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
            StartCoroutine(WaitSeconds(1, OpenDeathMenu));
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

            // Add to completed levels
            HashSet<string> levels =
            PlayerPrefs.GetString("levels").Split(',').ToHashSet();
            levels.Add(
                (SceneManager.GetActiveScene().buildIndex - 1).ToString()
            );
            PlayerPrefs.SetString("levels", string.Join(",", levels));
        }

        public void PlaySound(string name, bool loop, float volume)
        {
            if (!sounds.Add(name))
            {
                return;
            }
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.volume = volume;
            AudioClip clip = clips.First((clip) => clip.name == name);
            source.clip = clip;
            source.Play();

            // Loop forever if loop is on
            if (!loop)
            {
                StartCoroutine(WaitSeconds(clip.length, () =>
                {
                    sounds.Remove(name);
                    Destroy(source);
                }));
            } 
            else
            {
                source.loop = true;
            }
        }

        private IEnumerator WaitSeconds(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);

            callback();
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