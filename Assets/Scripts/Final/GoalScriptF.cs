using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalScriptF : MonoBehaviour {

    private GameManagerF manager;

	// Use this for initialization
	void Start () {
	    manager = GameControllerF.getManager();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Monster")
        {
            if (!other.GetComponent<MonsterControllerF>().IsMonsterForm())
            {
                manager.AddScore(tag);
                //tp au centre + invul de 3 secondes
                other.GetComponent<MonsterControllerF>().Respawn();
            }
        }

        if (other.tag == "TeamBlu" || other.tag == "TeamRed")
        {
            if (other.GetComponent<PlayerControllerF>().IsProjectionInGoal())
            {
                manager.AddScore(tag);
                other.GetComponent<PlayerControllerF>().Respawn();
            }
            else
            {
                Vector3 dirImpact;
                if (other.transform.position.x > transform.position.x)
                    dirImpact = new Vector3(-1, 0.1f, 0);
                else
                    dirImpact = new Vector3(1, 0.1f, 0);
                other.GetComponent<PlayerControllerF>().AddImpact(dirImpact * 200);
            }
        }
    }
}
