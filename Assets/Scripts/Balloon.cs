using UnityEngine;

namespace HGDFall2023
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Balloon : MonoBehaviour 
    {
        public float maxAcceleration = 10;

        private Rigidbody2D rb;
        private Vector2 lastVelocity;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();    
        }

        private void FixedUpdate()
        {
            Vector2 acceleration = lastVelocity - rb.velocity;
            lastVelocity = rb.velocity;

            if (acceleration.magnitude > maxAcceleration)
            {
                gameObject.SetActive(false);
            }
        }
    }
}