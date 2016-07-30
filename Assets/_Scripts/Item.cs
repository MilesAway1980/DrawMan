using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public int itemType;
	public float amount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<Equipment> ().addItem (itemType, amount);
			Destroy (this.gameObject);
		}
	}
}
