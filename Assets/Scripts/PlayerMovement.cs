using UnityEngine;

namespace HGDFall2023 
{
    public class PlayerMovement : MonoBehaviour
    {
        public float legSpeed = 40;
        public float kneeSpeed = 60;
        public float maxLegAngle = 30;
        public float maxKneeAngle = 100;

        public RelativeJoint2D leftLeg;
        public RelativeJoint2D rightLeg;
        public RelativeJoint2D leftKnee;
        public RelativeJoint2D rightKnee;

        public PhysicsMaterial2D forwardLegMaterial;
        public PhysicsMaterial2D backLegMaterial;

        private void Update()
        {
            // Shuffle legs forward and back
            if (Input.GetKey(KeyCode.W)) 
            {
                MoveLeg(true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                MoveLeg(false);
            }
            else
            {
                // Reset legs
                leftLeg.correctionScale = 0.01f;
                leftKnee.connectedBody.sharedMaterial = backLegMaterial;
                rightLeg.correctionScale = 0.01f;
                rightKnee.connectedBody.sharedMaterial = backLegMaterial;
            }

            // Raise knees
            if (Input.GetKey(KeyCode.A))
            {
                RetractKnee(true);
            }
            else
            {
                // Reset left knee
                leftKnee.angularOffset = 0;
                leftKnee.correctionScale = 0.1f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                RetractKnee(false);
            } 
            else
            {
                // Reset right knees
                rightKnee.angularOffset = 0;
                rightKnee.correctionScale = 0.1f;
            }
        }

        public void MoveLeg(bool direction)
        {
            float speed = legSpeed * Time.deltaTime;
            RelativeJoint2D forwardLeg;
            RelativeJoint2D forwardKnee;
            RelativeJoint2D backLeg;
            if (direction)
            {
                forwardLeg = rightLeg;
                forwardKnee = rightKnee;
                backLeg = leftLeg;
            }
            else
            {
                forwardLeg = leftLeg;
                forwardKnee = leftKnee;
                backLeg = rightLeg;
            }


            if (Mathf.Abs(backLeg.angularOffset - speed) <= maxLegAngle)
            {
                backLeg.angularOffset -= speed;
                backLeg.correctionScale = 0.3f;
            }

            if (Mathf.Abs(forwardLeg.angularOffset + speed) <= maxLegAngle)
            {
                forwardLeg.angularOffset += speed * 1.5f;
                forwardLeg.correctionScale = 0.4f;
                // Give forward leg a slight advantage in friction to make
                // walking faster
                forwardKnee.connectedBody.sharedMaterial = forwardLegMaterial;
            }
        }

        public void RetractKnee(bool isLeftKnee)
        {
            float speed = kneeSpeed * Time.deltaTime;
            RelativeJoint2D knee = isLeftKnee ? leftKnee : rightKnee;

            // Only checking max on one direction since knees only move one
            // direction
            if (knee.angularOffset - speed >= -maxKneeAngle)
            {
                knee.angularOffset -= speed;
                knee.correctionScale = 0.3f;
            }
        }
    }
}

