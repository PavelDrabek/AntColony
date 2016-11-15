using UnityEngine;
using System.Collections;

public abstract class MoveGenerator : MonoBehaviour {
	public float generateDistance;

	public abstract Vector3 GetNextDestination();
}
