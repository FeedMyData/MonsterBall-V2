using UnityEngine;
using System.Collections;

public class CamEffects : MonoBehaviour {

    public GameObject monster; //monster1
    public Transform boneToFollowWhenChewing; //bone16
    public float camMoveFactor = 0.5f;

    private Vector3 originalCamPos;

    private bool repositionned = false;
    private bool animatorDesactivated = false;
    //private GameManagerF.Step currentStep;
    //private bool playingUntilChoosePlayer = false;

	void Awake () {

        originalCamPos = transform.localPosition;

	}

    void Start()
    {
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().enabled = true;
        }
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
            if (!animatorDesactivated && GameControllerF.getManager().state == GameManagerF.Step.inGame)
            {
                if (GetComponent<Animator>())
                {
                    GetComponent<Animator>().enabled = false;
                    animatorDesactivated = true;
                }
            }

            transform.localPosition = originalCamPos;
            repositionned = true;
        }

	}

	public void GoToChoosePlayerState(){

		gameObject.GetComponent<Animator> ().enabled = false;

		GameControllerF.GetPlayer(1).GetComponentInChildren<Animator>().SetTrigger("stretch");
		GameControllerF.GetPlayer(2).GetComponentInChildren<Animator>().SetTrigger("fence");
		GameControllerF.GetPlayer(3).GetComponentInChildren<Animator>().SetTrigger("highKnee");
		GameControllerF.GetPlayer(4).GetComponentInChildren<Animator>().SetTrigger("tap");


		StartCoroutine (WaitForBeginningAnimationEnd ());
	}

	IEnumerator WaitForBeginningAnimationEnd(){
		yield return new WaitForSeconds(0.2f);
        if (GameControllerF.getManager().state == GameManagerF.Step.opening)
        {
            //GameControllerF.GetPlayer(1).transform.FindChild("AButtonSprite").GetComponent<SpriteRenderer>().enabled = true;
            //GameControllerF.GetPlayer(2).transform.FindChild("AButtonSprite").GetComponent<SpriteRenderer>().enabled = true;
            //GameControllerF.GetPlayer(3).transform.FindChild("AButtonSprite").GetComponent<SpriteRenderer>().enabled = true;
            //GameControllerF.GetPlayer(4).transform.FindChild("AButtonSprite").GetComponent<SpriteRenderer>().enabled = true;
            GameControllerF.getManager().state = GameManagerF.Step.choosePlayer;
        } else { // safe condition to play anyway
            GameControllerF.getManager().state = GameManagerF.Step.quickTest;
        }

	}
    public bool GetRepositionned()
    {
        return repositionned;
    }
}
