using UnityEngine;
using System.Collections;

public class moveCamWithKeys : MonoBehaviour {

    public float translationFactor = 0.1f;
    public float rotationFactor = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // TRANSLATION

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * translationFactor, Space.World);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * translationFactor, Space.World);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * translationFactor, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * translationFactor, Space.World);
        }

        if (Input.GetKey(KeyCode.P))
        {
            transform.Translate(Vector3.up * translationFactor, Space.World);
        }

        if (Input.GetKey(KeyCode.M))
        {
            transform.Translate(Vector3.down * translationFactor, Space.World);
        }

        // ROTATION

        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(Vector3.left * rotationFactor, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.right * rotationFactor, Space.World);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * rotationFactor, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotationFactor, Space.World);
        }

	}
}
