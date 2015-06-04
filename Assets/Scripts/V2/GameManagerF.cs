using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerF : MonoBehaviour {

	public enum Step
	{
		opening,
		choosePlayer,
		playerPlacement,
		inGame
	};

	public Step state = Step.opening;
    private string blue = "#68C5EE";
    private string red = "#AB0101";

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

    public int whenToBeginEndTimer = 5;

    private GameObject lastPlayerHitting;



	// Use this for initialization

    void Start()
    {
        blueScoreTxt = GameControllerF.GetBlueScoreTxt();
        redScoreTxt = GameControllerF.GetRedScoreTxt();
        txtDuration = GameControllerF.GetTxtDuration();

        RefreshDuration();
        RefreshScore();
        StartCoroutine(matchDuration());
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("Blu "+bluScore+" - "+redScore+" Red");


		switch (state) {

		case Step.opening :
			break;
		case Step.choosePlayer :
			break;
		case Step.playerPlacement :
			break;

		default : 
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

    public GameObject GetLastPlayerHitting()
    {
        return lastPlayerHitting;
    }

    public void SetLastPlayerHitting(GameObject player)
    {
        lastPlayerHitting = player;
    }
}
