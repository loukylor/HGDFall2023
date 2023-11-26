using UnityEngine;

namespace HGDFall2023.LevelElements
{
    public class Landmine : ElementBase
    {
        public float force = 10000;
        public Animator animator;

        protected override void OnPlayerCollision(Collision2D collision)
        {
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

            // TODO: Add explosion sound
            // Start explosion animation
            animator.enabled = true;

            // Add force in direction of explosion
            collision.rigidbody.AddForce(
                collision.GetContact(0).normal * -force
            );
            Debug.Log(collision.GetContact(0).normal);
        }

        // This will be called by the animator, and shouldn't be called manually
        public void OnAnimationFinish()
        {
            gameObject.SetActive(false);
        }
    }
}
