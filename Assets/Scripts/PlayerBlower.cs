using UnityEngine;

namespace HGDFall2023
{
    public class PlayerBlower : MonoBehaviour
    {
        // TODO: implement multiple mics
        public GameObject player;
        public float force;
        public float cooldown;
        public Sprite initialSprite;
        public Sprite blowSprite;

        //private AudioClip micClip;
        //private readonly float[] samples = new float[4410];
        private float lastWhoosh;
        private new SpriteRenderer renderer;

        private void Start()
        {
            //// Make sure I have permission to use the microphone
            //AsyncOperation op = Application.RequestUserAuthorization(
            //    UserAuthorization.Microphone
            //);

            //op.completed += (_) =>
            //{
            //    micClip = Microphone.Start(
            //        null, true, 1, 44100
            //    );
            //};
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

            // Get data to calculate rolling average
            // Ensure position is never negative
            //int position = Microphone.GetPosition(null) - (samples.Length / 2);
            //if (position < 0)
            //{
            //    position = micClip.samples + position;
            //}
            //micClip.GetData(samples, position);

            //// Don't use linq for better performance
            //double total = 0;
            //for (int i = 0; i < samples.Length; i++)
            //{
            //    total += Mathf.Abs(samples[i]);
            //}
            //float average = (float)(total / samples.Length);

            //// TODO: Add activation force
            //if (average < 0.2)
            //{
            //    return;
            //}
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
        }

        private void OnDestroy()
        {
            Microphone.End(null);
        }
    }
}