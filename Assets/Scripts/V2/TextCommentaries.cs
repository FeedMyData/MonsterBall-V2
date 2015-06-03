﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextCommentaries : MonoBehaviour {

    public Text blueText;
    public Text redText;


    public float timeBeforeOutGoal = 1.5f;

    public string[] commentsForGoalsByPlayers;
    public string[] commentsForGoalsByMonster;

    public float timerBeforeOutInformations = 3.0f;

    public string instructionWhenBall = "Kick goals !";
    public string instructionWhenMonster = "RUN !!!";

    public string beginningMessage = "Ready ?";
    public string endingMessage = "END";

    private float timerBlueCom = 0.0f;
    private float timerRedCom = 0.0f;

    Text textToWriteOn;

	// Use this for initialization
	void Start () {

        blueText.text = "";
        redText.text = "";

	}
	
	// Update is called once per frame
	void Update () {

        if (timerBlueCom > 0)
        {
            timerBlueCom -= Time.deltaTime;
        }
        else
        {
            blueText.GetComponent<Animator>().SetBool("doOUT", true);
        }

        if (timerRedCom > 0)
        {
            timerRedCom -= Time.deltaTime;
        }
        else
        {
            redText.GetComponent<Animator>().SetBool("doOUT", true);
        }

	}

    public void WriteCommentary(string side, string type)
    {

        if (side == "TeamBlu")
        {
            textToWriteOn = redText;
            timerRedCom = timeBeforeOutGoal;
            redText.GetComponent<Animator>().SetBool("doOUT", false);
        }
        else if (side == "TeamRed")
        {
            textToWriteOn = blueText;
            timerBlueCom = timeBeforeOutGoal;
            blueText.GetComponent<Animator>().SetBool("doOUT", false);
        }
        else
        {
            textToWriteOn = redText; // just to pass the condition below

            timerBlueCom = timerBeforeOutInformations;
            timerRedCom = timerBeforeOutInformations;

            blueText.GetComponent<Animator>().SetBool("doOUT", false);
            redText.GetComponent<Animator>().SetBool("doOUT", false);
        }

        if (textToWriteOn != null)
        {
            switch (type)
            {
                case "playerG":
                    GoalByTeam();
                    break;

                case "monsterG":
                    GoalByMonster();
                    break;

                case "ballP":
                    BallPhase();
                    break;

                case "monsterP":
                    MonsterPhase();
                    break;

                case "matchB":
                    MatchBegins();
                    break;

                case "matchE":
                    MatchEnds();
                    break;
            }
        }

    }

    void GoalByTeam()
    {
        textToWriteOn.text = commentsForGoalsByPlayers[Random.Range(0, commentsForGoalsByPlayers.Length)];
        textToWriteOn.GetComponent<Animator>().SetTrigger("playerIN");
    }

    void GoalByMonster()
    {
        textToWriteOn.text = commentsForGoalsByMonster[Random.Range(0, commentsForGoalsByMonster.Length)];
        textToWriteOn.GetComponent<Animator>().SetTrigger("playerIN");
    }

    void BallPhase()
    {
        blueText.text = instructionWhenBall;
        blueText.GetComponent<Animator>().SetTrigger("playerIN");
        redText.text = instructionWhenBall;
        redText.GetComponent<Animator>().SetTrigger("playerIN");
    }

    void MonsterPhase()
    {
        blueText.text = instructionWhenMonster;
        blueText.GetComponent<Animator>().SetTrigger("playerIN");
        redText.text = instructionWhenMonster;
        redText.GetComponent<Animator>().SetTrigger("playerIN");
    }

    void MatchBegins()
    {
        blueText.text = beginningMessage;
        blueText.GetComponent<Animator>().SetTrigger("playerIN");
        redText.text = beginningMessage;
        redText.GetComponent<Animator>().SetTrigger("playerIN");
    }

    void MatchEnds()
    {
        blueText.text = endingMessage;
        //blueText.GetComponent<Animator>().SetTrigger("playerIN");
        redText.text = endingMessage;
        //redText.GetComponent<Animator>().SetTrigger("playerIN");
    }

    public void WriteCustom(string side, string text, float duration, string easeIN)
    {

        if (side == "TeamBlu")
        {
            timerBlueCom = duration;
            blueText.GetComponent<Animator>().SetBool("doOUT", false);

            blueText.text = text;
            blueText.GetComponent<Animator>().SetTrigger(easeIN);
        }
        else if (side == "TeamRed")
        {
            timerRedCom = duration;
            redText.GetComponent<Animator>().SetBool("doOUT", false);

            redText.text = text;
            redText.GetComponent<Animator>().SetTrigger(easeIN);
        }
        else
        {
            timerBlueCom = duration;
            timerRedCom = duration;

            blueText.GetComponent<Animator>().SetBool("doOUT", false);
            redText.GetComponent<Animator>().SetBool("doOUT", false);

            blueText.text = text;
            blueText.GetComponent<Animator>().SetTrigger(easeIN);

            redText.text = text;
            redText.GetComponent<Animator>().SetTrigger(easeIN);
        }

    }

}
