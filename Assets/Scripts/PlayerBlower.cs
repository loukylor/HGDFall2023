using UnityEngine;

namespace HGDFall2023
{
    public class PlayerBlower : MonoBehaviour
    {
        // TODO: implement multiple mics
        public new Camera camera;
        public GameObject player;
        public float force;
        public float cooldown;

        private AudioClip micClip;
        private readonly float[] samples = new float[4410];
        private float lastWhoosh;

        private void Start()
        {
            // Make sure I have permission to use the microphone
            AsyncOperation op = Application.RequestUserAuthorization(
                UserAuthorization.Microphone
            );

            op.completed += (_) =>
            {
                micClip = Microphone.Start(
                    null, true, 1, 44100
                );
            };
        }

        private void Update()
        {
            // Set the position of the blower to the mouse position
            // Vector 2 cast causes unity to disregard the z value
            transform.position = 
                (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);
            // Point blower at player
            Vector3 playerDirection
                = (player.transform.position - transform.position).normalized;
            transform.right = playerDirection;
            
            if (micClip == null || Time.time - lastWhoosh < cooldown) 
            {
                return;
            }

            // Get data to calculate rolling average
            // Ensure position is never negative
            int position = Microphone.GetPosition(null) - (samples.Length / 2);
            if (position < 0)
            {
                position = micClip.samples + position;
            }
            micClip.GetData(samples, position);

            // Don't use linq for better performance
            double total = 0;
            for (int i = 0; i < samples.Length; i++)
            {
                total += Mathf.Abs(samples[i]);
            }
            float average = (float)(total / samples.Length);

            // TODO: Add activation force
            if (average < 0.2)
            {
                return;
            }

            // Raycast for player
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, playerDirection, 2, 1 << 3
            );

            if (hit.rigidbody == null)
            {
                return;
            }

            lastWhoosh = Time.time;
            hit.rigidbody.AddForce(average * force * playerDirection);
            Debug.Log($"whoosh {average * force * playerDirection}");
        }

        private void OnDestroy()
        {
            Microphone.End(null);
        }
    }
}