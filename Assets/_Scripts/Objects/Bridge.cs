using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {

	public GameObject leftSideObject;
	public GameObject rightSideObject;

	public GameObject segmentObject;

	public int segments;
	[Tooltip("Calculate the width of each bridge segment to automatically fit.")]
	public bool autoSegmentWidth;
	[Tooltip("The width of each bridge segment.")]
	public float segmentWidth;
	public float segmentHeight;

	public float strength;

	[Tooltip("How resistant each segment is to moving.")]
	public float moveResistance;

	float distance;

	// Use this for initialization
	void Start () {

		Vector2 segmentDistance = new Vector2 ();
		Vector2 pos = new Vector2 ();
		
		distance = Vector3.Distance (leftSideObject.transform.position, rightSideObject.transform.position);
		segmentDistance.x = (rightSideObject.transform.position.x - leftSideObject.transform.position.x) / segments;
		segmentDistance.y = (rightSideObject.transform.position.y - leftSideObject.transform.position.y) / segments;

		if (autoSegmentWidth) {
			segmentWidth = distance / segments;
		}

		GameObject previous = leftSideObject;
		pos.x = leftSideObject.transform.position.x + segmentDistance.x / 2;
		pos.y = leftSideObject.transform.position.y;

		DistanceJoint2D dj = null;

		for (int i = 0; i < segments; i++) {
			GameObject newLink = null;
			if (segmentObject != null) {
				newLink = (GameObject)Instantiate (segmentObject);
			} else {
				newLink = (GameObject)Instantiate (GameObject.CreatePrimitive (PrimitiveType.Cube));
			}

			newLink.transform.parent = transform;

			newLink.transform.localScale = new Vector3 (
				segmentWidth,
				segmentHeight,
				1
			);

			newLink.transform.position = new Vector3 (
				pos.x,
				pos.y,
				0
			);

			Rigidbody2D rb = newLink.GetComponent<Rigidbody2D> ();
			if (rb == null) {
				rb = newLink.AddComponent<Rigidbody2D> ();
			}

			rb.mass = strength;
			rb.angularDrag = 100000;
			rb.drag = 10;

			dj = previous.AddComponent<DistanceJoint2D> ();
			dj.maxDistanceOnly = false;
			dj.connectedBody = rb;

			pos.x += segmentDistance.x;
			pos.y += segmentDistance.y;
			previous = newLink;
		}

		dj = previous.AddComponent<DistanceJoint2D>();
		dj.maxDistanceOnly = false;
		dj.connectedBody = rightSideObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
