using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pheromone : MonoBehaviour {

	[System.Serializable]
	public struct PheroPath {
		public Vector3 destination;
		public float power;
	}

//	public float Power { get; private set; }
//	public Vector3 nextDestination;
	public List<PheroPath> pheroPaths = new List<PheroPath>();

	public void Add(Vector3 destination, float value) {
		for (int i = 0; i < pheroPaths.Count; i++) {
			if (destination.IsClose(pheroPaths[i].destination)) {
				PheroPath pp = pheroPaths[i];
				pp.power += value;
				pheroPaths[i] = pp;
				return;
			}
		}

		pheroPaths.Add(new PheroPath { destination = destination, power = value });
//		Power += value;
	}

//	public void SetDestination(Vector3 pos) {
//		nextDestination = pos;
//		transform.rotation = Quaternion.LookRotation(nextDestination, Vector3.up);
//	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		for (int i = 0; i < pheroPaths.Count; i++) {
			Gizmos.DrawLine(transform.position, pheroPaths[i].destination);
		}
	}
}
