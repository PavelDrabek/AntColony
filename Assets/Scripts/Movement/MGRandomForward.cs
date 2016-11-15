using UnityEngine;
using System.Collections;

public class MGRandomForward : MoveGenerator {


	public override Vector3 GetNextDestination ()
	{
		Vector3 dir = new Vector3 (Random.value - 0.5f, 0, Random.value - 0.5f).normalized;
		if(Vector3.Dot(dir, transform.forward) < 0) {
//			Debug.Log("Dot = " + Vector3.Dot(dir, transform.forward) + ", dir: " + dir + ", forward: " + transform.forward);
			dir = -dir;
		}
		return transform.position + dir * generateDistance;
	}
}
