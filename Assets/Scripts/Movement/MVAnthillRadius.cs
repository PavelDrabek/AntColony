﻿using UnityEngine;
using System.Collections;

public class MVAnthillRadius : MoveValidator {

	public Anthill anthill;
	public float maxHomeDistance;

	public override void Init() {
		anthill = GetComponent<Ant>().anthill;
	}

	public override bool CanMove (Vector3 destination)
	{
		if(anthill == null) {
			return false;
		}
		return Vector3.Distance(destination, anthill.transform.position) < maxHomeDistance;
	}

	void OnDrawGizmosSelected() {
		if(anthill != null) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(anthill.transform.position, maxHomeDistance);
		}
	}
}
