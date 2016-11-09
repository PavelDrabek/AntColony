using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ant : MonoBehaviour {

	public Move move;

	public float nextPathDistance;
	public List<Vector3> positionHistory;


	void Start() {
		move.OnMoveFinish += OnMoveFinish;
		SetRandomMove ();
	}

	private void OnMoveFinish(Vector3 position) {
		positionHistory.Add (position);
		SetRandomMove ();
	}

	public void SetRandomMove() {
		move.SetDirection (new Vector3 (Random.value, 0, Random.value).normalized * nextPathDistance);
	}
}
