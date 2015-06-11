using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionTerrain : MonoBehaviour {

    //public Transform lightFxCollisionPrefab;
    //private float lightDistanceFactor = 8.0f;

    public ParticleSystem collisionEffectPrefab;
    [Header("Reglages power balle")]
    public float minPower = 50.0f;
    public float maxPower = 80.0f;
    [Header("Speed particles")]
    public float minSpeed = 0.0f;
    public float maxSpeed = 10.0f;
    [Header("Emission Rate particles")]
    public float minRate = 10.0f;
    public float maxRate = 30.0f;
    [Header("Size particles")]
    public float minSize = 0.1f;
    public float maxSize = 1.0f;
    //private float speedFadingFactor = 4.0f;
    //private List<Transform> lightList = new List<Transform>();
    private List<ParticleSystem> effectList = new List<ParticleSystem>();

    void Update()
    {

        //foreach (Transform light in lightList)
        //{

        //    if (light.gameObject.activeSelf)
        //    {
        //        light.GetComponent<Light>().intensity -= Time.deltaTime * speedFadingFactor;
        //        if (light.GetComponent<Light>().intensity <= 0.1f)
        //        {
        //            light.GetComponent<Light>().intensity = 0;
        //            light.gameObject.SetActive(false);
        //        }
        //    }

        //}

        foreach (ParticleSystem particle in effectList)
        {

            if (!particle.IsAlive() && particle.gameObject.activeSelf)
            {
                particle.gameObject.SetActive(false);
            }

        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {

            // display sprite on dome

            //spotlightEffect(other);

            SoundManager sound = GameControllerF.GetSound();
            sound.PlayEvent("VX_Balle_Cloison", other.gameObject);

            collisionEffect(other);

        }
    }

    void collisionEffect(Collision other)
    {
        Vector3 averageContactPoint = Vector3.zero;
        Vector3 averageContactNormal = Vector3.zero;

        //Debug.Log(other.relativeVelocity.magnitude);

        foreach (ContactPoint contact in other.contacts)
        {
            averageContactPoint += contact.point;
            averageContactNormal += contact.normal;
            //Debug.DrawLine(contact.point, contact.point + contact.normal * 4, Color.green, 2.0f, false);
        }

        averageContactPoint = averageContactPoint / other.contacts.Length;
        averageContactNormal = averageContactNormal / other.contacts.Length;

        //Debug.DrawLine(averageContactPoint, averageContactPoint + averageContactNormal * 4, Color.red, 5.0f, false);

        bool hasRecycled = false;

        float speed = Mathf.Lerp(minSpeed, maxSpeed, (other.relativeVelocity.magnitude - minPower) / (maxPower - minPower));
        float emissionRate = Mathf.Lerp(minRate, maxRate, (other.relativeVelocity.magnitude - minPower) / (maxPower - minPower));
        float size = Mathf.Lerp(minSize, maxSize, (other.relativeVelocity.magnitude - minPower) / (maxPower - minPower));

        foreach (ParticleSystem particle in effectList)
        {
            if (!particle.gameObject.activeSelf)
            {
                particle.gameObject.SetActive(true);
                particle.startSpeed = speed;
                particle.emissionRate = emissionRate;
                particle.startSize = size;
                particle.Play();

                particle.transform.position = averageContactPoint;
                particle.transform.forward = averageContactNormal;

                //random rotation
                particle.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f), Space.Self);

                hasRecycled = true;
                break;
            }
        }

        if (!hasRecycled)
        {
            ParticleSystem particle = Instantiate(collisionEffectPrefab);
            particle.gameObject.SetActive(true);
            particle.startSpeed = speed;
            particle.emissionRate = emissionRate;
            particle.startSize = size;
            particle.Play();

            particle.transform.position = averageContactPoint;
            particle.transform.forward = averageContactNormal;

            //random rotation
            particle.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f), Space.Self);

            effectList.Add(particle);
        }
    }

    //void spotlightEffect(Collision other)
    //{
    //    Vector3 averageContactPoint = Vector3.zero;
    //    Vector3 averageContactNormal = Vector3.zero;

    //    foreach (ContactPoint contact in other.contacts)
    //    {
    //        averageContactPoint += contact.point;
    //        averageContactNormal += contact.normal;
    //        //Debug.DrawLine(contact.point, contact.point + contact.normal * 4, Color.green, 2.0f, false);
    //    }

    //    averageContactPoint = averageContactPoint / other.contacts.Length;
    //    averageContactNormal = averageContactNormal / other.contacts.Length;
    //    averageContactNormal.Normalize();

    //    //Debug.DrawLine(averageContactPoint, averageContactPoint + averageContactNormal * 4, Color.red, 5.0f, false);

    //    bool hasRecycled = false;

    //    foreach (Transform light in lightList)
    //    {
    //        if (!light.gameObject.activeSelf)
    //        {
    //            light.gameObject.SetActive(true);
    //            light.GetComponent<Light>().intensity = 8.0f;

    //            if (averageContactPoint.z < 0)
    //            {
    //                light.position = averageContactPoint + averageContactNormal * lightDistanceFactor;
    //                light.forward = -averageContactNormal;
    //            }
    //            else
    //            {
    //                light.position = averageContactPoint - averageContactNormal * lightDistanceFactor;
    //                light.forward = averageContactNormal;
    //            }

    //            //random rotation
    //            light.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f), Space.Self);

    //            hasRecycled = true;
    //            break;
    //        }
    //    }

    //    if (!hasRecycled)
    //    {
    //        Transform newLight = Instantiate(lightFxCollisionPrefab);
    //        newLight.gameObject.SetActive(true);
    //        newLight.GetComponent<Light>().intensity = 8.0f;

    //        if (averageContactPoint.z < 0)
    //        {
    //            newLight.position = averageContactPoint + averageContactNormal * lightDistanceFactor;
    //            newLight.forward = -averageContactNormal;
    //        }
    //        else
    //        {
    //            newLight.position = averageContactPoint - averageContactNormal * lightDistanceFactor;
    //            newLight.forward = averageContactNormal;
    //        }

    //        //random rotation
    //        newLight.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f), Space.Self);

    //        lightList.Add(newLight);
    //    }

        
    //}
}
