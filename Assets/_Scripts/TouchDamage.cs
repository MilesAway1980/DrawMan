using UnityEngine;
using System.Collections;

public class TouchDamage : MonoBehaviour {

	public float damage;
	public bool instantKill;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		HitPoints hp = col.gameObject.GetComponent<HitPoints> ();
		if (hp != null) {
			if (instantKill) {
				hp.kill ();	
			} else {
				hp.damage (damage);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		HitPoints hp = col.gameObject.GetComponent<HitPoints> ();
		if (hp != null) {
			if (instantKill) {
				hp.kill ();	
			} else {
				hp.damage (damage);
			}
		}
	}
}
