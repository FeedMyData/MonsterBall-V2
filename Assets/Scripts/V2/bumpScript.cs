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

                Vector3 dirImpact = other.transform.position - transform.position;

                player.PlayRandomSound(AbstractSound.Action.EjectBut);

                player.AddImpact(dirImpact.normalized * dashGoalPower);
            }
        }
    }
}
