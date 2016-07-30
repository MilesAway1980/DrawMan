using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Magnet : MonoBehaviour {

	public enum Polarity {Positive, Negative}
	public Polarity polarity;

	public float strength;

	static List<Magnet> magnetList;

	[Tooltip ("Magnet that does not interact with this magnet.\nCan use to make a magnet with a positive and negative end.")]
	public Magnet pair;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		if (magnetList == null) {
			magnetList = new List<Magnet> ();
		}

		rb = GetComponent<Rigidbody2D> ();

		magnetList.Add (this);
	}

	void OnDestroy() {
		magnetList.Remove (this);
	}
	
	// Update is called once per frame
	void Update () {

		/*
		 * 		Point Magnets:
		 * 
		 * 		F = strength(1) * strength(2)
		 *          -------------------------
		 *                 4 * Pi * distance^2
		*/

		if (rb == null) {
			//Objects without RigidBodies cannot be moved.
			return;
		}

		Vector2 magForce = Vector2.zero;

		for (int i = 0; i < magnetList.Count; i++) {
			Magnet magnet = magnetList [i];
			if (magnet != this) {
				float dist = Vector2.Distance (transform.position, magnet.transform.position);

				float force = (strength * magnet.getStrength()) / (4 * Mathf.PI * dist * dist);
				float angle = Angle.getAngle (this.transform.position, magnet.transform.position);

				if (polarity == magnet.getPolarity ()) {
					force = -force;
				}

				//print (angle + " " + force);

				//If the polarities are opposite, they repel.  Otherwise, they attract.

				magForce = new Vector2 (
					magForce.x + Mathf.Sin(angle / Mathf.Rad2Deg) * force,
					magForce.y + Mathf.Cos(angle / Mathf.Rad2Deg) * force
				);
			}
		}

		//print (magForce);

		rb.AddForce (magForce);
	}

	public float getStrength() {
		return strength;
	}

	public Polarity getPolarity() {
		return polarity;
	}
}
