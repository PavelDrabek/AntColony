using UnityEngine;
using System.Collections;

public abstract class MoveGenerator : MonoBehaviour {
	public float generateDistance;
	protected Ant ant;

	public virtual void Init(Ant ant) {
		this.ant = ant;
	}

	public abstract Vector3 GetNextDestination();
}
