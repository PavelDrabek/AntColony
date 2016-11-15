using UnityEngine;
using System.Collections;

public class Pheromone : MonoBehaviour {

	public float Power { get; private set; }
	public Vector3 nextDestination;

	public void Add(float value) {
		Power += value;
	}

	public void SetDestination(Vector3 pos) {
		nextDestination = pos;
		transform.rotation = Quaternion.LookRotation(nextDestination, Vector3.up);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(transform.position, nextDestination);
	}
}
