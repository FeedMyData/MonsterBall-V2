using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MenuFinF : MonoBehaviour {

    public GameObject defaultButton;
	public GameObject[] podiums;


  

	// Use this for initialization
	void Start () {
        if (defaultButton != null)
            EventSystem.current.SetSelectedGameObject(defaultButton);
		InitMenu ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void InitMenu()
    {

		List<string>[] awards = GameControllerF.GetPlayerAwards ();

		for (int i = 0; i<4; i++) {

			if(GameControllerF.GetWinner() == "blu") podiums[i].GetComponentInChildren<Animator>().SetTrigger(i%2 == 0 ? "lose" : "win"); // temporaire
			else if(GameControllerF.GetWinner() == "red") podiums[i].GetComponentInChildren<Animator>().SetTrigger(i%2 == 0 ? "win" : "lose");// idem
		
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
