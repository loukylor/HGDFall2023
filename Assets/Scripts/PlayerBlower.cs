using UnityEngine;

namespace HGDFall2023
{
    public class PlayerBlower : MonoBehaviour
    {
        public GameObject player;
        public float force;
        public float cooldown;
        public Sprite initialSprite;
        public Sprite blowSprite;

        private float lastWhoosh;
        private new SpriteRenderer renderer;

        private void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            // Set the position of the blower to the mouse position
            // Vector 2 cast causes unity to disregard the z value
            transform.position = 
                (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Point blower at player
            Vector3 direction
                = (player.transform.position - transform.position).normalized;
            transform.eulerAngles = new Vector3(0, 0, 
                Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x)
            );
            
            if (Time.time - lastWhoosh < cooldown)
            {
                return;
            }

            renderer.sprite = initialSprite;

            if (!Input.GetMouseButtonDown(0)
                || GameManager.Instance.IsPaused)// || micClip == null)
            {
                return;
            }

            float average = 1;

            // Raycast for player
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, direction, 4, 1 << 3
            );

            if (hit.rigidbody == null)
            {
                return;
            }

            lastWhoosh = Time.time;
            // Linear falloff (y = mx + b)
            float falloff = (-0.25f * hit.distance) + 1;
            Debug.Log(falloff);
            hit.rigidbody.AddForce(falloff * average * force * direction);
            Debug.Log($"whoosh {falloff * average * force * direction}");
            renderer.sprite = blowSprite;
            GameManager.Instance.PlaySound("puff", false, 1.0f);
        }
    }
}