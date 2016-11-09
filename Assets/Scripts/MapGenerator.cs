using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public City prefabCity;
	public OptimizationAnt prefabAnt;
	public Map map;

	public Vector3 from, to;
	public int amount;

	void Awake() {
		Generate();
	}

	private void Generate() {
		map.cities = new City[amount];
		for (int i = 0; i < amount; i++) {
			Vector3 position = new Vector3(Random.Range(from.x, to.x), Random.Range(from.y, to.y), Random.Range(from.z, to.z));
			City city = Instantiate(prefabCity, position, Quaternion.identity) as City;
			map.cities[i] = city;
		}

		List<OptimizationAnt> ants = new List<OptimizationAnt>();
		for (int i = 0; i < 50; i++) {
			ants.Add(Instantiate(prefabAnt) as OptimizationAnt);
		}
		map.ants = ants;
	}
}
