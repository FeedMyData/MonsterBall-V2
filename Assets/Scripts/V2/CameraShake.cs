using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float timeShaking = 5.0f;
    private float timeS = 0.0f;
    public float shakeAmount = 0.5f;
    public float decreaseFactor = 1.0f;

    public bool decreaseShakeWithTime = true;

    private Vector3 originalPos;

	// Use this for initialization
	void Start () {
	
        originalPos = transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {

            shake(0,0,0);

        }

        if (timeS > 0)
        {
            if (decreaseShakeWithTime)
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount * timeS;
            }
            else
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            }

            timeS -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            timeS = 0f;
            transform.localPosition = originalPos;
        }

	}

    public void shake(float durationFactor, float powerFactor, float speedFactor) {

        originalPos = transform.localPosition;

        timeS = timeShaking * durationFactor;
        shakeAmount *= powerFactor;
        decreaseFactor *= speedFactor;

        //timeS = (duration != 0) ? duration : timeShaking;
        //shakeAmount = (power != 0) ? power : shakeAmount;
        //decreaseFactor = (speed != 0) ? speed : decreaseFactor;

    }

}
