using UnityEngine;
using UnityEngine.SceneManagement;

namespace HGDFall2023
{
    public class LevelTransition : MonoBehaviour
    {
        public string nextLevelSceneName;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("pp");
            // Get root parent
            Transform parent = collision.transform;
            while (parent.parent != null)
            {
                parent = parent.parent;
            }

            // Check if root parent has the player tag
            if (!parent.CompareTag("Player"))
            {
                // If it's not the player, then return
                return;
            }

            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}
