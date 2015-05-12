using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

    private int wrath = 0;

    public GameController controller;

    public float speed = 2.0f;

    public bool isControllable;

    private Rigidbody body;
    private Vector3 moveDirection;

    private Collider[] colliders;

    private string horizontal = "Horizontal5";
    private string vertical = "Vertical5";

    private Vector3 previousPosition;
    private Vector3 actualDirection;
    private Vector3 previousDirection;

    public float goalSensibility = 0.1f;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        colliders = GetComponents<Collider>();

        previousPosition = transform.position;
        previousDirection = Vector3.zero;

        Debug.Log(GameControllerF.FieldSize());
	}

    bool CompareVector(Vector3 v1, Vector3 v2)
    {
        if (Mathf.Approximately(v1.x,v2.x)) return false;
        if (Mathf.Approximately(v1.y,v2.y)) return false;
        if (Mathf.Approximately(v1.z,v2.z)) return false;

        return true;
    }

    void SlowMo()
    {
        actualDirection = transform.position - previousPosition;


        if (!CompareVector(actualDirection, previousDirection))
        {
            //on fait le raycast ici
            RaycastHit hit;

            if (Physics.Raycast(transform.position, actualDirection, out hit, 100.0F))
            {
                Debug.Log(actualDirection.ToString());

                if ((hit.collider.name == "RedGoal" || hit.collider.name == "BluGoal") && actualDirection.sqrMagnitude > goalSensibility)
                {
                    Time.timeScale = 0.05f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }

        }

        previousPosition = transform.position;
        previousDirection = actualDirection;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //TODO: SlowMO
        //SlowMo();

        if (transform.position.y < -2)
        {
            Respawn(new Vector3(0, 10, 0), new Vector3(0, 0, 0));
        }

        if (isControllable)
        {
            moveDirection = new Vector3(Input.GetAxisRaw(horizontal), 0, Input.GetAxisRaw(vertical));
            
            transform.LookAt(transform.position + moveDirection);
            
            if (Vector3.Magnitude(moveDirection) != 0)
            {
                colliders[0].enabled = true;
                colliders[1].enabled = false;
                moveDirection = transform.forward * speed;
            }
            else
            {
                colliders[0].enabled = false;
                colliders[1].enabled = true;
            }
            //controller.Move(moveDirection * Time.deltaTime);
            body.AddForce(moveDirection,ForceMode.Acceleration);
        }
	}

    public void PushByPlayer(Vector3 positionPlayer)
    {
        //TODO: Faire varier la puissance en fonction de la colère du monster
        Vector3 directionForce = new Vector3(transform.position.x - positionPlayer.x, 0, transform.position.z - positionPlayer.z);
        float power = 0.2f;
        body.AddForce(directionForce * power, ForceMode.VelocityChange);
    }

    /*public void HitByPlayerInChoosenDirection(Vector3 directionPlayer, float powerOfAttack)
    {
        Vector3 directionForce = new Vector3(directionPlayer.x, 0.2f,directionPlayer.z);
        powerOfAttack /= 3;
        wrath += 10;
        controller.SetWrathMonster(wrath);

        body.AddForce((directionForce) * powerOfAttack, ForceMode.VelocityChange);

    }*/

    public void HitByPlayer(Vector3 directionForce, float powerOfAttack)
    {
        //Vector3 directionForce= new Vector3(transform.position.x - positionPlayer.x, 0f, transform.position.z - positionPlayer.z);
        powerOfAttack /= 3;
        directionForce.y = 0f;

        directionForce.Normalize();
        directionForce.y = 0.2f;
        wrath += 10;
        controller.SetWrathMonster(wrath);
            
        body.AddForce((directionForce) * powerOfAttack,ForceMode.VelocityChange);
    }

    public void Respawn(Vector3 newPosition, Vector3 newDirection)
    {
        transform.position = newPosition;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.AddForce(newDirection, ForceMode.Impulse);
    }
}
