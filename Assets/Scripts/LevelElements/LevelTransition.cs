using UnityEngine;
using UnityEngine.SceneManagement;

namespace HGDFall2023.LevelElements
{
    public class LevelTransition : ElementBase
    {
        public string nextLevelSceneName;

        protected override void OnPlayerTrigger(Collider2D _)
        {
            Debug.Log("pp");
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}
