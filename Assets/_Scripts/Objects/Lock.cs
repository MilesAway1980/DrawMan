using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour {

	public virtual float getRatio() {
		return 0;
	}

	public virtual bool isMoving() {
		return false;
	}
}
