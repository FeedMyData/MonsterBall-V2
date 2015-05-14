using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerF : MonoBehaviour {

    private int bluScore = 0;
    private int redScore = 0;
    private Text txtScore;
    private Text txtDuration;

    public int durationInSecond;

    public float timeBetweenTwoBonus = 45f;
    private float rngBonus = 0.1f;
    private bool bonusCanPopUp = true;

	// Use this for initialization
	void Awake () {
        txtScore = GameControllerF.GetTxtScore();
        txtDuration = GameControllerF.GetTxtDuration();
        
	}

    void Start()
    {
        RefreshDuration();
        RefreshScore();
        StartCoroutine(matchDuration());
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("Blu "+bluScore+" - "+redScore+" Red");
        if (durationInSecond <= 0)
        {
            Time.timeScale = 0;
        }

        
	}

    IEnumerator matchDuration()
    {
        while (durationInSecond > 0)
        {
            if (bonusCanPopUp && Random.value <= rngBonus)
            {
                bonusCanPopUp = false;
                StartCoroutine(RearmBonus());
                Debug.Log("CreateBonus");
                //CreateBonus();
                //active un bonus
            }
            yield return new WaitForSeconds(1);
            durationInSecond--;
            RefreshDuration();
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
        }
        else
        {
            bluScore++;
        }
        RefreshScore();
    }

    void RefreshScore()
    {
        txtScore.text = ("Blu "+bluScore+"-"+redScore+" Red");
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
}
