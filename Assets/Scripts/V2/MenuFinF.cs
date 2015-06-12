using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MenuFinF : MonoBehaviour {

    public GameObject defaultButton;
	public GameObject[] podiums;
	public GameObject monster;
	public float YPodiumOffset = 0.1f;

  

	// Use this for initialization
	void Start () {
        if (defaultButton != null)
            EventSystem.current.SetSelectedGameObject(defaultButton);
		InitMenu ();
		monster.GetComponentInChildren<Animator> ().SetTrigger ("endScreen");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void InitMenu()
    {

		List<string>[] awards = GameControllerF.GetPlayerAwards ();

		for (int i = 0; i<4; i++) {

			Transform 	podiumProp = podiums[i].transform.FindChild("Podium");


			if(GameControllerF.GetWinner() == "blu") {
				podiums[i].GetComponentInChildren<Animator>().SetTrigger(i%2 == 0 ? "lose" : "win"); 
				podiumProp.localScale = new Vector3( podiumProp.localScale.x, podiumProp.localScale.y, i%2 == 0? 2f : 3f);
				podiumProp.localPosition = new Vector3( podiumProp.localPosition.x, -(podiumProp.localScale.z+ YPodiumOffset), podiumProp.localPosition.z);
			}
			else if(GameControllerF.GetWinner() == "red"){
					podiums[i].GetComponentInChildren<Animator>().SetTrigger(i%2 == 0 ? "win" : "lose");
				podiumProp.localScale = new Vector3( podiumProp.localScale.x, podiumProp.localScale.y, i%2 == 0? 3f : 2f);
				podiumProp.localPosition = new Vector3( podiumProp.localPosition.x, -(podiumProp.localScale.z+ YPodiumOffset), podiumProp.localPosition.z);

			}
			else {
				podiums[i].GetComponentInChildren<Animator>().SetTrigger("lose"); 
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
					/*Debug.Log("HIGH");
					Debug.Log(awards[i][0].Split(","[0])[0]);
					Debug.Log(awards[i][0].Split(","[0])[1]);*/
					
				}
				if(awards[i].Count > 1 && awards[i][1] != string.Empty ){
					Transform lowReward = podiums[i].transform.FindChild("LowReward");
					lowReward.gameObject.SetActive(true);
					lowReward.FindChild("Award").GetComponent<SpriteRenderer>().sprite = Resources.Load("Awards/"+awards[i][1].Split(","[0])[0],typeof(Sprite)) as Sprite;
					lowReward.GetComponentInChildren<TextMesh>().text = awards[i][1].Split(","[0])[1];
					/*Debug.Log("Low");
					Debug.Log(awards[i][1].Split(","[0])[0]);
					Debug.Log(awards[i][1].Split(","[0])[1]);*/

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
