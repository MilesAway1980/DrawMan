using UnityEngine;
using System.Collections;

public class SpikeMultiplier : MonoBehaviour {

	public int numSpikes;
	public GameObject spike;

	GameObject[] spikes;

	// Use this for initialization
	void Start () {

		float angle = 360.0f / (float)numSpikes;
		float curAngle = angle;

		spikes = new GameObject[numSpikes];

		for (int i = 0; i < (numSpikes - 1); i++) {

			GameObject newSpike = (GameObject)Instantiate (spike);
			newSpike.transform.rotation = Quaternion.Euler (
				new Vector3(
					0, 0, curAngle
				)
			);

			newSpike.transform.parent = gameObject.transform;
			newSpike.transform.position = transform.position;
			newSpike.transform.localScale = Vector3.one;
			curAngle += angle;
			spikes [i] = newSpike;
		}
	}
}
