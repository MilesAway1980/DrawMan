using UnityEngine;
using System.Collections;

public class PressurePlate : Lock {

	float pressDistance;
	float startPos;
	float endPos;

	SliderJoint2D slider;

	bool moving;

	// Use this for initialization
	void Start () {

		slider = GetComponent<SliderJoint2D> ();

		JointTranslationLimits2D limits = slider.limits;

		pressDistance = limits.max - limits.min;

		startPos = transform.position.y;
	}

	void Update() {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();

		int v = (int)(rb.velocity.y * 10000);

		if (v != 0) {
			moving = true;
		} else {
			moving = false;
		}
	}
	
	public override float getRatio() {		
		return (startPos - transform.position.y) / pressDistance;
	}

	public override bool isMoving() {
		return moving;
	}
}
