using UnityEngine;
using System.Collections;

public class Initialization : MonoBehaviour {

	GameObject playerPrefab;
	GameObject GUIPrefab;

	//Player player;

	void Awake() {

		playerPrefab = Resources.Load("Prefabs/Player/Player") as GameObject;
		GUIPrefab = Resources.Load("Prefabs/Player/GUI") as GameObject;

		Player[] playerObjects = FindObjectsOfType<Player> ();
		if (playerObjects.Length == 0) {
			GameObject player = (GameObject)Instantiate (playerPrefab);
			player.name = "Player";

			Camera.main.GetComponent<CamFollow> ().setTarget (player);
		}

		Instantiate (GUIPrefab);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
