using UnityEngine;
using System.Collections;

public class ballSpotlight : MonoBehaviour {

    public Transform ball;

	// Update is called once per frame
	void Update () {

        position();

	}

    public void position()
    {
        transform.position = new Vector3(ball.position.x, transform.position.y, ball.position.z);
    }
}
