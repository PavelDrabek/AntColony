using UnityEngine;
using System.Collections;

public class MGPheromoneTrace : MoveGenerator {

	public MoveGenerator defaultGenerator;
	public Collider trigger;

	public Pheromone tracingPheromone;

	void Awake() {
		if(trigger == null) {
			trigger = GetComponent<Collider>();
			if(trigger == null) {
				SphereCollider sc = gameObject.AddComponent<SphereCollider>();
				trigger = sc;
				sc.isTrigger = true;
				sc.radius = 2;
			}
		}
	}

	public override void Init (Ant ant)
	{
		base.Init (ant);
		defaultGenerator.Init(ant);
	}

	public override Vector3 GetNextDestination ()
	{
		if(tracingPheromone == null) {
			return defaultGenerator.GetNextDestination();
		}

		return tracingPheromone.nextDestination;
	}

	void OnTriggerEnter(Collider c) {
		if(c.gameObject.CompareTag("pheromone")) {
			tracingPheromone = c.gameObject.GetComponent<Pheromone>();
			//			p.Add(QVAL/ distanceSum);
		}
	}

	void OnTriggerExit(Collider c) {
		if(tracingPheromone.gameObject.Equals(c.gameObject)) {
			tracingPheromone = null;
		}
	}
}
