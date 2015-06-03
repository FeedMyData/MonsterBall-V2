using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionTerrain : MonoBehaviour {

    public Transform lightFxCollisionPrefab;
    private float lightDistanceFactor = 8.0f;
    //private float fadeLightDuration = 1.5f;
    //private float fadeLightTimer = 0.0f;
    //private bool isFading = false;

    private float speedFadingFactor = 4.0f;
    private List<Transform> lightList = new List<Transform>();

    void Start()
    {
        //lightFxCollision = GameObject.Find("light fx dome").transform;
    }

    void Update()
    {
        //if (isFading)
        //{
        //    fadeLightTimer += Time.deltaTime;
        //    if (fadeLightTimer > fadeLightDuration)
        //    {
        //        isFading = false;
        //        fadeLightTimer = fadeLightDuration;
        //    }
        //    float percentageL = fadeLightTimer / fadeLightDuration;
        //    percentageL = 1.0f - Mathf.Cos(percentageL * Mathf.PI * 0.5f);
        //    lightFxCollision.GetComponent<Light>().intensity = Mathf.Lerp(8.0f, 0.0f, percentageL);
        //}

        foreach(Transform light in lightList) {

            if (light.gameObject.activeSelf)
            {
                light.GetComponent<Light>().intensity -= Time.deltaTime * speedFadingFactor;
                if (light.GetComponent<Light>().intensity <= 0.1f)
                {
                    light.GetComponent<Light>().intensity = 0;
                    light.gameObject.SetActive(false);
                }
            }

        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("testCollision");


            // display sprite on dome

            spotlightEffect(other);

        }
    }

    void spotlightEffect(Collision other)
    {
        Vector3 averageContactPoint = Vector3.zero;
        Vector3 averageContactNormal = Vector3.zero;

        foreach (ContactPoint contact in other.contacts)
        {
            averageContactPoint += contact.point;
            averageContactNormal += contact.normal;
            //Debug.DrawLine(contact.point, contact.point + contact.normal * 4, Color.green, 2.0f, false);
        }

        averageContactPoint = averageContactPoint / other.contacts.Length;
        averageContactNormal = averageContactNormal / other.contacts.Length;
        averageContactNormal.Normalize();

        //Debug.DrawLine(averageContactPoint, averageContactPoint + averageContactNormal * 4, Color.red, 5.0f, false);

        //fadeLightTimer = 0.0f;
        //isFading = true;

        bool hasRecycled = false;

        foreach (Transform light in lightList)
        {
            if (!light.gameObject.activeSelf)
            {
                light.gameObject.SetActive(true);
                light.GetComponent<Light>().intensity = 8.0f;

                if (averageContactPoint.z < 0)
                {
                    light.position = averageContactPoint + averageContactNormal * lightDistanceFactor;
                    light.forward = -averageContactNormal;
                }
                else
                {
                    light.position = averageContactPoint - averageContactNormal * lightDistanceFactor;
                    light.forward = averageContactNormal;
                }

                //random rotation
                light.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f), Space.Self);

                hasRecycled = true;
                break;
            }
        }

        if (!hasRecycled)
        {
            Transform newLight = Instantiate(lightFxCollisionPrefab);
            newLight.gameObject.SetActive(true);
            newLight.GetComponent<Light>().intensity = 8.0f;

            if (averageContactPoint.z < 0)
            {
                newLight.position = averageContactPoint + averageContactNormal * lightDistanceFactor;
                newLight.forward = -averageContactNormal;
            }
            else
            {
                newLight.position = averageContactPoint - averageContactNormal * lightDistanceFactor;
                newLight.forward = averageContactNormal;
            }

            //random rotation
            newLight.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f), Space.Self);

            lightList.Add(newLight);
        }

        
    }
}
