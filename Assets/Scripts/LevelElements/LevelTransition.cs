using UnityEngine;

namespace HGDFall2023.LevelElements
{
    public class LevelTransition : ElementBase
    {
        public string nextLevelSceneName;

        protected override void OnPlayerTrigger(Collider2D _)
        {
            GameManager.LoadLevel(nextLevelSceneName);
        }
    }
}
