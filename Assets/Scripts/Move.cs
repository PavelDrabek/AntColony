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
			Debug.Log ("velocity: " + velocity);
			if (OnMoveFinish != null) {
				OnMoveFinish(destination);
			}
		}
		transform.position += (velocity.normalized * speed * Time.deltaTime);
	}

	public void SetDirection(Vector3 direction) {
		Debug.Log ("direction: " + direction);
		destination = transform.position + direction;
		transform.LookAt (destination, Vector3.up);
	}
}
