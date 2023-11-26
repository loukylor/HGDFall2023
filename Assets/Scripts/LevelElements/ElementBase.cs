using UnityEngine;

namespace HGDFall2023.LevelElements
{
    [RequireComponent(typeof(Collider2D))]
    public class ElementBase : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsPlayer(collision.gameObject))
            {
                return;
            }

            OnPlayerCollision(collision);
        }
        
        protected virtual void OnPlayerCollision(Collision2D collision) { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsPlayer(collision.gameObject))
            {
                return;
            }

            OnPlayerTrigger(collision);
        }

        protected virtual void OnPlayerTrigger(Collider2D collision) { }

        protected bool IsPlayer(GameObject go)
        {
            // Get root parent
            Transform parent = go.transform;
            while (parent.parent != null)
            {
                parent = parent.parent;
            }

            // Check if root parent has the player tag
            return parent.CompareTag("Player");
        }

    }
}