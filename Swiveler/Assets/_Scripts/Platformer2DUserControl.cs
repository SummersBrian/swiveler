/*
 * Author: Brian Summers
 * Date: 4/6/15
 * This file has been modified from its original version in the standard assets package.
 * 
 * 
 */
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		public GameObject rotatingSquareParent;
		public Camera2DFollow camera;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				//alpha1_block1.transform.rotation = Quaternion.AngleAxis(rotationAngle, alpha1_block1.transform.forward);
				//alpha1_block2.transform.rotation = Quaternion.AngleAxis(rotationAngle, alpha1_block2.transform.forward);
				Transform t = rotatingSquareParent.transform.GetChild(0);
				for (int i = 0; i < t.childCount; i++) {
					Transform c = t.GetChild(i);
					float currRot = c.eulerAngles.z;
					if (currRot == 360.0f)
						currRot = 0.0f;
					c.rotation = Quaternion.AngleAxis(currRot + 90.0f, c.forward);
				}
			}

			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				//alpha1_block1.transform.rotation = Quaternion.AngleAxis(rotationAngle, alpha1_block1.transform.forward);
				//alpha1_block2.transform.rotation = Quaternion.AngleAxis(rotationAngle, alpha1_block2.transform.forward);
				Transform t = rotatingSquareParent.transform.GetChild(1);
				for (int i = 0; i < t.childCount; i++) {
					Transform c = t.GetChild(i);
					float currRot = c.eulerAngles.z;
					if (currRot == 360.0f)
						currRot = 0.0f;
					c.rotation = Quaternion.AngleAxis(currRot + 90.0f, c.forward);
				}
			}

			if (!m_Jump) {
				// Read the jump input in Update so button presses aren't missed.
				m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
			}
        }


        private void FixedUpdate()
        {

			// Read the inputs.
			bool crouch = Input.GetKey (KeyCode.LeftControl);
			float dir = Input.GetAxis ("WASD");
							// Pass all parameters to the character control script.
			m_Character.Move (dir, crouch, m_Jump);
			if (Input.GetKey(KeyCode.LeftShift)) {
				float camMoveHoriz = Input.GetAxis ("LeftRightArrows");
				float camMoveVert = Input.GetAxis ("UpDownArrows");
				camera.moveCamera (new Vector3 (camMoveHoriz, camMoveVert, 0));
			} else {
				camera.releaseControl();
			}
			//camera.releaseControl();
			m_Jump = false;
		}
        
    }
}
