using UnityEngine;
using System.Collections;

public class MGRandom : MoveGenerator {

	public override Vector3 GetNextDestination ()
	{
		return transform.position + new Vector3 (Random.value - 0.5f, 0, Random.value - 0.5f).normalized * generateDistance;
	}

}
