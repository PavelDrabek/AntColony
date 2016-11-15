using UnityEngine;
using System.Collections;

public class OptimizationAnt : MonoBehaviour {

	public Transform myTransform;

	public int actualCity;
	public int nextCity;
	public int pathIndex;

	public bool[] visited;
	public int[] paths;
	public float distanceSum;
	public float distanceAct;

	Map map;

	public void Init(Map map) {
		this.map = map;
		visited = new bool[map.Count];
		paths = new int[map.Count];
	}

	public void Reset(int city) {
		nextCity = -1;
		distanceSum = 0f;
		distanceAct = 0;
		pathIndex = 0;

		for(int i = 0; i < map.Count; i++) {
			visited[i] = false;
			paths[i] = -1;
		}

		actualCity = city;
		visited[actualCity] = true;
		paths[pathIndex++] = actualCity;

		nextCity = SelectNextCity();
	}

	public bool Move(float step) {
		if(pathIndex < map.Count)
		{
			distanceAct += step;
			distanceSum += step;
			float diff; // = map.Distance(actualCity, nextCity) - distanceAct;

			while((diff = map.Distance(actualCity, nextCity) - distanceAct) <= 0) {
				distanceAct = -diff;
//				Debug.Log("dif: " + diff);
				actualCity = nextCity;
				visited[actualCity] = true;
				paths[pathIndex++] = actualCity;

				//handle last case->last city to first
				if(pathIndex == map.Count) {
					distanceSum += map.Distance(paths[map.Count -1], paths[0]); 
					return false;
				}

				nextCity = SelectNextCity();
			}

			return true;
		}
		return false;
	}

	public void UpdatePosition() {
		if(nextCity < 0) {
			myTransform.position = map.cities[actualCity].Position;
			return;
		}

		myTransform.position = Vector3.Lerp(map.cities[actualCity].Position, map.cities[nextCity].Position, distanceAct / map.Distance(actualCity, nextCity));
	}

	private void SetPosition(Vector3 position) {

	}

	private int SelectNextCity()
	{
		int to = 0;
		float denom = 0;

		for(to = 0; to < map.Count; to++) {
			if(!visited[to]) {
				denom += map.AntProduct(actualCity, to);
			}
		}

		do {
			to = (to + 1) % map.Count;
			if(!visited[to]) {
				float p = map.AntProduct(actualCity, to) / denom;
				if(Random.Range(0f, 1f) < p) {
					break;
				}
			}
		} while(true);

		return to;
	}
}
