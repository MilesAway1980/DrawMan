using UnityEngine;
using System.Collections;

public class Angle  {

	public static float getAngle(Vector2 from, Vector2 to)
	{
		if ((from.y - to.y) == 0) {
			if (from.x < to.x) {
				return 90;
			} else if (from.x > to.x) {
				return 270;
			} else {
				return -1;
			}
		}

		float ang = Mathf.Atan ((from.x - to.x) / (from.y - to.y)) * Mathf.Rad2Deg;

		if (from.y > to.y) {
			ang += 180;
		} else {
			if (from.x > to.x) {
				ang += 360;
			}
		}

		return ang;
	}

	/*
	 * Returns an array of vectors that find the four corners of a rotated rectangle
	*/
	public static Vector2[] getCorners (Transform original) {

		Vector2 center = original.position;
		float rotation = original.rotation.eulerAngles.z;

		float width = original.localScale.x / 2;
		float height = original.localScale.y / 2;

		Vector2[] final = new Vector2[4];

		for (int i = 0; i < 4; i++) {

			Vector2 endPoint = new Vector2();

			switch (i) {
			case 0: endPoint = new Vector2(center.x + width, center.y + height); break;
			case 1: endPoint = new Vector2(center.x - width, center.y + height); break;
			case 2: endPoint = new Vector2(center.x - width, center.y - height); break;
			case 3: endPoint = new Vector2(center.x + width, center.y - height); break;
			}


			float angle = rotation + 90 + Angle.getAngle(center, endPoint);
			float dist = Vector2.Distance (center, endPoint);

			final [i].x = center.x + Mathf.Cos (angle / Mathf.Rad2Deg) * dist;
			final [i].y = center.y + Mathf.Sin (angle / Mathf.Rad2Deg) * dist;
		}

		return final;
	}
}
