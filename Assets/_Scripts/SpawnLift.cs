using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Platform {
	public GameObject platform = null;
	public int currentTarget = 0;
	public float distance = 0;
}

public class SpawnLift : MonoBehaviour {

	public BoxCollider2D[] activateRegion;
	public BoxCollider2D[] deactivateRegion;

	public GameObject[] wayPoints;

	public float respawnRate;
	public float moveSpeed;

	float[] distance;
	float[] angle;

	public GameObject platform;

	float startTime;
	float currentTime;

	List<Platform> platformList;

	public bool active;

	public Player player;

	// Use this for initialization
	void Start () {
		startTime = Time.fixedTime;
		platformList = new List<Platform> ();

		if (activateRegion.Length == 0 && deactivateRegion.Length == 0) {
			active = true;
		}

		/*	
		 * No Spawn Lifts should only have a single waypoint.  
		 * If so, destroy it for its insolence
		*/
		if (wayPoints.Length <= 1) {
			Destroy (this);
		}

		int lengths = wayPoints.Length - 1;
		distance = new float[lengths];
		angle = new float[lengths];
		for (int i = 0; i < lengths; i++) {
			distance [i] = Vector2.Distance (wayPoints[i].transform.position, wayPoints[i + 1].transform.position);
			angle [i] = Angle.getAngle (wayPoints [i].transform.position, wayPoints [i + 1].transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			currentTime = Time.fixedTime;

			if ((currentTime - startTime) > respawnRate) {
				startTime = Time.fixedTime;
				GameObject newObject = (GameObject)Instantiate (platform);
				newObject.transform.position = transform.position;

				Platform newPlatform = new Platform ();
				newPlatform.platform = newObject;
				newPlatform.currentTarget = 1;

				platformList.Add (newPlatform);
			}

			if (platformList != null) {
				for (int i = 0; i < platformList.Count; i++) {

					Platform current = platformList [i];

					bool good = false;

					Vector2 waypointPrev = new Vector2 ();
					Vector2 waypointNext = new Vector2 ();

					while (!good) {
						if (current != null) {
							waypointPrev = wayPoints [current.currentTarget - 1].transform.position;
							waypointNext = wayPoints [current.currentTarget].transform.position;

							float moveDist = moveSpeed / distance [current.currentTarget - 1];
							current.distance += moveDist;

							if (current.distance > 1) {
								current.distance = 0;
								current.currentTarget++;
								if (current.currentTarget >= wayPoints.Length) {
									platformList.Remove (current);
									Destroy (current.platform);
									current = null;
									good = true;
								}
							} else {
								good = true;
							}
						}
					}

					if (current != null) {
						current.platform.transform.position = Vector2.Lerp (waypointPrev, waypointNext, current.distance);
					}
				}
			}

			if (deactivateRegion.Length != 0 && player != null) {
				for (int i = 0; i < deactivateRegion.Length; i++) {
					BoxCollider2D region = deactivateRegion [i];
					if (region.OverlapPoint (player.transform.position)) {
						active = false;
					}
				}
			}

		} else {
			if (platformList.Count > 0) {
				for (int i = 0; i < platformList.Count; i++) {					
					Platform current = platformList [i];
					platformList.Remove (current);
					Destroy (current.platform);
				}
			}

			if (activateRegion.Length != 0 && player != null) {
				for (int i = 0; i < activateRegion.Length; i++) {
					BoxCollider2D region = activateRegion [i];
					if (region.OverlapPoint (player.transform.position)) {						
						active = true;
					}
				}
			}
		}
	}
}
