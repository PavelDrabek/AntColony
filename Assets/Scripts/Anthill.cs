using UnityEngine;
using System.Collections;

public class Anthill : MonoBehaviour {

	public Ant prefabAnt;

	public bool generate;

	void Update() {
		if(generate) {
			generate = false;
			CreateAnt();
		}
	}

	public void CreateAnt() {
		Ant ant = Instantiate (prefabAnt, transform.position, Quaternion.Euler (0, Random.value * 360, 0)) as Ant;
		ant.Init(this);
	}
}
