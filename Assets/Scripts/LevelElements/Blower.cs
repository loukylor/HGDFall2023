using UnityEngine;

namespace HGDFall2023.LevelElements
{
    public class Blower : ElementBase
    {
        public Vector3 force;

        protected override void OnPlayerTriggerStay(Collider2D collision)
        {
            collision.attachedRigidbody.AddForce(force * Time.fixedDeltaTime);
        }
    }
}