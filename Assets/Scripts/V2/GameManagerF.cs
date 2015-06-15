using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManagerF : MonoBehaviour {

	public enum Step
	{
		opening,
		choosePlayer,
		playerPlacement,
		inGame,
		quickTest
	};

	public Step state = Step.opening;
    //private string blue = "#68C5EE";
    //private string red = "#AB0101";

    private int bluScore = 0;
    private int redScore = 0;
    private Text blueScoreTxt;
    private Text redScoreTxt;
    private Text txtDuration;
    private Text chooseYourplayer;

    public int durationInSecond;

    public GameObject ecranFin;

    public float timeBetweenTwoBonus = 45f;
    private float rngBonus = 0.1f;
    private bool bonusCanPopUp = true;

    private bool displayEnd = false;
	private bool kickOff = false;
	private bool quickTestLaunched = false;
    
	public int whenToBeginEndTimer = 5;

	public int nextStateValidationRemaining = 4; // if you want to test with one player only, just set it to 1
	// Use this for initialization

    private TwitterFeed twitterScript;

    void Start()
    {
        blueScoreTxt = GameControllerF.GetBlueScoreTxt();
        redScoreTxt = GameControllerF.GetRedScoreTxt();
        txtDuration = GameControllerF.GetTxtDuration();
        chooseYourplayer = GameControllerF.GetChooseYourPlayer();

        RefreshDuration();
        RefreshScore();

        blueScoreTxt.enabled = false;
        redScoreTxt.enabled = false;
        txtDuration.enabled = false;
        chooseYourplayer.enabled = true;

        twitterScript = GameObject.Find("tweets").GetComponent<TwitterFeed>();

        GameControllerF.GetMonster().GetComponent<MonsterControllerF>().ballSpotlight.SetActive(false);
        GameControllerF.GetMonster().GetComponent<MonsterControllerF>().monsterSpotlight.SetActive(false);
		if (state != Step.quickTest)
			state = Step.opening;

    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("Blu "+bluScore+" - "+redScore+" Red");


		switch (state) {

		
		case Step.inGame : 
			if (durationInSecond <= 0)
			{
				//Time.timeScale = 0;
				
				if (!displayEnd)
				{
					/*ecranFin.SetActive(true);
					ecranFin.GetComponent<MenuFin>().InitMenu();
					ecranFin.GetComponent<MenuFin>().WhoWin(bluScore, redScore);
					ecranFin.GetComponent<MenuFin>().RewardPlayer();*/
					displayEnd = true;
					WhoWin();
					RewardPlayer();

					Application.LoadLevel(2);
				}
				
			}
			break;
		
		case Step.choosePlayer :
			if( nextStateValidationRemaining > 0)
				break;

            // changement des jerseys
            List<GameControllerF.Jersey> jerseysAvailable = new List<GameControllerF.Jersey>() { GameControllerF.Jersey.player1, GameControllerF.Jersey.player2, GameControllerF.Jersey.player3, GameControllerF.Jersey.player4 };
            List<PlayerControllerF> playersNotAssigned = new List<PlayerControllerF>();

            for (int i = 1; i < 5; i++)
            {
                PlayerControllerF playerScriptTested = GameControllerF.GetPlayer(i).GetComponent<PlayerControllerF>();
                playersNotAssigned.Add(playerScriptTested);
            }

            for (int i = 1; i < 5; i++)
            {

                PlayerControllerF playerScriptTested = GameControllerF.GetPlayer(i).GetComponent<PlayerControllerF>();
                int wantedPositionPlayerToPutJerseyOn = playerScriptTested.GetPositionControllerSelection();
                GameControllerF.Jersey jerseyToPutOnOtherPlayer = playerScriptTested.jersey;

                if (GameControllerF.GetPlayerPositionsAtStart().ContainsKey(wantedPositionPlayerToPutJerseyOn))
                {
                    PlayerControllerF playerToPutJerseyOn = GameControllerF.GetPlayerPositionsAtStart()[wantedPositionPlayerToPutJerseyOn];

                    playerToPutJerseyOn.SetWantedJersey(jerseyToPutOnOtherPlayer);

                    playersNotAssigned.Remove(playerToPutJerseyOn);

                    if (jerseysAvailable.Contains(jerseyToPutOnOtherPlayer))
                    {
                        jerseysAvailable.Remove(jerseyToPutOnOtherPlayer);
                    }
                    else
                    {
                        Debug.Log("Bug : probably many players with the same jersey");
                    }
                }

            }

            if (playersNotAssigned.Count == jerseysAvailable.Count)
            {
                foreach (PlayerControllerF player in playersNotAssigned)
                {
                    player.SetWantedJersey(jerseysAvailable[0]);
                    jerseysAvailable.RemoveAt(0);
                }
            }
            else
            {
                Debug.Log("Bug with jerseys Available, not the same count of jerseys available and players not assigned :");
                foreach (PlayerControllerF player in playersNotAssigned)
                {
                    Debug.Log(player.gameObject.name);
                }
                Debug.Log("player count : " + playersNotAssigned.Count + " ; Jerseys count : " + jerseysAvailable.Count);
            }

            for (int i = 1; i < 5; i++)
            {
                GameControllerF.GetPlayer(i).GetComponent<PlayerControllerF>().initPlayer();
            }

            GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;

            nextStateValidationRemaining = 4;

            StartCoroutine(FadeCanvasChooseYourPlayer());

            GameControllerF.GetPlayer(1).GetComponentInChildren<Animator>().ResetTrigger("stretch");
            GameControllerF.GetPlayer(2).GetComponentInChildren<Animator>().ResetTrigger("fence");
            GameControllerF.GetPlayer(3).GetComponentInChildren<Animator>().ResetTrigger("highKnee");
            GameControllerF.GetPlayer(4).GetComponentInChildren<Animator>().ResetTrigger("tap");

            state = Step.playerPlacement;
            break;
		
		case Step.playerPlacement :
			if( nextStateValidationRemaining > 0)
				break;
			nextStateValidationRemaining = 4;

            //GameControllerF.GetMonster().GetComponent<Rigidbody>().velocity = Vector3.zero;
            //TeleportationF telMonster = GameControllerF.GetMonster().GetComponentInChildren<TeleportationF>();
            //telMonster.InstantTP(true);
            //telMonster.SetTeleportation(false);
						
			if(!kickOff) StartCoroutine(WaitForKickOff());


			break;

		case Step.quickTest: 
			if(!quickTestLaunched){
				quickTestLaunched = true;
				StartCoroutine(matchDuration());
			}
            if (twitterScript != null)
            {
                twitterScript.SetCanDisplay(true);
                twitterScript.LaunchFirstTweet();
            }

            //GameObject.Find("ball_monster").GetComponent<MeshRenderer>().enabled = true;
            GameControllerF.GetMonster().GetComponent<MonsterControllerF>().RespawnBall();
			GameObject.Find("Main Camera").GetComponent<Animator>().SetTrigger("finalPosition");
            GameObject.Find("Main Camera").GetComponent<Animator>().enabled = false;
			GameControllerF.GetPlayer(1).GetComponent<PlayerControllerF>().Respawn();
			GameControllerF.GetPlayer(2).GetComponent<PlayerControllerF>().Respawn();
			GameControllerF.GetPlayer(3).GetComponent<PlayerControllerF>().Respawn();
			GameControllerF.GetPlayer(4).GetComponent<PlayerControllerF>().Respawn();

            GameControllerF.GetPlayer(1).GetComponent<PlayerControllerF>().DesactivateControllerSprite();
            GameControllerF.GetPlayer(2).GetComponent<PlayerControllerF>().DesactivateControllerSprite();
            GameControllerF.GetPlayer(3).GetComponent<PlayerControllerF>().DesactivateControllerSprite();
            GameControllerF.GetPlayer(4).GetComponent<PlayerControllerF>().DesactivateControllerSprite();

            blueScoreTxt.enabled = true;
            redScoreTxt.enabled = true;
            txtDuration.enabled = true;
            chooseYourplayer.enabled = false;

			state = Step.inGame;
			break;

		}
        
        
	}

    IEnumerator matchDuration()
    {
        while (durationInSecond > 0)
        {
            //if (bonusCanPopUp && Random.value <= rngBonus)
            //{
                //bonusCanPopUp = false;
                //StartCoroutine(RearmBonus());
                //Debug.Log("CreateBonus");
                //CreateBonus();
                //active un bonus
            //}
            yield return new WaitForSeconds(1);
            durationInSecond--;
            RefreshDuration();

            //feedbacks end timer
            //if (durationInSecond == 0)
            //{
            //    commentariesScript.WriteCommentary("both", "matchE");
            //}
            //else if (durationInSecond <= whenToBeginEndTimer)
            //{
            //    commentariesScript.WriteCustom("both", durationInSecond.ToString(), 1.0f, "countdownIN");
            //}
            if (durationInSecond <= whenToBeginEndTimer)
            {
                if(txtDuration.GetComponent<Animator>());
                    txtDuration.GetComponent<Animator>().SetTrigger("countdownIN");
            }

        }
    }

    IEnumerator RearmBonus()
    {
        yield return new WaitForSeconds(timeBetweenTwoBonus);
        bonusCanPopUp = true;
    }

    public void AddScore(string team)
    {
        if (team == "TeamBlu")
        {
            redScore++;
            AnimateRedScore();
        }
        else
        {
            bluScore++;
            AnimateBlueScore();
        }
        //RefreshScore();
    }

    public void RefreshScore()
    {
        //txtScore.text = ("<color="+blue+">"+bluScore+"</color> <color="+red+">"+redScore+"</color>");
        blueScoreTxt.text = bluScore.ToString();
        redScoreTxt.text = redScore.ToString();
    }

    void AnimateBlueScore()
    {
        blueScoreTxt.GetComponent<Animator>().SetTrigger("newScore");
    }

    void AnimateRedScore()
    {
        redScoreTxt.GetComponent<Animator>().SetTrigger("newScore");
    }

    void RefreshDuration()
    {
        if(durationInSecond/60<10 && (durationInSecond%60<10))
            txtDuration.text = ("0"+durationInSecond/60+":0"+durationInSecond%60);
        else if (durationInSecond%60<10)
            txtDuration.text = (durationInSecond / 60 + ":0" + durationInSecond % 60);
        else if (durationInSecond / 60 < 10)
            txtDuration.text = ("0"+durationInSecond / 60 + ":" + durationInSecond % 60);
        else
            txtDuration.text = (durationInSecond / 60 + ":" + durationInSecond % 60);
    }

    void CreateBonus()
    {
        //instantiate un bonus à un emplacement aléatoire
        Vector3 rngPop = Random.insideUnitSphere*5;
        rngPop.y = 1.0f;
        Instantiate(GameControllerF.GetCake(),rngPop,Quaternion.identity);
    }

	public void validNextState(bool valid)
    {
        if (valid)
            nextStateValidationRemaining--;
        else
            nextStateValidationRemaining++;
	}

	IEnumerator WaitForKickOff(){

		kickOff = true;

        GameControllerF.GetMonster().transform.FindChild("ball_monster").GetComponent<MeshRenderer>().enabled = true;

        GameControllerF.GetMonster().GetComponent<MonsterControllerF>().RespawnBall();

		GameObject.Find ("Commentaries").GetComponent<TextCommentaries> ().WriteCommentary ("", "matchB");

        GameObject.Find("Main Camera").GetComponent<Animator>().SetTrigger("finalPosition");
        GameObject.Find("Main Camera").GetComponent<Animator>().enabled = false;

        if (twitterScript != null)
        {
            twitterScript.SetCanDisplay(true);
            twitterScript.LaunchFirstTweet();
        }

		yield return new WaitForSeconds(3.0f);

        GameControllerF.GetSound().PlayEvent("SFX_Buzz_DebutMatch", Camera.main.gameObject);

		GameObject.Find ("Commentaries").GetComponent<TextCommentaries> ().WriteCommentary ("", "ballP");
		StartCoroutine (matchDuration ());

		state = Step.inGame;
		
	}

    public int GetBlueScore()
    {
        return bluScore;
    }

    public int GetRedScore()
    {
        return redScore;
    }

    IEnumerator FadeCanvasChooseYourPlayer()
    {
        if (chooseYourplayer.GetComponent<Animator>())
        {
            chooseYourplayer.GetComponent<Animator>().enabled = false;
        }
        if (txtDuration.GetComponent<Animator>())
        {
            txtDuration.GetComponent<Animator>().enabled = false;
        }
        StartCoroutine(FadeText(1.0f, 0.0f, 1.5f, chooseYourplayer));
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(FadeText(0.0f, 1.0f, 1.5f, txtDuration));
        StartCoroutine(FadeText(0.0f, 1.0f, 1.5f, blueScoreTxt));
        StartCoroutine(FadeText(0.0f, 1.0f, 1.5f, redScoreTxt));

    }

    IEnumerator FadeText(float beginAlpha, float endAlpha, float aTime, Text text)
    {

        text.enabled = true;
        Color textColor = text.color;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(textColor.r, textColor.g, textColor.b, Mathf.Lerp(beginAlpha, endAlpha, t));
            text.color = newColor;
            yield return null;
        }

        if (endAlpha < 0.01f)
        {
            text.enabled = false;
            text.gameObject.SetActive(false);
        }
        else if (endAlpha > 0.99f)
        {
            text.color = new Color(textColor.r, textColor.g, textColor.b, 1.0f);
        }

        if (text.GetComponent<Animator>() && !text.GetComponent<Animator>().enabled)
        {
            text.GetComponent<Animator>().enabled = true;
        }

    }
	public void RewardPlayer()
	{
		PlayerControllerF[] tabPlayer = new PlayerControllerF[4];
		
		tabPlayer[0] = GameControllerF.GetPlayer(1).GetComponent<PlayerControllerF>();
		tabPlayer[1] = GameControllerF.GetPlayer(2).GetComponent<PlayerControllerF>();
		tabPlayer[2] = GameControllerF.GetPlayer(3).GetComponent<PlayerControllerF>();
		tabPlayer[3] = GameControllerF.GetPlayer(4).GetComponent<PlayerControllerF>();
		
		int coupDonne = -1, coupRecu = -1, coupFort = -1, mangeJoueur = -1, sautBut = -1, coupVide = -1, marqueBut = -1;
		
		//Coups donnés
		int maxCoupsDonnes = 0;
		for (int i = 0; i < tabPlayer.Length; i++)
		{
			if (tabPlayer[i].coupsDonnes > maxCoupsDonnes)
			{
				maxCoupsDonnes = tabPlayer[i].coupsDonnes;
				coupDonne = i;
			}
		}


		if (coupDonne >= 0 && coupDonne < 4) {
			if(GameControllerF.GetPlayerAwards () [coupDonne] == null) GameControllerF.GetPlayerAwards () [coupDonne] = new List<string>();
			GameControllerF.GetPlayerAwards () [coupDonne].Add ("Bully,"+maxCoupsDonnes.ToString());
		}

		
		//Coups reçus
		int maxCoupsRecus = 0;
		for (int i = 0; i < tabPlayer.Length; i++)
		{
			if (tabPlayer[i].coupsRecus > maxCoupsRecus)
			{
				maxCoupsRecus = tabPlayer[i].coupsRecus;
				coupRecu = i;
			}
		}

		if (coupRecu >= 0 && coupRecu < 4) {
			if(GameControllerF.GetPlayerAwards () [coupRecu] == null) GameControllerF.GetPlayerAwards () [coupRecu] = new List<string>();
			GameControllerF.GetPlayerAwards () [coupRecu].Add ("PunchingBall,"+maxCoupsRecus.ToString());
		}


		
		//Coups forts
		
		int maxCoupsFort = 0;
		for (int i = 0; i < tabPlayer.Length; i++)
		{
			if (tabPlayer[i].coupsCharges > maxCoupsFort)
			{
				maxCoupsFort = tabPlayer[i].coupsCharges;
				coupFort = i;
			}
		}

		
		if (coupFort >= 0 && coupFort < 4) {
			if(GameControllerF.GetPlayerAwards () [coupFort] == null) GameControllerF.GetPlayerAwards () [coupFort] = new List<string>();
			GameControllerF.GetPlayerAwards () [coupFort].Add ("Berserk,"+maxCoupsFort.ToString());
		}

		int maxMangeJoueur = 0;
		for (int i = 0; i < tabPlayer.Length; i++)
		{
			if (tabPlayer[i].joueurMange > maxMangeJoueur)
			{
				maxMangeJoueur = tabPlayer[i].joueurMange;
				mangeJoueur = i;
			}
		}
		if (mangeJoueur >= 0 && mangeJoueur < 4) {
			if(GameControllerF.GetPlayerAwards () [mangeJoueur] == null) GameControllerF.GetPlayerAwards () [mangeJoueur] = new List<string>();
			GameControllerF.GetPlayerAwards () [mangeJoueur].Add ("Scrumptious,"+maxMangeJoueur.ToString());
		}

		// buts marqués
		int maxMarqueBut = 0;
		for (int i = 0; i < tabPlayer.Length; i++)
		{
			if (tabPlayer[i].marqueBut > maxMarqueBut)
			{
				maxMarqueBut = tabPlayer[i].marqueBut;
				marqueBut = i;
			}
		}

		if (marqueBut >= 0 && marqueBut < 4) {
			if(GameControllerF.GetPlayerAwards () [marqueBut] == null) GameControllerF.GetPlayerAwards () [marqueBut] = new List<string>();
			GameControllerF.GetPlayerAwards () [marqueBut].Add ("Champion,"+maxMarqueBut.ToString());
		}

		int maxSautBut = 0;
		for (int i = 0; i < tabPlayer.Length; i++)
		{
			if (tabPlayer[i].sautBut > maxSautBut)
			{
				maxSautBut = tabPlayer[i].sautBut;
				sautBut = i;
			}
		}
		if (sautBut >= 0 && sautBut < 4) {
			if(GameControllerF.GetPlayerAwards () [sautBut] == null) GameControllerF.GetPlayerAwards () [sautBut] = new List<string>();
			GameControllerF.GetPlayerAwards () [sautBut].Add ("LivingCanonBall,"+maxSautBut.ToString());
		}

		int maxCoupsVides = 0;
		for (int i = 0; i < tabPlayer.Length; i++)
		{
			if (tabPlayer[i].coupsVide > maxCoupsVides)
			{
				maxCoupsVides = tabPlayer[i].coupsVide;
				coupVide = i;
			}
		}
		if (coupVide >= 0 && coupVide < 4) {
			if(GameControllerF.GetPlayerAwards () [coupVide] == null) GameControllerF.GetPlayerAwards () [coupVide] = new List<string>();
			GameControllerF.GetPlayerAwards () [coupVide].Add ("Ghostbuster,"+maxCoupsVides.ToString());
		}

		
	}
	public void WhoWin()
	{
		if (bluScore > redScore)
		{
			GameControllerF.SetWinner("blu");
		}
		else if (bluScore < redScore)
		{
			GameControllerF.SetWinner("red");
		}
		else
		{
			GameControllerF.SetWinner("tie");
		}

		GameControllerF.SetFinalScore (bluScore, redScore);


	}


}
