using UnityEngine;
using System.Collections;

public class ShowMovementInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		float magnitude = Mathf.Abs(rb.velocity.magnitude);
		float mass = rb.mass;
		float angular = Mathf.Abs(rb.angularVelocity);

		print (mass + " " + Mathf.RoundToInt(magnitude) + " " + Mathf.RoundToInt(angular));
	}
}
