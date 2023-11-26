using UnityEngine;

namespace HGDFall2023
{
    public class FollowCamera : MonoBehaviour
    {
        public GameObject target;
        public float lerpSpeed = 1;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            // Lerp camera and let unity handle collisions with walls
            Vector3 difference = target.transform.position - transform.position;
            rb.velocity = lerpSpeed * 30 * difference;
        }
    }
}