using UnityEngine;

namespace HGDFall2023.LevelElements
{
    public class LevelTransition : ElementBase
    {
        protected override void OnPlayerTrigger(Collider2D _)
        {
            GameManager.Instance.OpenFinishMenu();
        }
    }
}
