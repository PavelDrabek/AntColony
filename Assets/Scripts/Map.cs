using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// https://en.wikipedia.org/wiki/Ant_colony_optimization_algorithms

public class Map : MonoBehaviour {

	public float ALPHA = 1.0f; // [0 or greater]
	public float BETA = 5.0f; // this parameter raises the weight of distance over pheromone [1 or greater]
	public float RHO = 0.5f; // evapouration rate
	public float QVAL = 100f; // constant for pheromone update

	public float initPheromone;
	public float speed;

	public City[] cities;
	public int Count { get { return cities.Length; } }

	public List<Ant> ants;

	private float[][] distances;
	private float[][] pheromones;

	public float bestDistance = float.MaxValue;

	void Start() {
		distances = new float[Count][];
		pheromones = new float[Count][];
		for (int i = 0; i < Count; i++) {
			distances[i] = new float[Count];
			pheromones[i] = new float[Count];
		}

		Init();
	}

	public void Init() {
		initPheromone = 1.0f / Count;

		for (int a = 0; a < Count; a++) {
			for (int b = a; b < Count; b++) {
				pheromones[a][b] = pheromones[b][a] = initPheromone;

				if(a == b) {
					distances[a][b] = distances[b][a] = 0;
				} else {
					distances[a][b] = distances[b][a] = Vector3.Distance(cities[a].Position, cities[b].Position);
				}

//				Debug.Log("distance: " + distances[a][b]);

			}
		}
			
		restartAnts();
	}

	void Update() {
		bool allFinished = true;
		for (int i = 0; i < ants.Count; i++) {
			if(ants[i].Move(speed * Time.deltaTime)) {
				allFinished = false;
			}
			ants[i].UpdatePosition();
		}

		if(allFinished) {
			CheckBestPath();
			updateTrails();
			restartAnts();
			Debug.Log("best: " + bestDistance);
		} else {
//			Debug.Log("movin");
		}
	}

	public float Distance(int a, int b) {
		return distances[a][b];
	}

	public float Pheromone(int a, int b) {
		return pheromones[a][b];
	}

	public float AntProduct(int from, int to) {
		float phe = Pheromone(from, to);
		float dist = Distance(from, to);
		float a = Mathf.Pow(phe, ALPHA);
		float b = Mathf.Pow((1.0f / dist), BETA);

		return( a * b);
	}

	void updateTrails() {
		int from, to, i, ant;

		//Pheromone Evaporation

		for(from=0; from<Count;from++)
		{
			for(to=0;to<Count;to++)
			{
				if(from!=to)
				{
					pheromones[from][to] *= (1f - RHO);

					if(pheromones[from][to] < 0f)
					{
						pheromones[from][to] = initPheromone;
					}
				}
			}
		}

		//Add new pheromone to the trails

		for(ant = 0; ant < ants.Count; ant++)
		{
			for(i = 0; i < Count; i++)
			{	
				if(i < Count - 1)
				{
					from = ants[ant].paths[i];
					to = ants[ant].paths[i+1];
				}
				else
				{
					from = ants[ant].paths[i];
					to = ants[ant].paths[0];
				}

				pheromones[from][to] += (QVAL/ ants[ant].distanceSum);
				pheromones[to][from] = pheromones[from][to];
			}
		}

		for (from = 0; from < Count; from++)
		{
			for(to = 0; to < Count; to++)
			{
				pheromones[from][to] *= RHO;
			}
		}

	}

	void CheckBestPath() {
		int bestAnt = -1;
		for(int ant = 0; ant < ants.Count; ant++) {
			if(ants[ant].distanceSum < bestDistance) {
				bestDistance = ants[ant].distanceSum;
				bestAnt = ant;
			}
		}
		if(bestAnt != -1) {
			DrawPath(ants[bestAnt]);
		}
	}

	void restartAnts(bool checkBestDistance = false)
	{
		int to = 0;

		for(int ant = 0; ant < ants.Count; ant++) {
			to = (to + 1) % Count;

			ants[ant].Init(this);
			ants[ant].Reset(to++);
			ants[ant].UpdatePosition();
		}
	}

	private void DrawPath(Ant ant) {
		LineRenderer line = GetComponent<LineRenderer>();
		line.SetVertexCount(Count + 1);
		for (int i = 0; i < Count; i++) {
			line.SetPosition(i, cities[ant.paths[i]].Position);
		}
		line.SetPosition(Count, cities[ant.paths[0]].Position);
	}
}
