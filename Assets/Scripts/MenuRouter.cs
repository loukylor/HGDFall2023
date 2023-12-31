using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HGDFall2023
{
    public class MenuRouter : MonoBehaviour
    {
        private readonly Stack<GameObject> pageStack = new();

        private void Start()
        {
            OpenMenu("MainMenu");
        }

        public void OpenMenu(string gameMenu)
        {
            // Hide current
            if (pageStack.TryPeek(out GameObject current))
            {
                current.SetActive(false);
            }

            // Add new to page stack and show it
            pageStack.Push(transform.Find(gameMenu).gameObject);
            pageStack.Peek().SetActive(true);

            if (gameMenu == "LevelSelect")
            {
                Transform buttons = transform.Find("LevelSelect/Buttons/Levels");
                foreach (Transform child in buttons)
                {
                    Button button = child.GetComponent<Button>();
                    if (button == null)
                    {
                        continue;
                    }

                    int level = int.Parse(child.name);
                    if (GameManager.Instance.HasUnlockedLevel(level))
                    {
                        continue;
                    }

                    button.interactable = false;
                }
            }
        }

        public void Back()
        {
            // Remove current page from stack
            if (!pageStack.TryPop(out GameObject current))
            {
                return;
            }

            // And hide it
            current.SetActive(false);

            // Show the new top most page
            if (pageStack.TryPeek(out GameObject last))
            {
                last.SetActive(true);
            }
            else
            {
                // Or the main menu if there is nothing in the stack
                OpenMenu("MainMenu");
            }
        }

        public void LoadLevel(int level)
        {
            GameManager.Instance.LoadLevel(level);
        }
    }
}
