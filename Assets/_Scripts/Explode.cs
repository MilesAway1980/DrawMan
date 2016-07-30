using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	public GameObject explosionPrefab;
	public float expandRate;
	public float endingDiameter;
	public float startingDiameter;

	GameObject explosion;

	float currentDiameter;
	bool start;

	// Use this for initialization
	void Start () {
		start = false;
	}

	void OnDestroy() {
		start = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			
			currentDiameter += expandRate;
			explosion.transform.localScale = new Vector2 (
				currentDiameter,
				currentDiameter
			);

			if (currentDiameter > endingDiameter) {
				Destroy (explosion);
				start = false;
			}
		}

		//print (currentDiameter);
	}

	public bool isStarted() {
		return start;
	}

	public void startExplosion() {
		
		explosion = (GameObject)Instantiate (explosionPrefab);
		currentDiameter = startingDiameter;
		explosion.transform.position = transform.position;
		start = true;

	}

	public bool isFinished() {		
		if (explosion == null) {
			return true;
		}

		return false;
	}
}
