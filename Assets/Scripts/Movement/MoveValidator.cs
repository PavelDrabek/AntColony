using UnityEngine;
using System.Collections;

public abstract class MoveValidator : MonoBehaviour {

	public abstract void Init();
	public abstract bool CanMove(Vector3 destination);

}
