using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarsDisplaying : MonoBehaviour {


    private ParticleSystem prefabStar;
    private List<ParticleSystem> starList = new List<ParticleSystem>();

    void Awake()
    {
        prefabStar = Resources.Load<ParticleSystem>("Etoile/Etoile");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (ParticleSystem particle in starList)
        {

            if (!particle.IsAlive() && particle.gameObject.activeSelf)
            {
                particle.gameObject.SetActive(false);
            }

        }
	}

    public void DisplayStarEffect(Vector3 position)
    {

        bool hasRecycled = false;

        foreach (ParticleSystem particle in starList)
        {
            if (!particle.gameObject.activeSelf)
            {
                particle.gameObject.SetActive(true);
                particle.Play();

                particle.transform.position = position;

                //random rotation
                particle.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f), Space.Self);

                hasRecycled = true;
                break;
            }
        }

        if (!hasRecycled)
        {
            ParticleSystem particle = Instantiate(prefabStar);
            particle.transform.SetParent(transform, false);
            particle.gameObject.SetActive(true);
            particle.Play();

            particle.transform.position = position;

            //random rotation
            particle.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f), Space.Self);

            starList.Add(particle);
        }
    }

}
