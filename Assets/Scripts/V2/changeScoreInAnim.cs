using UnityEngine;
using System.Collections;

public class changeScoreInAnim : MonoBehaviour {

    private GameManagerF gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameController").GetComponent<GameManagerF>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void changeScore()
    {
        gameManager.RefreshScore();
    }

}
