using UnityEngine;
using System.Collections;

public class bumpScript : MonoBehaviour {
    
    public float dashGoalPower = 200.0f;

    private GameManagerF manager;

    // Use this for initialization
    void Start()
    {
        manager = GameControllerF.getManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        PlayerControllerF player = other.GetComponent<PlayerControllerF>();

        if (player != null)
        {
            if (!player.IsProjectionInGoal())
            {
                player.callStun(player.stunGoal);
                player.sautBut++;

                Vector3 dirImpact = other.transform.position - transform.position;

                dirImpact.y = 0;

                if (Mathf.Abs(dirImpact.x) < 3.0f)
                {
                    dirImpact.x = 3.0f;
                }

                if(transform.localPosition.x < 0) {
                    dirImpact.x = Mathf.Abs(dirImpact.x);
                }
                else
                {
                    dirImpact.x = - Mathf.Abs(dirImpact.x);
                }

                player.AddImpact(dirImpact.normalized * dashGoalPower);

				if(player.GetComponentInChildren<Animator>())player.GetComponentInChildren<Animator>().SetTrigger("bump");
            }
        }
    }
}
