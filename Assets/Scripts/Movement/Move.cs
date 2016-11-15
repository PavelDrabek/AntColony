using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {

	public delegate void MoveDelegate(Vector3 position);
	public MoveDelegate OnMoveFinish;

	public float speed;
	public Vector3 destination;

	void Update () {
		Vector3 velocity = destination - transform.position;
		if (velocity.magnitude < speed * Time.deltaTime) {
			if (OnMoveFinish != null) {
				OnMoveFinish(destination);
			}
		}
		transform.position += (velocity.normalized * speed * Time.deltaTime);
	}

	public void SetDestination(Vector3 destination) {
		this.destination = destination;
		transform.LookAt (destination, Vector3.up);
	}

}
