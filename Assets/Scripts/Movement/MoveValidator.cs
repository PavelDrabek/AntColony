using UnityEngine;
using System.Collections;

public abstract class MoveValidator : MonoBehaviour {

	protected Ant ant;

	public virtual void Init(Ant ant) {
		this.ant = ant;
	}

	public abstract bool CanMove(Vector3 destination);

}
