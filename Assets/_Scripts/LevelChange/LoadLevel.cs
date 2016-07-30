using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	void Awake() {

		if (PersistantData.justWarped) {
			Player[] player = FindObjectsOfType<Player>();
			for (int i = 0; i < player.Length; i++) {
				if (player [i].justWarped == true) {
					player [i].warpTo (PersistantData.warpingTo);
					Camera.main.GetComponent<CamFollow> ().setTarget (player [i].gameObject);
				} else {
					Destroy (player [i].gameObject);
				}
				PersistantData.justWarped = false;
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
