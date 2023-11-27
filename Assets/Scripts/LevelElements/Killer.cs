using UnityEngine;

namespace HGDFall2023.LevelElements
{
    public class Killer : ElementBase
    {
        protected override void OnPlayerTrigger(Collider2D collision)
        {
            GameManager.Instance.KillPlayer();
            GameManager.Instance.PlaySound("pop", false, 0.5f);
        }

        protected override void OnPlayerCollision(Collision2D collision)
        {
            GameManager.Instance.KillPlayer();
            GameManager.Instance.PlaySound("pop", false, 0.5f);
        }
    }
}