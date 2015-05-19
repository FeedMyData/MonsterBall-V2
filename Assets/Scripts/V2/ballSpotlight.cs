using UnityEngine;
using System.Collections;

public class ballSpotlight : MonoBehaviour {

    public Transform ball;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(ball.position.x, transform.position.y, ball.position.z);

	}
}
