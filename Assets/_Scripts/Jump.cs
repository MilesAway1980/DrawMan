using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	bool isJumping = false;
	bool isFalling = false;
	bool onGround = false;
	bool jumpReady = false;
	float jumpTimer = 0;		//Used for any random occurances when conditions allow the player to jump twice

	Rigidbody2D rb;
	public float jumpPower;
	public float touchAngle;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround) {
			isJumping = false;
		} else {
			jumpReady = false;
		}

		print (rb.velocity.y);

		if (rb.velocity.y < 0) {			
			isFalling = true;
			isJumping = false;
		} else {
			if (rb.velocity.y > 0) {
				jumpReady = false;
				onGround = false;
			}
			isFalling = false;
		}

		if (!isJumping && !isFalling) {			
			onGround = true;
		}
	}

	public void setTouchAngle(float newTouchAngle) {
		touchAngle = newTouchAngle;
	}

	public void setJumpPower(float newJumpPower) {
		jumpPower = newJumpPower;
	}

	public void jump() {
		float timeSinceLastJump = Time.fixedTime - jumpTimer;

		if (!jumpReady || (timeSinceLastJump < 0.25)) {
			return;
		}

		jumpTimer = Time.fixedTime;

		isJumping = true;
		isFalling = false;
		onGround = false;
		jumpReady = false;

		rb.AddForce (
			new Vector2(0, jumpPower * 100)
		);
	}

	void OnCollisionStay2D(Collision2D hit) {
		
		foreach (ContactPoint2D touch in hit.contacts) {
			float angle = Angle.getAngle(touch.point, gameObject.transform.position);

			if (
				(angle >= (360 - touchAngle) && angle <= 360) ||
				(angle >= 0 && angle <= touchAngle)
			) 
			{				
				if (!isJumping) {					
					//Debug.Log ("Ready at " + touchAngle);
					jumpReady = true;
				}
			}
		}
	}

	/*void OnCollisionStay2D(Collision2D hit) {
		//Debug.Log ("Stay");
	}
	void OnCollisionLeave2D(Collision2D hit) {
		Debug.Log ("Leave");
	}*/
}
