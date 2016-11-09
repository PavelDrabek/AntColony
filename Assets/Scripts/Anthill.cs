using UnityEngine;
using System.Collections;

public class Anthill : MonoBehaviour {

	public OptimizationAnt prefabAnt;

	public void CreateAnt() {
		Instantiate (prefabAnt, transform.position, Quaternion.Euler (0, Random.value * 360, 0));
	}
}
