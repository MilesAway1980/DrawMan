using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mouse : MonoBehaviour {
	
	List<GameObject> items;

	public GameObject originalDot;
	public GameObject originalLine;
	public float dotDistance;

	float drawAmount;

	GameObject container;

	bool firstHeld = true;
	bool[] mouseButton;

	// Use this for initialization
	void Start () {		
		mouseButton = new bool[5];
		container = new GameObject ();
		container.name = "Shapes Container";
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = 10;
		Vector3 mouseScreen = Camera.main.ScreenToWorldPoint (mousePos2D);

		for (int i = 0; i < 5; i++) {
			mouseButton[i] = Input.GetMouseButton (i);
		}

		Equipment eq = gameObject.GetComponent<Equipment> ();

		bool blocked = false;

		if (mouseButton [0] && eq.getDrawCurrent () > 1) {
			NoDrawZone[] zones = FindObjectsOfType<NoDrawZone> ();
			for (int i = 0; i < zones.Length; i++) {
				Collider2D col = zones [i].GetComponent<Collider2D> ();
				if (col.OverlapPoint (mouseScreen)) {
					blocked = true;
				}
			}
		}

		if (mouseButton [0] && eq.getDrawCurrent() > 1 && blocked == false) {

			if (firstHeld) {
				firstHeld = false;
				items = new List<GameObject> ();
				drawAmount = 0;
			}

			bool tooClose = false;

			foreach (GameObject item in items) {

				float dist = Vector3.Distance (item.transform.position, mouseScreen);

				if (dist < dotDistance) {
					tooClose = true;
				}
			}

			if (tooClose == false) {
				
				GameObject dot = Instantiate (originalDot);

				/*
				 * 		If it is the first dot, place it at the mouse pointer.
				 * 		Otherwise, place it at a specified direction from the previous
				 * 		in the direction of the mouse.
				*/

				dot.transform.position = mouseScreen;

				if (items.Count > 0) {

					GameObject line = Instantiate (originalLine);
					GameObject prev = items [items.Count - 1];

					float distance, angle;

					distance = Vector3.Distance (prev.transform.position, dot.transform.position);
					angle = Angle.getAngle (prev.transform.position, dot.transform.position);

					float currentDraw = eq.getDrawCurrent ();
					if (distance > currentDraw) {
						dot.transform.position = new Vector2 (
							prev.transform.position.x + Mathf.Sin (angle / Mathf.Rad2Deg) * currentDraw,
							prev.transform.position.y + Mathf.Cos (angle / Mathf.Rad2Deg) * currentDraw
						);
						distance = currentDraw;
					}

					drawAmount += distance;

					Vector3 centerPoint = Vector3.Lerp (prev.transform.position, dot.transform.position, 0.5f);
					line.transform.position = centerPoint;
					line.transform.localScale = new Vector3 (2.0f, distance, 2.0f);
					line.transform.rotation = Quaternion.Euler (
						new Vector3 (0, 0, -angle)
					);

					eq.useDraw (distance);

					items.Add (line);

				} else {
					eq.useDraw (1);
					drawAmount += 1;
				}

				items.Add (dot);
			}

			
		} else {
			if (items != null) {
				if (items.Count > 0) {

					GameObject newObject = new GameObject ();
					DrawnObject drawn = newObject.AddComponent<DrawnObject> ();

					newObject.transform.parent = container.transform;
					drawn.makeObject (items);
					drawn.setOwner (gameObject);

					drawn.setDrawAmount (drawAmount);

					items = null;
					firstHeld = true;
				}
			}
		}

		if (mouseButton[4]) {	//Erase drawn object
			Collider2D col = Physics2D.OverlapPoint(mouseScreen);
			//print (col);
			if (col) {				
				if (col.gameObject.tag == "Drawn Object") {
					//Destroy (col.gameObject);

					DrawnObject dob = col.GetComponentInParent<DrawnObject>();
					//print (dob);
					if (dob != null) {
						Destroy (dob.gameObject);
					}
				}
			}
		}
	}
}
