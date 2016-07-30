using UnityEngine;
using System.Collections;

public class ConveyorCircle : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setSpeed(float newSpeed) {
		speed = newSpeed;
	}

	void OnCollisionStay2D(Collision2D col) {

		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D> ();

		if (rb != null) {
			ContactPoint2D[] cp = col.contacts;
			float angle = 0;
			for (int i = 0; i < cp.Length; i++) {
				angle = Angle.getAngle(transform.position, cp[i].point);
			}

			float speedX = Mathf.Cos (angle / Mathf.Rad2Deg) * speed;
			float speedY = Mathf.Sin (angle / Mathf.Rad2Deg) * speed;

			rb.AddForce (
				new Vector2(speedX, speedY)
			);
		}
	}
}
