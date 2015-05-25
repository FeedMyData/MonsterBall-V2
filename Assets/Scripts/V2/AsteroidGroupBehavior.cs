using UnityEngine;
using System.Collections;

public class AsteroidGroupBehavior : MonoBehaviour {

	public Vector3 speed = new Vector3(0.05f, 0f,0f);
	[Range(-300,0f)]
	public float leftLimit;
	[Range(0f,300f)]
	public float rightLimit;
	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		foreach (Transform childTransform in transform) {

			childTransform.Translate( speed, Space.World);
			if(childTransform.localPosition.x > rightLimit)
				Replace(childTransform);
		}


	}

	void Replace(Transform target){

		target.localPosition = new Vector3 (leftLimit, target.localPosition.y, target.localPosition.z);
		target.Rotate(new Vector3(Random.Range(-180f,180f),Random.Range(-180f,180f)));

	}
}
