using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ant : MonoBehaviour {

	public MoveGenerator moveGenerator;
	public MoveValidator moveValidator;
	public MeshRenderer meshRenderer;
	private Move move;


	public Anthill anthill;
	public Pheromone prefabPheromone;

	public float defaultPheromoneValue;
	public float nextPathDistance;
	public List<Vector3> positionHistory;

	public bool isReturning = false;
	public GameObject sugarPeace = null;
	public Pheromone returningPheromone;
	public Anthill returningAnthill;

	void Awake() {
		if(moveGenerator == null) {
			moveGenerator = GetComponent<MoveGenerator>();
		}
		if(moveValidator == null) {
			moveValidator = GetComponent<MoveValidator>();
		}
		move = GetComponent<Move>();
		SetReturning(false);
	}

	public void Init(Anthill anthill) {
		this.anthill = anthill;
		moveGenerator.Init(this);
		moveValidator.Init(this);
		move.OnMoveFinish += OnMoveFinish;
		SetNextDestination ();
	}

	private void OnMoveFinish(Vector3 position, DestinationType type) {
		if(isReturning) {
			if(positionHistory.Count > 0) {
				PlacePheromone(position, positionHistory[positionHistory.Count - 1]);
				positionHistory.RemoveAt(positionHistory.Count - 1);
				if (positionHistory.Count == 0) {
					AddSugarPeaceToAnthill();
					if (sugarPeace != null) {
						Debug.LogWarning("No path history and still has sugar peace");
					}
					SetReturning(false);
				}
			}
		} else {
			positionHistory.Add (position);
		}
		SetNextDestination ();
	}

	public void SetNextDestination() {
		if(isReturning) {
			if(positionHistory.Count > 1) {
				move.SetDestination(positionHistory[positionHistory.Count - 2], DestinationType.EMPTY);
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

		move.SetDestination (nextDestinaton, DestinationType.EMPTY);
	}

	private void PlacePheromone(Vector3 position, Vector3 destination) {
		Pheromone p = returningPheromone;
		returningPheromone = null;
		if (p == null) {
			p = Instantiate(prefabPheromone, position, Quaternion.identity) as Pheromone;
		}
		p.Add(destination, defaultPheromoneValue);
	}

	private void SetReturning(bool isReturning) {
		this.isReturning = isReturning;
		meshRenderer.material.color = isReturning ? Color.blue : Color.red;
	}

	private void AddSugarPeaceToAnthill() {
		if (sugarPeace != null && returningAnthill != null) {
			returningAnthill.AddSugarPeace(sugarPeace);
			sugarPeace = null;
		}
	}

	void OnTriggerEnter(Collider c) {
		if(c.gameObject.CompareTag("pheromone")) {
			returningPheromone = c.gameObject.GetComponent<Pheromone>();
//			p.Add(QVAL/ distanceSum);
		} else if(c.gameObject.CompareTag("sugar")) {
			SetReturning(true);
			sugarPeace = c.gameObject.GetComponent<Sugar>().GetPeace();
			positionHistory.Add (transform.position);
			SetNextDestination();
		} else if(c.gameObject.CompareTag("anthill")) {
			returningAnthill = c.gameObject.GetComponent<Anthill>();
//			if (!isReturning) {
//				positionHistory.Clear();
//				SetReturning(false);
//				if (sugarPeace != null) {
//					c.gameObject.GetComponent<Anthill>().AddSugarPeace(sugarPeace);
//					sugarPeace = null;
//				}
//				SetNextDestination();
//			}
		}
	}

	void OnTriggerExit(Collider c) {
		if (c.gameObject.CompareTag("anthill")) {
			returningAnthill = null;
		}
	}
}
