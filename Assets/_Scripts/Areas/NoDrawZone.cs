using UnityEngine;
using System.Collections;

public class NoDrawZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {		
		if (col.gameObject.tag == "Drawn Object") {
			//Destroy (col.gameObject);
		}
	}
}
