using UnityEngine;

namespace HGDFall2023
{
    public class FollowCamera : MonoBehaviour
    {
        public GameObject target;
        public float lerpSpeed = 1;

        private void FixedUpdate()
        {
            // Lerp camera and let unity handle collisions with walls
            Vector3 difference = target.transform.position - transform.position;
            GetComponent<Rigidbody2D>().velocity = lerpSpeed * 30 * difference;
        }
    }
}