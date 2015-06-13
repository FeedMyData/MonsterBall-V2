using UnityEngine;
using System.Collections;

public class TeleportationF : MonoBehaviour {

    public float durationTP = 3.0f;
    private float actualValueShaderTP;
    private Material[] ballMaterial;
    private bool teleportationUp;
    private bool teleportationDown;
    private float timeTP;

	// Use this for initialization
	void Start () {
        ballMaterial = GetComponent<Renderer>().materials;
        actualValueShaderTP = ballMaterial[0].GetFloat("_alpha_slider");
	}
	
	// Update is called once per frame
	void Update () {

        if (teleportationUp)
            TeleportationUp();
        
        if(teleportationDown)
            TeleportationDown();
	}

    void TeleportationUp()
    {
        float delayTP = Mathf.Abs(((timeTP - Time.time) / durationTP) - 1);

        for (int i = 0; i < ballMaterial.Length; i++)
        {
            ballMaterial[i].SetFloat("_alpha_slider", Mathf.Lerp(actualValueShaderTP,0,delayTP));
        }

        if (delayTP >= 1)
        {
            teleportationUp = false;
            InstantTP(true);
            actualValueShaderTP = ballMaterial[0].GetFloat("_alpha_slider");
        }
            
    }

    public void InstantTP(bool up)
    {
        actualValueShaderTP = ballMaterial[0].GetFloat("_alpha_slider");
        Debug.Log("tpinstant " + ballMaterial[0].GetFloat("_alpha_slider"));

        for (int i = 0; i < ballMaterial.Length; i++)
        {
            if (up)
            {
                ballMaterial[i].SetFloat("_alpha_slider", 0);
            }
            else
            {
                ballMaterial[i].SetFloat("_alpha_slider", 80);
            }
        }
    }

    void TeleportationDown()
    {
        float delayTP = Mathf.Abs(((timeTP - Time.time) / durationTP) - 1);

        for (int i = 0; i < ballMaterial.Length; i++)
        {
            ballMaterial[i].SetFloat("_alpha_slider", Mathf.Lerp(actualValueShaderTP, 80, delayTP));
        }

        if (delayTP >= 1)
        {
            teleportationDown = false;
            InstantTP(false);
            actualValueShaderTP = ballMaterial[0].GetFloat("_alpha_slider");
        }
    }

    public void SetTeleportation(bool up)
    {
        actualValueShaderTP = ballMaterial[0].GetFloat("_alpha_slider");
        if (up)
        {
            teleportationUp = true;
            teleportationDown = false;
        }
        else
        {
            teleportationUp = false;
            teleportationDown = true;
        }

        timeTP = Time.time + durationTP;
    }
}
