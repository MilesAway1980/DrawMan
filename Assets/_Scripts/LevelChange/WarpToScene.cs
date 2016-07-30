using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WarpToScene : MonoBehaviour {

	public int whichLevelNum = -1;
	public string whichLevel;
	public Vector2 destination;

	//BoxCollider2D bc;

	// Use this for initialization
	void Start () {
		//bc = GetComponent<BoxCollider2D> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {

			Player player = col.gameObject.GetComponent<Player> ();
			player.justWarped = true;

			PersistantData.justWarped = true;
			PersistantData.warpedFrom = col.gameObject.transform.position;
			PersistantData.warpingTo = destination;

			//print (PersistantData.player);

			if (whichLevelNum >= 0) {
				SceneManager.LoadScene (whichLevelNum, LoadSceneMode.Single);
			} else {
				SceneManager.LoadScene (whichLevel, LoadSceneMode.Single);
			}
		}
	}
}
