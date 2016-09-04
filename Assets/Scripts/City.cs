using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {

	public Transform myTransform;
	public Vector3 Position { get { return myTransform.position; } }

}
