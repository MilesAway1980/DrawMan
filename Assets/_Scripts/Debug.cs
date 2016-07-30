using UnityEngine;
using System.Collections;

public class Debug : MonoBehaviour {

	public float startingDraw;

	Equipment eq;

	bool initialized;

	Player player;
	// Use this for initialization
	void Start () {		
		initialized = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!initialized) {
			if (player != null) {
				initialized = true;
				eq = player.GetComponent<Equipment> ();
				eq.addItem (1, startingDraw);
			} else {
				player = FindObjectOfType<Player> ();
			}
		}
	}
}
