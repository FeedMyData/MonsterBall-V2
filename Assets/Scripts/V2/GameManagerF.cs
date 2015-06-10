using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

        RefreshDuration();
        RefreshScore();

        twitterScript = GameObject.Find("tweets").GetComponent<TwitterFeed>();

        GameControllerF.GetMonster().GetComponent<MonsterControllerF>().ballSpotlight.SetActive(false);
        GameControllerF.GetMonster().GetComponent<MonsterControllerF>().monsterSpotlight.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("Blu "+bluScore+" - "+redScore+" Red");


		switch (state) {

		
		case Step.inGame : 
			if (durationInSecond <= 0)
			{
				Time.timeScale = 0;
				
				if (!displayEnd)
				{
					ecranFin.SetActive(true);
					ecranFin.GetComponent<MenuFin>().InitMenu();
					ecranFin.GetComponent<MenuFin>().WhoWin(bluScore, redScore);
					ecranFin.GetComponent<MenuFin>().RewardPlayer();
					displayEnd = true;
				}
				
			}
			break;
		
		case Step.choosePlayer :
			if( nextStateValidationRemaining != 0)
				break;
            
            // changement des jerseys
            for (int i = 1; i < 5; i++)
            {

                PlayerControllerF playerScript = GameControllerF.GetPlayer(i).GetComponent<PlayerControllerF>();
                int positionPlayerTested = playerScript.GetPositionControllerSelection();
                if (GameControllerF.GetJerseyPositionsAtStart().ContainsKey(positionPlayerTested))
                {
                    playerScript.jersey = GameControllerF.GetJerseyPositionsAtStart()[positionPlayerTested];
                    playerScript.initPlayer();
                }
                else
                {
                    Debug.Log("Bug on init jerseys for player : " + playerScript.gameObject.name);
                }


            }

			GameObject.Find("Main Camera").GetComponent<Animator>().enabled = true;
			nextStateValidationRemaining = 4;
			state = Step.playerPlacement;
			break;
		
		case Step.playerPlacement :
			if( nextStateValidationRemaining != 0)
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
            twitterScript.SetCanDisplay(true);
            twitterScript.LaunchFirstTweet();

			GameObject.Find("ball_monster").GetComponent<MeshRenderer>().enabled = true;
            GameControllerF.GetMonster().GetComponent<MonsterControllerF>().RespawnBall();
			GameObject.Find("Main Camera").GetComponent<Animator>().SetTrigger("finalPosition");
            GameObject.Find("Main Camera").GetComponent<Animator>().enabled = false;
			GameControllerF.GetPlayer (1).GetComponent<PlayerControllerF>().Respawn();
			GameControllerF.GetPlayer (2).GetComponent<PlayerControllerF>().Respawn();
			GameControllerF.GetPlayer (3).GetComponent<PlayerControllerF>().Respawn();
			GameControllerF.GetPlayer (4).GetComponent<PlayerControllerF>().Respawn();

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
                Debug.Log(txtDuration.GetComponent<Animator>());
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

	public void validNextState(bool add)
    {
        if (add)
            nextStateValidationRemaining++;
        else
            nextStateValidationRemaining--;
	}

	IEnumerator WaitForKickOff(){

		kickOff = true;

        GameControllerF.GetMonster().transform.FindChild("ball_monster").GetComponent<MeshRenderer>().enabled = true;

        GameControllerF.GetMonster().GetComponent<MonsterControllerF>().RespawnBall();

		GameObject.Find ("Commentaries").GetComponent<TextCommentaries> ().WriteCommentary ("", "matchB");

        GameObject.Find("Main Camera").GetComponent<Animator>().enabled = false;

        twitterScript.SetCanDisplay(true);
        twitterScript.LaunchFirstTweet();

		yield return new WaitForSeconds(3.0f);

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

}
