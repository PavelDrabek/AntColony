using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DestinationType { EMPTY, PHEROMONE, SUGAR, ANTHILL };

public class Move : MonoBehaviour {

	public delegate void MoveDelegate(Vector3 position, DestinationType type);
	public MoveDelegate OnMoveFinish;

	public float speed;
	public Vector3 destination;
	public DestinationType type;

	void Update () {
		Vector3 velocity = destination - transform.position;
		if (velocity.magnitude < speed * Time.deltaTime) {
			if (OnMoveFinish != null) {
				OnMoveFinish(destination, type);
			}
		}
		transform.position += (velocity.normalized * speed * Time.deltaTime);
	}

	public void SetDestination(Vector3 destination, DestinationType type) {
		this.destination = destination;
		this.type = type;
		transform.LookAt (destination, Vector3.up);
	}

}
