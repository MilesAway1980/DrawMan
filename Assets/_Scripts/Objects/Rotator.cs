using UnityEngine;
using System.Collections;

public class Rotator: Lock {

	public enum SpinDirection {Left, Right};

	float mechanismStartRotation;		//The angle the mechanism starts rotation at.
	float mechanismEndRotation;			//The angle the mechanism ends rotation at.  Only used if it locks.
	float mechanismCurrentRotation;
	float mechanismPrevRotation;

	float currentAngle;
	float prevAngle;

	[Tooltip("Which direction increases the rotation?")]
	public SpinDirection spinDirection;

	[Tooltip("Does the rotation count stop increasing when the max limit is reached?")]
	public bool limitMaxRotation;
	[Tooltip("Does the rotation count stop decreasing when the min limit is reached?")]
	public bool limitMinRotation;

	bool moving;

	HingeJoint2D hinge;
	JointAngleLimits2D limits;

	void Start () {
		hinge = GetComponent<HingeJoint2D> ();
		limits = hinge.limits;

		mechanismStartRotation = transform.rotation.eulerAngles.z;
		mechanismCurrentRotation = mechanismStartRotation;
		mechanismEndRotation = mechanismStartRotation + (limits.max - limits.min);
		moving = false;
		prevAngle = mechanismStartRotation;
	}

	void Update () {

		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		currentAngle = transform.rotation.eulerAngles.z;

		int turnVel = (int)rb.angularVelocity;

		if (turnVel != 0) {
			
			moving = true;

			float angleDist = prevAngle - currentAngle;
			prevAngle = currentAngle;

			if (turnVel < 0) {
				if (angleDist < 0) {
					if (Mathf.Abs (angleDist) > 180) {
						angleDist += 360;
					}
				}
			} else if (turnVel > 0) {
				if (angleDist > 0) {
					if (Mathf.Abs (angleDist) > 180) {
						angleDist -= 360;
					}
				}
			}

			if (spinDirection == SpinDirection.Left) {
				mechanismCurrentRotation -= angleDist;
			} else {
				mechanismCurrentRotation += angleDist;
			}

			if (limitMinRotation) {
				if (mechanismCurrentRotation < mechanismStartRotation) {
					mechanismCurrentRotation = mechanismStartRotation;
				}
			}
			if (limitMaxRotation) {
				if (mechanismCurrentRotation > mechanismEndRotation) {
					mechanismCurrentRotation = mechanismEndRotation;
				}
			}

		} else {
			moving = false;
		}
	}

	public void setLimitRotation(bool limit) {
		HingeJoint2D hj = GetComponent<HingeJoint2D> ();
		hj.useLimits = limit;
	}

	public void setMaxRotation(float newMaxRotation) {
		JointAngleLimits2D jl = new JointAngleLimits2D();		
		jl.max = newMaxRotation;		

		HingeJoint2D hj = GetComponent<HingeJoint2D> ();
		hj.limits = jl;
	}

	public void setMinRotation(float newMinRotation) {
		JointAngleLimits2D jl = new JointAngleLimits2D();
		jl.min = newMinRotation;

		HingeJoint2D hj = GetComponent<HingeJoint2D> ();
		hj.limits = jl;
	}

	public void setRotation(float newMinRotation, float newMaxRotation) {
		JointAngleLimits2D jl = new JointAngleLimits2D();
		jl.min = newMinRotation;
		jl.max = newMaxRotation;

		HingeJoint2D hj = GetComponent<HingeJoint2D> ();
		hj.limits = jl;
	}

	public void setLimitMaxRotation(bool limit) {
		limitMaxRotation = limit;
	}

	public void setLimitMinRotation(bool limit) {
		limitMinRotation = limit;
	}

	public float getDegreesTurned() {
		return (mechanismCurrentRotation - mechanismStartRotation);
	}

	public float getRange() {
		return (limits.max - limits.min);
	}

	public override float getRatio() {
		return getDegreesTurned () / getRange ();
	}

	public override bool isMoving() {
		return moving;
	}
}
