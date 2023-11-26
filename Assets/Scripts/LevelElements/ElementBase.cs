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

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!IsPlayer(collision.gameObject))
            {
                return;
            }

            OnPlayerCollisionStay(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!IsPlayer(collision.gameObject))
            {
                return;
            }

            OnPlayerCollisionExit(collision);
        }

        protected virtual void OnPlayerCollision(Collision2D collision) { }
        protected virtual void OnPlayerCollisionStay(Collision2D collision) { }
        protected virtual void OnPlayerCollisionExit(Collision2D collision) { }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsPlayer(collision.gameObject))
            {
                return;
            }

            OnPlayerTrigger(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!IsPlayer(collision.gameObject))
            {
                return;
            }

            OnPlayerTriggerStay(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!IsPlayer(collision.gameObject))
            {
                return;
            }

            OnPlayerTriggerExit(collision);
        }

        protected virtual void OnPlayerTrigger(Collider2D collision) { }
        protected virtual void OnPlayerTriggerStay(Collider2D collision) { }
        protected virtual void OnPlayerTriggerExit(Collider2D collision) { }

        protected bool IsPlayer(GameObject go)
        {
            // Get root parent
            Transform parent = go.transform;
            while (parent.parent != null)
            {
                if (parent.CompareTag("Player"))
                {
                    return true;
                }
                parent = parent.parent;
            }

            // Check if root parent has the player tag
            return parent.CompareTag("Player");
        }

    }
}