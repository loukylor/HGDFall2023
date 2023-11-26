using UnityEngine;

namespace HGDFall2023.LevelElements
{
    public class Landmine : ElementBase
    {
        public float force = 10000;
        public Animator animator;

        protected override void OnPlayerCollision(Collision2D collision)
        {
            // TODO: Add explosion sound
            // Start explosion animation
            animator.enabled = true;

            // Add force in direction of explosion
            collision.rigidbody.AddForce(
                collision.GetContact(0).normal * -force
            );
        }

        // This will be called by the animator, and shouldn't be called manually
        public void OnAnimationFinish()
        {
            gameObject.SetActive(false);
        }
    }
}
