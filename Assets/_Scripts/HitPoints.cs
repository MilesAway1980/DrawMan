using UnityEngine;
using System.Collections;

public class HitPoints : MonoBehaviour {

	public int maxHitPoints;
	[Tooltip("How much to divide the damage by when applied. \ndamage = (damage / modifier);")]
	public int modifier;
	[Tooltip("The minimum amount the damage needs to be before it will be applied.")]
	public int minDamage;
	float currentHitPoints;
	bool dead;

	// Use this for initialization
	void Start () {
		currentHitPoints = maxHitPoints;
		if (minDamage < 0) {
			minDamage = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHitPoints <= 0) {
			kill ();
			dead = true;
		} else {
			dead = false;
		}

		if (currentHitPoints > maxHitPoints) {
			revive(100);
		}
	}

	public void increaseMaxHitPoints(int amount) {
		if (amount > 0) {
			maxHitPoints += amount;
		}
	}

	public void decreaseMaxHitPoints(int amount) {
		if (amount > 0) {
			maxHitPoints -= amount;
		}
	}

	public void heal (float amount) {
		if (amount > 0) {
			currentHitPoints += amount;
		}
	}

	public void damage (float amount) {
		amount = (float)(amount / modifier);
		if (amount > minDamage) {
			currentHitPoints -= amount;
		}
	}

	public void revive(float percentage) {
		if (percentage < 0) {
			percentage = 0;
		} else if (percentage > 100) {
			percentage = 100;
		}
		currentHitPoints = (maxHitPoints * percentage);
	}

	public void kill() {
		currentHitPoints = 0;
	}

	public int getMaxHitPoints() {
		return maxHitPoints;
	}

	public float getCurrentHitPoints() {
		return currentHitPoints;
	}

	public bool isDead() {
		return dead;
	}
}
