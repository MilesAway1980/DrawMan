using UnityEngine;
using System.Collections;

public class LockDoor : MonoBehaviour {

	public enum MoveDirection {Vertical, Horizontal};

	[Tooltip("The object that rotates to open the door, such as a gear, lever, etc.")]
	public GameObject mechanism;
	[Tooltip("How far does the door open.")]
	public int doorRange;
	[Tooltip("Which direction does the door move when the mechanism is turned.")]
	public MoveDirection doorDirection; 


	float doorStartLocation;
	Lock doorlock;

	// Use this for initialization
	void Start () {
		if (doorDirection == MoveDirection.Vertical) {
			doorStartLocation = transform.position.y;
		} else {
			doorStartLocation = transform.position.x;
		}
		doorlock = mechanism.GetComponent<Lock>();

	}
	
	// Update is called once per frame
	void Update () {

		if (doorlock.isMoving()) {			
			
			float doorOpen = doorlock.getRatio();

			if (doorOpen > 1) {
				doorOpen = 1;
			} else if (doorOpen < 0) {
				doorOpen = 0;
			}

			if (doorDirection == MoveDirection.Vertical) {
				transform.position = new Vector2 (
					transform.position.x,
					doorStartLocation + doorOpen * doorRange
				);
			} else {
				transform.position = new Vector2 (					
					doorStartLocation + doorOpen * doorRange,
					transform.position.y
				);
			}
		}
	}
}
