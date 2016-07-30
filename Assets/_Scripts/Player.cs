using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed;
	public float maxSpeed;

	Rigidbody2D rb;

	Jump jump;
	HitPoints hp;

	Vector2 lastSafe;

	bool dying;

	public bool justWarped = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		jump = GetComponent<Jump> ();
		hp = GetComponent<HitPoints> ();
		lastSafe = new Vector2 ();
	}

	// Update is called once per frame
	void Update () {
		
		bool moveButton = Input.GetMouseButton (1);
		bool jumpButton = Input.GetMouseButton (3);

		if (moveButton) {
			Vector2 mousePos2D = Input.mousePosition;
			Vector3 mouseScreen = Camera.main.ScreenToWorldPoint (mousePos2D);

			if (mouseScreen.x > transform.position.x) {
				rb.AddForce (new Vector2 (
					moveSpeed,
					0
				));

			} else if (mouseScreen.x < transform.position.x) {
				rb.AddForce (new Vector2 (
					-moveSpeed,
					0
				));
			}
		}

		if (rb.velocity.x < -maxSpeed) {
			rb.velocity = new Vector2 (
				-maxSpeed, rb.velocity.y
			);
		} else if (rb.velocity.x > maxSpeed) {
			rb.velocity = new Vector2 (
				maxSpeed, rb.velocity.y
			);
		}

		if (jumpButton) {	
			jump.jump ();
		}

		if (hp.isDead ()) {		

			Explode ex = GetComponent<Explode> ();
			MeshRenderer mr = GetComponent<MeshRenderer> ();
			CircleCollider2D cc = GetComponent<CircleCollider2D> ();

			if (dying == false) {				
				mr.enabled = false;
				cc.enabled = false;
				if (ex.isStarted () == false) {
					ex.startExplosion ();
				}
			}

			if (ex.isFinished ()) {
				mr.enabled = true;
				cc.enabled = true;
				transform.position = lastSafe;
				hp.revive (100);
				dying = false;
			} else {
				dying = true;
			}
		}
	}

	public void setLastSafe() {
		lastSafe = transform.position;
	}

	void OnCollisionEnter2D(Collision2D col) {
		
		if (col.gameObject.tag == "Drawn Object") {
			return;
		}
		
		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D> ();
		if (rb != null) {	
			float magnitude = Mathf.Abs(rb.velocity.magnitude);
			float mass = rb.mass;
			float angular = Mathf.Abs(rb.angularVelocity);

			//float damage = (magnitude + mass + angular);

			float damage = 0;

			if (magnitude > 1) {
				damage += magnitude * mass;
			}

			if (angular > 1) {
				damage += angular * mass;
			}

			//print (mass + " " + magnitude + " " + angular + " " + (mass * magnitude) + " " + (mass * angular) + " " + damage);

			//hp.damage (damage);
		}
	}

	public void warpTo(Vector2 destination) {
		transform.position = destination;
	}
}
