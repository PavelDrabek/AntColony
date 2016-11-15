using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ant : MonoBehaviour {

	private MoveGenerator moveGenerator;
	private MoveValidator moveValidator;
	private Move move;


	public Anthill anthill;
	public Pheromone prefabPheromone;

	public float nextPathDistance;
	public List<Vector3> positionHistory;

	public bool isReturning = false;
	public GameObject sugarPeace = null;

	void Awake() {
		moveGenerator = GetComponent<MoveGenerator>();
		moveValidator = GetComponent<MoveValidator>();
		move = GetComponent<Move>();
		isReturning = false;
	}

	public void Init(Anthill anthill) {
		this.anthill = anthill;
		moveValidator.Init();
		move.OnMoveFinish += OnMoveFinish;
		SetNextDestination ();
	}

	private void OnMoveFinish(Vector3 position) {
		if(isReturning) {
			if(positionHistory.Count > 0) {
				Pheromone p = Instantiate(prefabPheromone, position, Quaternion.identity) as Pheromone;
				p.SetDestination(positionHistory[positionHistory.Count - 1]);
				positionHistory.RemoveAt(positionHistory.Count - 1);
			}
		} else {
			positionHistory.Add (position);
		}
		SetNextDestination ();
	}

	public void SetNextDestination() {
		if(isReturning) {
			if(positionHistory.Count > 1) {
				move.SetDestination(positionHistory[positionHistory.Count - 2]);
			}
			return;
		}

		int repetition = 0;
		Vector3 nextDestinaton;
		do {
			if(repetition > 10) {
				nextDestinaton = (anthill.transform.position - transform.position).normalized * nextPathDistance;
				break;
			}
			nextDestinaton = moveGenerator.GetNextDestination();
			repetition++;
		} while (!moveValidator.CanMove(nextDestinaton));

		move.SetDestination (nextDestinaton);
	}

	void OnTriggerEnter(Collider c) {
		if(c.gameObject.CompareTag("sugar")) {
			isReturning = true;
			sugarPeace = c.gameObject.GetComponent<Sugar>().GetPeace();
			SetNextDestination();
		} else if(c.gameObject.CompareTag("pheromone")) {
			Pheromone p = c.gameObject.GetComponent<Pheromone>();
//			p.Add(QVAL/ distanceSum);
		} else if(c.gameObject.CompareTag("anthill")) {
			isReturning = false;
			positionHistory.Clear();
			SetNextDestination();
		}
	}
}
