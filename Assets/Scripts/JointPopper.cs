using UnityEngine;

namespace HGDFall2023 {
    [RequireComponent(typeof(RelativeJoint2D))]
    public class JointPopper : MonoBehaviour
    {
        public float brokenJointTorque = 50f;

        private void OnJointBreak2D(Joint2D joint)
        {
            if (joint is not RelativeJoint2D relativeJoint)
                return;

            Debug.Log(
                $"{joint.name} broke; torque {joint.reactionTorque};" +
                $"force {joint.reactionForce}"
            );

            relativeJoint.maxTorque = brokenJointTorque;
        }
    }
}

