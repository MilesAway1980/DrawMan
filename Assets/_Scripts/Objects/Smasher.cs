using UnityEngine;
using System.Collections;

public class Smasher : MonoBehaviour {

	public float delay;
	public GameObject smasher;
	public GameObject[] piston;

	[Range (1, 1000)]
	public float acceleration;


	public GameObject startPoint;
	public GameObject endPoint;

	bool start;
	int dir;		//up = 1 		down = 0

	// Use this for initialization
	void Start () {
		if (smasher == null || piston.Length == 0) {
			Destroy (gameObject);
		}

		acceleration /= 5000;

		/*
		 * 	startPoint should always be on top and endPoint on the bottom. 
		 * 	If they're not, reverse them.
		 * 
		 * 	Either way, align the x position with the smasher
		 */

		if (startPoint.transform.position.y < endPoint.transform.position.y) {
			float temp = startPoint.transform.position.y;

			startPoint.transform.position = new Vector2 (
				smasher.transform.position.x,
				endPoint.transform.position.y
			);
			endPoint.transform.position = new Vector2 (
				smasher.transform.position.x,
				temp
			);
		} else {
			startPoint.transform.position = new Vector2 (
				smasher.transform.position.x,
				startPoint.transform.position.y
			);
			endPoint.transform.position = new Vector2 (
				smasher.transform.position.x,
				endPoint.transform.position.y
			);
		}

		smasher.GetComponent<Rigidbody2D> ().mass = 0;

		start = false;
		dir = 1;
	}
	
	// Update is called once per frame
	void Update () {		
		if (start == false) {
			if (Time.fixedTime >= delay) {
				start = true;
			} else {
				return;
			}
		}

		Rigidbody2D rb = smasher.GetComponent<Rigidbody2D> ();

		print (rb.velocity);

		if (dir == 0) {		//Going Down
			if (smasher.transform.position.y <= endPoint.transform.position.y) {				
				smasher.transform.position = endPoint.transform.position;
				rb.velocity = Vector2.zero;
				return;
			}

			if (rb.velocity.y >= 0) {
				dir = 1;
			}

			rb.AddForce (
				new Vector2(
					0, -acceleration
				)
			);
		} else {			//Going Up
			if (smasher.transform.position.y >= startPoint.transform.position.y) {				
				smasher.transform.position = startPoint.transform.position;
				rb.velocity = Vector2.zero;
				return;
			}

			if (rb.velocity.y <= 0) {
				dir = 0;
			}

			rb.AddForce (
				new Vector2(
					0, acceleration	
				)
			);
		}

		float halfWay = (smasher.transform.position.y + startPoint.transform.position.y) / 2.0f;
		float distance = startPoint.transform.position.y - smasher.transform.position.y;

		for (int i = 0; i < piston.Length; i++) {
			piston [i].transform.position = new Vector3 (
				piston[i].transform.position.x,
				halfWay,
				piston[i].transform.position.z
			);

			piston [i].transform.localScale = new Vector2(
				piston[i].transform.localScale.x,
				distance
			);
		}
	}
}
