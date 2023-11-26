using UnityEngine;

namespace HGDFall2023.LevelElements
{
    public class Blower : ElementBase
    {
        private static Blower currentPusher;

        public Vector3 force;

        protected override void OnPlayerTriggerStay(Collider2D collision)
        {
            // Prevent multiple pushers from pushing
            if (currentPusher != this && currentPusher != null)
            {
                return;
            }

            collision.attachedRigidbody.AddForce(force * Time.fixedDeltaTime);
            currentPusher = this;
        }

        protected override void OnPlayerTriggerExit(Collider2D collision)
        {
            currentPusher = null;
        }
    }
}