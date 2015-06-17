using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MenuFinF : MonoBehaviour {

	public GameObject playAgainButton, mainMenuButton;
	public GameObject[] podiums;
	public GameObject monster;
	public float YPodiumOffset = 0.1f;
	public float buttonWaitingDuration = 2.5f;

    private SoundManager sound;

	// Use this for initialization
	void Start () {

        sound = GetComponent<SoundManager>();
        sound.LoadBank();

        playAgainButton.GetComponent<Button>().interactable = mainMenuButton.GetComponent<Button>().interactable = false;

		InitMenu ();

        sound.PlayEvent("Music_Fin", Camera.main.gameObject);

		monster.GetComponentInChildren<Animator> ().SetTrigger ("endScreen");

		StartCoroutine (WaitButtonActivation ());


	}

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetAxis("Vertical") > 0.3f || Input.GetAxis("Vertical") < -0.3f)
            {
                 EventSystem.current.SetSelectedGameObject(playAgainButton);
            }
        }
    }

	IEnumerator WaitButtonActivation(){

		yield return new WaitForSeconds (buttonWaitingDuration);

		playAgainButton.GetComponent<Button> ().interactable = mainMenuButton.GetComponent<Button> ().interactable = true;

		if (playAgainButton != null)
			EventSystem.current.SetSelectedGameObject(playAgainButton);
	}

    private void InitMenu()
    {



		GameObject.Find ("BlueScore").GetComponent<Text> ().text = GameControllerF.GetFinalScore ().Split ("," [0]) [0];
		GameObject.Find("RedScore").GetComponent<Text> ().text = GameControllerF.GetFinalScore ().Split ("," [0]) [1];


		List<string>[] awards = GameControllerF.GetPlayerAwards ();

		for (int i = 0; i<4; i++) {

			Transform 	podiumProp = podiums[i].transform.FindChild("Podium");


			if(GameControllerF.GetWinner() == "blu") {
				podiums[i].GetComponentInChildren<Animator>().SetBool(i%2 == 0 ?  (i == 0 ? "lose":"loseAlt") : (i == 1 ? "win" : "winAlt"), true); 
				podiumProp.localScale = new Vector3( podiumProp.localScale.x, podiumProp.localScale.y, i%2 == 0? 2f : 3f);
				podiumProp.localPosition = new Vector3( podiumProp.localPosition.x, -(podiumProp.localScale.z+ YPodiumOffset), podiumProp.localPosition.z);
			}
			else if(GameControllerF.GetWinner() == "red"){
				podiums[i].GetComponentInChildren<Animator>().SetBool(i%2 == 0 ?  (i == 0 ? "win":"winAlt") : (i == 1 ? "lose" : "loseAlt"), true);
				podiumProp.localScale = new Vector3( podiumProp.localScale.x, podiumProp.localScale.y, i%2 == 0? 3f : 2f);
				podiumProp.localPosition = new Vector3( podiumProp.localPosition.x, -(podiumProp.localScale.z+ YPodiumOffset), podiumProp.localPosition.z);

			}
			else {
				podiums[i].GetComponentInChildren<Animator>().SetBool(( i == 0|| i == 1 )?"lose" : "loseAlt", true); 
				podiumProp.localScale = new Vector3( podiumProp.localScale.x, podiumProp.localScale.y,  2.5f);
				podiumProp.localPosition = new Vector3( podiumProp.localPosition.x, -(podiumProp.localScale.z+ YPodiumOffset), podiumProp.localPosition.z);

			}

			podiums[i].transform.localPosition = new Vector3( podiums[i].transform.localPosition.x,(podiumProp.localScale.z ) == 3 ? 10.2f : (podiumProp.localScale.z == 2.5) ? 8.5f : 6.8f ,podiums[i].transform.localPosition.z);

			if(awards[i] != null && awards[i].Count > 0){

				if(awards[i][0] != string.Empty){
					Transform highReward = podiums[i].transform.FindChild("HighReward");
					highReward.gameObject.SetActive(true);
					highReward.FindChild("Award").GetComponent<SpriteRenderer>().sprite = Resources.Load("Awards/"+awards[i][0].Split(","[0])[0], typeof(Sprite)) as Sprite;
					highReward.GetComponentInChildren<TextMesh>().text = awards[i][0].Split(","[0])[1];
				
					
				}
				if(awards[i].Count > 1 && awards[i][1] != string.Empty ){
					Transform lowReward = podiums[i].transform.FindChild("LowReward");
					lowReward.gameObject.SetActive(true);
					lowReward.FindChild("Award").GetComponent<SpriteRenderer>().sprite = Resources.Load("Awards/"+awards[i][1].Split(","[0])[0],typeof(Sprite)) as Sprite;
					lowReward.GetComponentInChildren<TextMesh>().text = awards[i][1].Split(","[0])[1];
				

				}



			}
			else continue;


			


		}






        
    }

    public void Replay()
    {

        Application.LoadLevel(1);
    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }

    

    
}
