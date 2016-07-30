using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Equipment : MonoBehaviour {

	int drawAbility;
	float drawCurrent;

	// Use this for initialization
	void Awake () {
		drawAbility = 0;
		drawCurrent = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float getDrawCurrent() {
		return drawCurrent;
	}

	public int getDrawAbility() {
		return drawAbility;
	}

	public void resetAllDraw() {
		DrawnObject[] drawn = FindObjectsOfType<DrawnObject> ();
		for (int i = 0; i < drawn.Length; i++) {
			Destroy (drawn [i].gameObject);
		}
	}

	public bool useDraw(float amount) {		
		if (amount <= drawCurrent) {
			drawCurrent -= amount;
			return true;
		}

		return false;
	}

	public void refreshDraw (float amount) {
		if (amount > 0) {
			drawCurrent += amount;
			if (drawCurrent > drawAbility) {
				drawCurrent = drawAbility;
			}
		}
	}

	public void addItem(int itemType, float amount) {		
		switch (itemType) {
			case 1:
				drawAbility += (int)amount;
				drawCurrent += (int)amount;				
				break;
		}
	}
}
