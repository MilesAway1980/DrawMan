using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectGenerator : MonoBehaviour {

	public GameObject spawnObject;
	public float spawnTime;
	public float objectSize;
	public float objectMass;
	public bool limitSpawns;
	public int limit;

	public float spawnForce;
	public float spawnAngleMin;
	public float spawnAngleMax;

	float startTime;

	List<GameObject> list;

	BoxCollider2D[] destroyZones;

	// Use this for initialization
	void Start () {
		list = new List<GameObject> ();
		startTime = Time.fixedTime;

		destroyZones = GetComponents<BoxCollider2D> ();

		if (spawnAngleMax < spawnAngleMin) {
			spawnAngleMax += 360;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnObject == null) {
			return;
		}

		GameObject[] temp = list.ToArray();

		for (int i = 0; i < destroyZones.Length; i++) {
			for (int j = 0; j < temp.Length; j++) {
				if (temp [j] == null) {
					list.Remove (temp [j]);
					continue;
				}

				if (destroyZones [i].OverlapPoint (temp[j].transform.position)) {
					list.Remove (temp[j]);
					Destroy (temp[j]);
				}
			}
		}

		if (limitSpawns == true) {
			if (list.Count >= limit) {
				return;
			}
		}

		float curTime = Time.fixedTime;
		if ((curTime - startTime) >= spawnTime) {
			startTime = curTime;

			GameObject newObject = (GameObject)Instantiate (spawnObject);
			newObject.transform.localScale = new Vector3 (
				objectSize, objectSize, objectSize
			);

			Rigidbody2D rb = newObject.GetComponent<Rigidbody2D> ();
			if (rb != null) {
				rb.mass = objectMass;
				if (spawnForce > 0) {
					float spawnAngle = Random.Range (spawnAngleMin, spawnAngleMax);
					float forceX = Mathf.Sin (spawnAngle / Mathf.Rad2Deg) * spawnForce;
					float forceY = Mathf.Cos (spawnAngle / Mathf.Rad2Deg) * spawnForce;
					rb.AddForce (new Vector2 (forceX, forceY));
				}
			}

			newObject.transform.position = transform.position;
			newObject.transform.parent = transform;

			list.Add (newObject);
		}
	}
}
