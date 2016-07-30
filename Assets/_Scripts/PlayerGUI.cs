using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGUI : MonoBehaviour {

	public Text drawText;
	public Text hitPointText;
	public Button resetDraw;

	HitPoints hp = null;
	Equipment eq = null;

	Player player = null;

	// Use this for initialization
	void Start () {
		resetDraw.onClick.AddListener (
			() => {
				eq.resetAllDraw ();
			}
		);
	}
	
	// Update is called once per frame
	void Update () {

		if (player == null) {
			player = FindObjectOfType<Player> ();
			hp = player.GetComponent<HitPoints> ();
			eq = player.GetComponent<Equipment> ();
		}

		if (drawText != null) {
			drawText.text = "Draw: " + (int)eq.getDrawCurrent() + " / " + eq.getDrawAbility();
		}

		if (hitPointText != null) {
			hitPointText.text = "Hit Points: " + (int)hp.getCurrentHitPoints() + " / " + hp.getMaxHitPoints();
		}
	}
}
