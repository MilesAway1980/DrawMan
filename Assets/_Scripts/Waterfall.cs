using UnityEngine;
using System.Collections;

public class Waterfall : MonoBehaviour {

	public GameObject droplet;
	public float width;
	public float height;
	public int count;

	GameObject[] droplets;

	// Use this for initialization
	void Start () {
		droplets = new GameObject[count];

		for (int i = 0; i < count; i++) {
			droplets [i] = (GameObject)Instantiate (droplet);

			float newPosX = Random.Range (-width / 2, width / 2);
			float newPosY = Random.Range (-height / 2, height / 2);

			droplets [i].transform.position = new Vector3 (
				transform.position.x + newPosX,
				transform.position.y + newPosY,
				transform.position.z
			);
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
