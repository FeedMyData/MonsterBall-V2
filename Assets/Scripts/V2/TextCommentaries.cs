using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextCommentaries : MonoBehaviour {

    public Text redText;
    public Text blueText;

    private string blue = "#68C5EE";
    private string red = "#AB0101";

    public string[] commentsForGoalsByPlayers;
    public string[] commentsForGoalsByMonster;

    public string instructionWhenBall = "Kick goals !";
    public string instructionWhenMonster = "RUN !!!";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
