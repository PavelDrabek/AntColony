using UnityEngine;
using System.Collections;

public static class Vector3Extension {

	public static bool IsClose(this Vector3 a, Vector3 b) {
		return Vector3.Distance(a, b) < 0.1f;
	}
}
