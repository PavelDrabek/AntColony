using UnityEngine;
using System.Collections;

public class Sugar : MonoBehaviour {

//	public float Amount { get; private set; } 
	public float amount;

	public GameObject GetPeace() {
		amount -= 1;
		return new GameObject("sugar peace");
	}
}
