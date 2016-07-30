using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {

	[Tooltip("Left = Negative values.  Right = Positive values")]
	public float speed;
	[Tooltip("The higher the value, the slower the gears will turn. Gear rotation = speed / gearSlowdown.")]
	public float gearSlowdown;
	public GameObject[] gears;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < gears.Length; i++) {
			/*
			 *  The gears are placed inside a "wheel" on the conveyor belt.  The wheel is the gear's parent.
			*/
			ConveyorCircle cc = gears [i].transform.parent.gameObject.AddComponent<ConveyorCircle> ();
			cc.setSpeed (speed);
		}

		setGearSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setSpeed(float newSpeed) {
		speed = newSpeed;
		setGearSpeed ();
	}

	void setGearSpeed() {
		for (int i = 0; i < gears.Length; i++) {
			HingeJoint2D hj = gears [i].GetComponent<HingeJoint2D> ();
			JointMotor2D jm = hj.motor;

			jm.motorSpeed = (speed / gearSlowdown);

			hj.motor = jm;

			ConveyorCircle cc = gears [i].transform.parent.gameObject.GetComponent<ConveyorCircle> ();
			cc.setSpeed (speed);
		}
	}

	void OnCollisionStay2D(Collision2D col) {
			
		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D> ();

		if (rb != null) {
			bool above = true;

			ContactPoint2D[] cp = col.contacts;
			for (int i = 0; i < cp.Length; i++) {

				float angle = Angle.getAngle(transform.position, cp[i].point) + transform.rotation.eulerAngles.z;

				while (angle < 0 || angle >= 360) {
					if (angle < 0) {
						angle += 360;
					} else if (angle >= 360) {
						angle -= 360;
					}
				}

				if (angle >= 90 && angle <= 270) {
					above = false;
				}
			}

			float tempSpeed = speed;
			if (!above) {
				tempSpeed = -speed;
			} 

			rb.AddForce (
				new Vector2(tempSpeed, 0)
			);
		}
	}
}
