using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float timeShaking = 5.0f;
    private float timeS = 0.0f;
    public float shakeAmount = 0.5f;
    private float shakeA = 0.5f;
    public float decreaseFactor = 1.0f;
    private float decreaseF = 1.0f;

    public bool decreaseShakeWithTime = true;

    private Vector3 originalPos;
    private bool repositionned = false;

	// Use this for initialization
	void Start () {
	
        originalPos = transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<CamEffects>() && GetComponent<CamEffects>().GetRepositionned())
        {

            if (timeS > 0)
            {
                if (decreaseShakeWithTime)
                {
                    transform.localPosition = originalPos + Random.insideUnitSphere * shakeA * timeS;
                }
                else
                {
                    transform.localPosition = originalPos + Random.insideUnitSphere * shakeA;
                }

                timeS -= Time.deltaTime * decreaseF;
                repositionned = false;
            }
            else if (!repositionned)
            {
                timeS = 0f;
                transform.localPosition = originalPos;
                repositionned = true;
            }

        }

	}

    public void shake(float durationFactor, float powerFactor, float speedFactor) {

        originalPos = transform.localPosition;

        timeS = timeShaking * durationFactor;
        shakeA = shakeAmount * powerFactor;
        decreaseF = decreaseFactor * speedFactor;

        //timeS = (duration != 0) ? duration : timeShaking;
        //shakeAmount = (power != 0) ? power : shakeAmount;
        //decreaseFactor = (speed != 0) ? speed : decreaseFactor;

    }

}
