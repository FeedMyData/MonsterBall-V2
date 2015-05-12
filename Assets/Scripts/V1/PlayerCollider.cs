using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

    private bool chooseDirection;
    private float homing;
    private PlayerController controller;
    private string tagEnemy;
    private GameObject ally;

    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        this.chooseDirection = controller.chooseDirection;
        this.homing = controller.homing;

        if (transform.parent.tag == "TeamRed")
            tagEnemy = "TeamBlu";
        else
            tagEnemy = "TeamRed";

        switch(controller.jersey){
            case PlayerController.Jersey.player1:
                ally = GameObject.Find("Player 2");
                break;
            case PlayerController.Jersey.player2:
                ally = GameObject.Find("Player 1");
                break;
            case PlayerController.Jersey.player3:
                ally = GameObject.Find("Player 4");
                break;
            case PlayerController.Jersey.player4:
                ally = GameObject.Find("Player 3");
                break;
            default:
                Debug.LogError("Le joueur n'a pas d'allié !");
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        float distance = Vector3.Distance(controller.transform.position, other.transform.position);
        //Debug.Log(distance);

        Vector3 direction;
        if (other.tag == "Monster")
        {
            if (!chooseDirection)
            {
                direction = other.transform.position - controller.transform.position;
            }
            else
            {
                direction = controller.transform.forward;
            }

            if (homing > 0)
            {
                //regarde l'angle entre la direction et l'allié 
                Vector3 directionToAlly = ally.transform.position - controller.transform.position;
                if (Mathf.Abs(Vector3.Angle(direction, directionToAlly)) < homing / 2)
                    direction = directionToAlly;
            }
            direction.Normalize();
            Debug.Log(direction);
            other.GetComponent<MonsterControllerV2>().HitByPlayer(direction, controller.GetAttack());
        }
        else if (other.tag == tagEnemy)
        {
            if (!chooseDirection)
            {
                direction = other.transform.position - controller.transform.position;
            }
            else
            {
                direction = controller.transform.forward;
            }
            other.GetComponent<PlayerController>().HitByPlayer(direction, controller.GetAttack());
        }
    }
}
