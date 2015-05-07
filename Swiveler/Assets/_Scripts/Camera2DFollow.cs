using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

		public GameObject floor;
		public GameObject ceiling;
		public GameObject rightWall;
		public GameObject leftWall;
		private float xMax;
		private float xMin;
		private float yMax;
		private float yMin;
		private bool userControl;
		private Vector3 userInput;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
			yMin = floor.transform.position.y;
			yMax = ceiling.transform.position.y;
			xMin = leftWall.transform.position.x;
			xMax = rightWall.transform.position.x;
			userControl = false;
			userInput = new Vector3 (0,0,0);
        }


        // Update is called once per frame
        private void Update()
        {
			// only update lookahead pos if accelerating or changed direction
			float xMoveDelta = (target.position - m_LastTargetPosition).x;

			bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;

			if (updateLookAheadTarget) {
				m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
			} else {
				m_LookAheadPos = Vector3.MoveTowards (m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
			}

			Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;

			Vector3 newPos;
			if (!userControl) {
				newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
			} else {
				newPos = transform.position + userInput;
				//newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
			}


			if (newPos.x > xMax)
				newPos.x = xMax;
			if (newPos.x < xMin)
				newPos.x = xMin;
			if (newPos.y > yMax)
				newPos.y = yMax;
			if (newPos.y < yMin)
				newPos.y = yMin;
			transform.position = newPos;

			m_LastTargetPosition = target.position;
        }

		public void moveCamera(Vector3 dir) {
			userControl = true;
			userInput = dir;
		}

		public void releaseControl() {
			userControl = false;
		}
    }
}
