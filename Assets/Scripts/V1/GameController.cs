using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    private int BluScore = 0, RedScore = 0, wrathMonster = 0;
    private CameraController cameraController;
    private Text[] uiTexts;

	// Use this for initialization
	void Start () {
        uiTexts = GetComponentsInChildren<Text>();
        refreshText();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void AddScore(string team, int score)
    {
        if (team == "TeamBlu")
            RedScore += score;
        else
            BluScore += score;

        refreshText();
    }

    public void SetWrathMonster(int wrath)
    {
        wrathMonster = wrath;

        refreshText();
    }

    public void ResetCamera()
    {
        //Lorsque le deplacement de la camera est annule, elle se replace à son origine
        cameraController.SetMoveMode(false);
    }

    public void refreshText()
    {
        for (int i = 0; i < uiTexts.Length; i++)
        {
            if (uiTexts[i].name == "TxtScores")
                uiTexts[i].text = "Blu " + BluScore + " - " + RedScore + " Red";
            if (uiTexts[i].name == "TxtMonster")
                uiTexts[i].text = "Wrath's Monster : " + wrathMonster;
        }
    }
}
