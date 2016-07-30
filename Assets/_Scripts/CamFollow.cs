using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public GameObject target;
	public float zoomDistance;
	public float distanceAbove;
	public float zoomSpeed;

	float zoomedDistanceAbove;

	[Range(1, 100)]
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		zoomedDistanceAbove = distanceAbove;
		transform.position = new Vector3 (
			target.transform.position.x,
			target.transform.position.y + zoomedDistanceAbove,
			-zoomDistance
		);

	}
	
	// Update is called once per frame
	void Update () {
		float camX = transform.position.x;
		float camY = transform.position.y;
		float targetX = target.transform.position.x;
		float targetY = target.transform.position.y + zoomedDistanceAbove;

		if (camX != targetX) {
			camX += (targetX - camX) / (10000 / (moveSpeed * moveSpeed));		
		}

		if (camY != targetY) {
			camY += (targetY - camY) / (10000 / (moveSpeed * moveSpeed));
		}

		transform.position = new Vector3 (
			camX,
			camY,
			-zoomDistance
		);

		float mouseScroll = Input.GetAxis ("Mouse ScrollWheel");
		if (mouseScroll != 0) {
			Camera.main.orthographicSize -= (mouseScroll * zoomSpeed);
			zoomedDistanceAbove = distanceAbove * (float)(Camera.main.orthographicSize / 100);
		}
	}

	public void setTarget(GameObject newTarget) {
		target = newTarget;	
	}
}
