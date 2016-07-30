using UnityEngine;
using System.Collections;

public class LockedJoint2D : MonoBehaviour {

	public GameObject linkedObject;

	public float minDistance;
	public float maxDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float currentDistance = Vector2.Distance (transform.position, linkedObject.transform.position);

		if (currentDistance > maxDistance) {
			
			float angle = Angle.getAngle(linkedObject.transform.position, transform.position);
			//print (currentDistance + " " + angle);

			Vector2 newPos = new Vector2 (
				linkedObject.transform.position.x + Mathf.Sin(angle / Mathf.Rad2Deg) * maxDistance,
				linkedObject.transform.position.y + Mathf.Cos(angle / Mathf.Rad2Deg) * maxDistance
			);

			//float newAngle = Angle.getAngle (linkedObject.transform.position, newPos);
			//print (angle + " " + newAngle);
			//transform.position = newPos;
			/*transform.position = new Vector2 (
				linkedObject.transform.position.x + Mathf.Cos(angle / Mathf.Rad2Deg) * maxDistance,
				linkedObject.transform.position.y + Mathf.Sin(angle / Mathf.Rad2Deg) * maxDistance
			);*/
		}
	}
}
