using UnityEngine;
using System.Collections;

public class SafeZone : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			col.GetComponent<Player> ().setLastSafe ();
		}
	}
}
