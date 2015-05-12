using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float mass = 0.3f;
    public float speed = 6.0F;
    public float speedWhileDribble = 2.0F;
    public float speedWhileCharge = 2.0F;
    private float actualSpeed;
    public float gravity = 20.0F;

    public float kickByPlayer = 0.5f;
    public float kickByGoal = 0.5f;

    public bool chooseDirection = true;
    public float homing = -1f;

    public bool magnet;
    private bool hasBall = false;

    public Color chargedColor;

    private bool isStunned = false;

    public enum Jersey
    {
        player1,
        player2,
        player3,
        player4
    };

    public Jersey jersey;

    public GameController gameController;
    
    public Material teamBlu;
    public Material teamRed;

    private Transform stick;
    private Animator stick_animator;
    private Transform circle;
    private Collider halfArcCollider;

    private float attack = 50f;
    public float delayCharge = 2.0f;
    private int levelCharge = 10;
    private float chargeInProgress;

    private Vector3 impact = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private string horizontal;
    private string vertical;
    private string fire;

    private Transform positionBall;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        actualSpeed = speed;

        Transform[] stuff = GetComponentsInChildren<Transform>();

        foreach (Transform item in stuff)
        {
            if (item.name == "Stick")
            {
                stick = item;
                stick_animator = item.GetComponent<Animator>();
            }
            else if (item.name == "Circle")
                circle = item;
            else if (item.name == "HalfArcCollider")
                halfArcCollider = item.GetComponent<Collider>();
        }

        halfArcCollider.enabled = false;

        InitPlayer();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isStunned)
        {
            moveDirection = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical));
            PlayerMove();
            PlayerAttack();
        }
        else
        {
            moveDirection = Vector3.zero;
            moveDirection.y -= gravity;
            controller.Move(moveDirection * Time.deltaTime);
        }

        if (impact.sqrMagnitude > 0.2f)
            controller.Move(impact * Time.deltaTime);
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);

        stick.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, chargedColor, (attack-50)/50);
	}

    void PlayerAttack()
    {
        
        if (Input.GetButton(fire) && chargeInProgress < Time.time && attack < 100)
        {
            actualSpeed = speedWhileCharge;
            chargeInProgress = Time.time + (delayCharge / levelCharge);
            attack += 10;
        }
        if (Input.GetButtonUp(fire))
        {
            actualSpeed = speed;

            StartCoroutine(ActiveTrigger());

            //Animation du baton
            stick_animator.SetTrigger("Hit");
        }
    }

    IEnumerator ActiveTrigger()
    {
        halfArcCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        halfArcCollider.enabled = false;
        attack = 50;
    }

    void PlayerMove()
    {
        transform.LookAt(transform.position + moveDirection);
        if (Vector3.SqrMagnitude(moveDirection) != 0)
            moveDirection = transform.forward * actualSpeed;
        moveDirection.y -= gravity;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void InitPlayer()
    {
        switch(jersey){
            case Jersey.player1 :
                horizontal = "Horizontal1";
                vertical = "Vertical1";
                fire = "Fire1";
                circle.GetComponent<SpriteRenderer>().color = new Color(1f,0.5f,0f,0.5f);
                break;
            case Jersey.player2 :
                horizontal = "Horizontal2";
                vertical = "Vertical2";
                fire = "Fire2";
                circle.GetComponent<SpriteRenderer>().color = new Color(1f, 0.2f, 0.2f, 0.5f);
                break;
            case Jersey.player3 :
                horizontal = "Horizontal3";
                vertical = "Vertical3";
                fire = "Fire3";
                circle.GetComponent<SpriteRenderer>().color = new Color(0f, 0.4f, 0.4f, 0.5f);
                break;
            case Jersey.player4 :
                horizontal = "Horizontal4";
                vertical = "Vertical4";
                fire = "Fire4";
                circle.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0f, 0.4f, 0.5f);
                break;
            default :
                Debug.LogError("L'objet " + name + " n'est attibué à aucun joueur ! Veuillez remplir le champ 'Jersey' !");
                break;
        }


        if (tag == "TeamBlu")
            GetComponent<Renderer>().material = teamBlu;
        else if (tag == "TeamRed")
            GetComponent<Renderer>().material = teamRed;
        else
            Debug.LogError("Le joueur " + name + " n'est dans aucune équipe ! Appliquez lui un tag !");

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Monster")
        {
            if (magnet)
            {
                SetHasBall(hit.gameObject.GetComponent<MonsterControllerV2>().SetMagnet(gameObject));
            }
            else
                hit.gameObject.GetComponent<MonsterController>().PushByPlayer(transform.position);
        }
            
        
    }

    public IEnumerator Stun(float duration)
    {
        isStunned = true;
        SetHasBall(false);
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    public void HitByPlayer(Vector3 direction, float power)
    {
        StartCoroutine(Stun(kickByPlayer));
        direction.y = 0f;
        AddImpact(direction,power);

        attack = 50;
    }

    public void AddImpact(Vector3 direction, float power)
    {
        power /= 10;
        direction.Normalize();
        direction.y = 0.2f;
        impact += direction * power / mass;
    }

    public void KickFromGoal(Vector3 direction)
    {
        StartCoroutine(Stun(kickByGoal));
        impact += direction * 10 / mass;
    }

    public void Dash(Vector3 to)
    {
        controller.Move(to *3* speed * Time.deltaTime);
        StartCoroutine(Stun(1f));
        //le joueur se place à la position de la balle
        //Vector3 direction = transform. 

        //transform.position = Vector3.Lerp(transform.position, to, 5 * Time.deltaTime);
        
    }

    public float GetAttack()
    {
        //Debug.Log(attack);
        return attack;
    }

    public void SetAttack(float attack)
    {
        this.attack = attack;
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public void Expulse()
    {
        Transform goalTarget;
        Vector3 newPosition; 
        
        if (tag == "TeamBlu"){
            goalTarget = GameObject.Find("TriggerBluGoal").transform;
            newPosition = goalTarget.position;
            newPosition.x += 4;
            gameController.AddScore("TeamBlu", 1);
        }
        else{
            goalTarget = GameObject.Find("TriggerRedGoal").transform;
            newPosition = goalTarget.position;
            newPosition.x -= 4;
            gameController.AddScore("TeamRed", 1);
        }

        StartCoroutine(Stun(1));
        transform.position = newPosition;
                
    }

    public void SetHasBall(bool hasBall)
    {
        if (hasBall)
        {
            //link ball et joueur
            //Ralentir joueur
            actualSpeed = speedWhileDribble;
        }
        else
        {
            actualSpeed = speed;
        }
        this.hasBall = hasBall;
    }
}
