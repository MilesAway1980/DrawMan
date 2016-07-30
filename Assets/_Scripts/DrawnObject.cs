using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawnObject : MonoBehaviour {

	float drawAmount;
	static int drawnObjects;
	GameObject owner;

	// Use this for initialization
	void Start () {
		drawnObjects++;
		gameObject.tag = "Drawn Object";
	}

	void OnDestroy() {
		if (owner != null) {
			Equipment eq = owner.GetComponent<Equipment> ();
			drawnObjects--;

			if (eq) {
				if (drawnObjects == 0) {			
					eq.refreshDraw (int.MaxValue);
				} else {
					eq.refreshDraw (drawAmount);
				}
			}
		}
	}

	public static int getDrawnObjects() {
		return drawnObjects;
	}

	public void setOwner(GameObject newOwner) {
		owner = newOwner;
	}
	
	// Update is called once per frame
	void Update () {

		float dist = Vector2.Distance (
			owner.transform.position,
			transform.position
		);

		if (dist > 500) {			
			Destroy (this.gameObject);
		}	
	}

	public float getDrawAmount () {
		return drawAmount;
	}

	public void setDrawAmount(float newDrawAmount) {
		drawAmount = newDrawAmount;
		Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D> ();
		rb.mass = drawAmount;
	}

	public void makeObject(List<GameObject> items) {

		if (items != null) {		

			foreach (GameObject item in items) {

				CircleCollider2D itemCC = item.GetComponent<CircleCollider2D> ();
				BoxCollider2D itemBC = item.GetComponent<BoxCollider2D> ();
				item.tag = "Drawn Object";

				if (itemCC != null) {
					itemCC.enabled = true;
				}

				if (itemBC != null) {
					itemBC.enabled = true;
				}				

				item.transform.parent = gameObject.transform;
			}
			//gameObject.AddComponent<ShowMovementInfo> ();
			items = null;

			//gameObject.name = "Drawn Object";
		} else {
			Destroy (this);
		}

	}
}
