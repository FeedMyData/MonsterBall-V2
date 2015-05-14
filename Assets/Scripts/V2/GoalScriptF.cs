using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalScriptF : MonoBehaviour {

	public float dashGoal = 400.0f;

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
        PlayerControllerF player = other.GetComponent<PlayerControllerF>();

        if (other.tag == "Monster")
        {
            if (!other.GetComponent<MonsterControllerF>().IsMonsterForm())
            {
                //feedbacks
                Camera.main.GetComponent<CameraShake>().shake(0, 0, 0);

                manager.AddScore(tag);
                //tp au centre + invul de 3 secondes
                other.GetComponent<MonsterControllerF>().Respawn();
            }
        }

        if (player != null)
        {
            if (player.IsProjectionInGoal())
            {
                //feedbacks
                Camera.main.GetComponent<CameraShake>().shake(0, 0, 0);

                manager.AddScore(tag);
                player.Respawn();

            }
            else
            {
                player.callStun(player.stunGoal);
                Vector3 dirImpact;
                if (other.transform.position.x > transform.position.x)
                    dirImpact = new Vector3(-1, 0.1f, 0);
                else
                    dirImpact = new Vector3(1, 0.1f, 0);
                player.AddImpact(dirImpact * 200);
            }
        }
    }
}
