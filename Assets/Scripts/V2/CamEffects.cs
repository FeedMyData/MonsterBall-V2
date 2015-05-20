using UnityEngine;
using System.Collections;

public class CamEffects : MonoBehaviour {

    public GameObject monster;
    public Transform boneToFollowWhenChewing;
    public float camMoveFactor = 0.5f;

    private Vector3 originalCamPos;

    private bool repositionned = false;

	// Use this for initialization
	void Start () {

        originalCamPos = transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {

        if (monster.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SpitPlayer"))
        {

            transform.localPosition = originalCamPos + boneToFollowWhenChewing.forward * camMoveFactor;
            repositionned = false;

        }
        else if (!repositionned)
        {
            transform.localPosition = originalCamPos;
            repositionned = true;
        }

	}

    public bool GetRepositionned()
    {
        return repositionned;
    }
}
