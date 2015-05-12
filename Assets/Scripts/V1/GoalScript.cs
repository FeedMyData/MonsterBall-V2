using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour
{

    public GameController controller;
    public Transform respawn;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            other.GetComponent<MonsterControllerV2>().Respawn(respawn.position, new Vector3(0, 10, -10));

            controller.AddScore(tag, 1);

            controller.ResetCamera();
        }
        if (other.tag == "TeamRed" || other.tag == "TeamBlu")
        {
            Vector3 direction;
            if (other.transform.position.x < transform.position.x)
                direction = new Vector3(-3, 1, 0);
            else
                direction = new Vector3(3, 1, 0);
            other.GetComponent<PlayerController>().KickFromGoal(direction);
        }
    }
}
