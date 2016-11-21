using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Anthill : MonoBehaviour {

	public Ant prefabAnt;

	public bool generate;
	public List<GameObject> sugarPeaces = new List<GameObject>();

	void Start() {
		for (int i = 0; i < 10; i++) {
			CreateAnt();
		}
	}

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

	public void AddSugarPeace(GameObject sugarPeace) {
		sugarPeaces.Add(sugarPeace);
		sugarPeace.transform.SetParent(transform);
		sugarPeace.transform.localPosition = Vector3.zero;
	}
}
