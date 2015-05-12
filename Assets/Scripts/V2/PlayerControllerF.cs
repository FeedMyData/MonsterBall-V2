using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerF : MonoBehaviour {

    


    [Header("Moving")]
    public float speed = 12.5f;
    [Range(0.0f, 100.0f)]
    public float speedMax = 100.0f;
    [Range(0.0f, 100.0f)]
    public float speedDribbling = 66.0f;
    [Range(0.0f, 100.0f)]
    public float speedLoading = 50.0f ;
    private float actualSpeed;
    private float gravity = 20.0f;
    private CharacterController controller;
    private Vector3 direction;
    

    [Header("Shoot")]
    public float powerMin = 50.0f;
    public float powerMax = 100.0f;
    public float coefPower = 10.0f;
    public float delayPowerMax = 4.0f;
    private Bonus bonus = null;
    private float chargingShoot = 0.0f;
    private float power;
    private bool loading = false;
    [Range(0.0f, 360.0f)]
    public float angleShoot = 180.0f;
    public float rangeShoot = 2.0f;
    [Range(0.0f, 360.0f)]
    public float angleDash = 35.0f;
    public float rangeDash = 3.0f;
    [Range(0.0f, 360.0f)]
    public float angleHoming = 30.0f;

    private Vector3 posDash = Vector3.zero;
    private bool dash = false;

    [Header("Stun")]
    public float stunKick = 0.5f;
    public float stunGoal = 0.5f;
    private float mass = 3.0f;
    private bool isStunned = false;
    private Vector3 impact = Vector3.zero;
    public float stunDash = 0.5f;

    private bool magnet = false;

    [Header("Custom")]
    public GameControllerF.Jersey jersey;
    public GameControllerF.Team team;
    private string horizontal;
    private string vertical;
    private string fire;


    private bool projectionInGoal;
    private GameObject goal;
    [Space(20)]
    public float durationInvul = 2.0f;
    private bool touchable = true;
    private SpriteRenderer spriteBonus;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        
        initPlayer();
        if (team == GameControllerF.Team.Blu)
            goal = GameControllerF.GetBluGoal();
        else
            goal = GameControllerF.GetRedGoal();

        spriteBonus = GetComponentInChildren<SpriteRenderer>();

        //angleDash *= Mathf.Deg2Rad;
        //angleHoming *= Mathf.Deg2Rad;
        //angleShoot *= Mathf.Deg2Rad;
	}

    // Update is called once per frame
    void Update()
    {
        if (bonus != null)
            spriteBonus.enabled = true;
        else
            spriteBonus.enabled = false;

        Move();
        if (!isStunned)
        {
            Attack();
        }
    }

    /**
     * Calcul la vitesse de la balle. le chargement d'un coup est prioritaire
     * 
     */
    float setSpeed()
    {
        float newSpeed = speed * speedMax/100;
        if (magnet)
            newSpeed = speed * speedDribbling / 100;
        if (loading)
            newSpeed = speed * speedLoading / 100; 
            

        return newSpeed;

    }

    /**
     * Initialise les controles du joueur, son équipe et sa couleur
     */
    void initPlayer()
    {
        switch (jersey)
        {
            case GameControllerF.Jersey.player1:
                horizontal = "Horizontal1";
                vertical = "Vertical1";
                fire = "Fire1";
                if (team == GameControllerF.Team.Blu)
                    GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);
                else
                    GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 255);
                break;
            case GameControllerF.Jersey.player2:
                horizontal = "Horizontal2";
                vertical = "Vertical2";
                fire = "Fire2";
                if (team == GameControllerF.Team.Blu)
                    GetComponent<Renderer>().material.color = new Color32(0, 128, 255, 255);
                else
                    GetComponent<Renderer>().material.color = new Color32(255, 128, 0, 255);
                break;
            case GameControllerF.Jersey.player3:
                horizontal = "Horizontal3";
                vertical = "Vertical3";
                fire = "Fire3";
                if (team == GameControllerF.Team.Blu)
                    GetComponent<Renderer>().material.color = new Color32(0, 255, 255, 255);
                else
                    GetComponent<Renderer>().material.color = new Color32(255, 115, 200, 255);
                break;
            case GameControllerF.Jersey.player4:
                horizontal = "Horizontal4";
                vertical = "Vertical4";
                fire = "Fire4";
                if (team == GameControllerF.Team.Blu)
                    GetComponent<Renderer>().material.color = new Color32(0, 0, 128, 255);
                else
                    GetComponent<Renderer>().material.color = new Color32(128, 0, 0, 255);
                break;
            default:
                Debug.LogError("L'objet " + name + " n'est attibué à aucun joueur ! Veuillez remplir le champ 'Jersey' !");
                break;

        }

        if (team == GameControllerF.Team.Blu)
        {
            tag = "TeamBlu";
        }
        else
            tag = "TeamRed";
    }

    /**
     * Gestion des déplacements et orientation à la manette
     */
    void Move()
    {
        actualSpeed = setSpeed();
        Vector3 directionMove = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical));

        if (impact.sqrMagnitude > 0.2f)
            directionMove = impact;
        else if (dash)
        {
            transform.position = Vector3.MoveTowards(transform.position, posDash, Time.deltaTime * 80);

            if (Vector3.Distance(transform.position, posDash) < 0.3f)
                dash = false;
        }
        else if (projectionInGoal)
        {
            transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, Time.deltaTime * 120);
        }
        else if (directionMove.sqrMagnitude > 0.2f && !isStunned && !dash)
        {
            transform.LookAt(transform.position + directionMove);
            directionMove = transform.forward * actualSpeed;
            direction = transform.forward;
			if(GetComponentInChildren<Animator>())GetComponentInChildren<Animator>().SetBool("isRunning", true);
        }
        else
        {
            directionMove = Vector3.zero;
			if(GetComponentInChildren<Animator>())GetComponentInChildren<Animator>().SetBool("isRunning", false);
        }
            
        directionMove.y -= gravity;
        controller.Move(directionMove * Time.deltaTime);

        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    /**
     * Gestion de l'attaque lors de la pression du bouton
     */
    void Attack()
    {
        if (Input.GetButtonDown(fire))
        {
            loading = true;
            power = powerMin;
            chargingShoot = 0;
        }

        if (chargingShoot < 1)
        {
            chargingShoot += Time.deltaTime / delayPowerMax;
        }

        if (Input.GetButton(fire))
        {
            power = Mathf.Lerp(power, powerMax, chargingShoot);
        }

        if (Input.GetButtonUp(fire) && loading)
        {
			Debug.Log ("test");
            loading = false;
			if(GetComponentInChildren<Animator>())GetComponentInChildren<Animator>().SetTrigger("hit");
            if (bonus != null)
            {
                bonus.Activated(transform.position);
                bonus = null;
            }
            else
            {
                List<GameObject> tabProxi = GameControllerF.PlayerView(this, rangeShoot, angleShoot);

                if (tabProxi.Count > 0)
                {
                    for (int i = 0; i < tabProxi.Count; i++)
                    {
                        Hit(tabProxi[i]);
                    }
                }
                else
                {
                    List<GameObject> tabProxiDash = GameControllerF.PlayerView(this, rangeDash, angleDash);

                    float distanceNearest = float.PositiveInfinity;
                    GameObject nearest = null;

                    for (int i = 0; i < tabProxiDash.Count; i++)
                    {
                        //Pikachu sert juste de stockage
                        float pikachu = GameControllerF.Distance(this.gameObject, tabProxiDash[i]);
                        if (tabProxiDash[i].tag != this.tag && pikachu < distanceNearest)
                        {
                            distanceNearest = pikachu;
                            nearest = tabProxiDash[i];
                        }
                    }

                    if (nearest != null)
                    {
                        if (Hit(nearest))
                        {
                            posDash = nearest.transform.position;
                            dash = true;
                            StartCoroutine(Stun(stunDash));
                        }
                    }
                }

            }

        }
    }

    public void FlyAway()
    {
        projectionInGoal = true;
    }

    /**
     * Action de frapper un objet
     * 
     * @param   obj     l'objet frappé
     * @return          à touché quelque chose
     * 
     */
    bool Hit(GameObject obj)
    {
        Vector3 directionImpact = GetDirectionNormalize() * power;
        directionImpact.y = 0.1f;

        PlayerControllerF player = obj.GetComponent<PlayerControllerF>();
        MonsterControllerF monster = obj.GetComponent<MonsterControllerF>();

        if (player != null && obj.tag != this.tag)
        {
            if (player.IsTouchable())
            {
                player.AddImpact(directionImpact);
                player.setLoading(false);
                player.callStun(stunKick);

                if (player.GetMagnet())
                {
                    //TODO: faire sauté la balle et la décrocher
                    GameObject ball = GameControllerF.GetMonster();
                    ball.GetComponent<MonsterControllerF>().callDisableMagnet();
                    //ball.GetComponent<MonsterControllerF>().Jump(2.0f);
                }
                return true;
            }
        }

        if (monster != null)
        {
            if (!monster.IsMonsterForm())
            {
                if (monster.IsTouchable())
                {
                    monster.callDisableMagnet();

                    if (GameControllerF.WhereIsMyAlly(this) != Vector3.zero)
                    {
                        directionImpact = GameControllerF.WhereIsMyAlly(this) - transform.position;
                        directionImpact.y = 0;
                        directionImpact.Normalize();
                        directionImpact *= power;
                    }
                    monster.GetComponent<Rigidbody>().AddForce(directionImpact, ForceMode.Impulse);
                    //TODO: changer pour une valeur proportionnelle



                    monster.AddWrath((int)(power/coefPower));

                    return true;
                }
            }
            else
            {
                //TODO: que faire si c'est un monstre
            }
        }
        return false;
    }

    /**
     * Permet d'assomer un joueur
     * 
     * @param   duration    Durée du stun
     */
    public void callStun(float duration)
    {
        StartCoroutine(Stun(duration));
    }

    /**
     * Permet d'assomer les joueurs ! Attention, à n'utiliser qu'à l'intérieur de l'objet,
     * pour Stun un autre joueur, utiliser callStun
     * 
     * @param   duration    durée du stun
     */
    IEnumerator Stun(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    /**
     * réécriture de la méthode AddForce dans le CharacterController
     * 
     * @param   force   Direction de la force appliquée
     */
    public void AddImpact(Vector3 force)
    {
        impact += force / mass;
    }
	
    /**
     * @return      le vecteur direction normalisé
     */
    public Vector3 GetDirectionNormalize(){
        Vector3 value = direction;
        value.y = 0;
        value.Normalize();
        
        return value;
    }

    public void SetMagnet(bool magnet)
    {
        this.magnet = magnet;
    }

    public bool GetMagnet()
    {
        return this.magnet;
    }

    public bool IsProjectionInGoal()
    {
        return projectionInGoal;
    }

    public void Respawn()
    {
        //TODO : Rajouter une composante aléatoire en x et z
        projectionInGoal = false;
        transform.position = new Vector3(0, 5, 0);

        StartCoroutine(Intouchable());
    }

    public bool IsTouchable()
    {
        return touchable;
    }

    IEnumerator Intouchable()
    {
        //clignote et ignore tous le coups
        touchable = false;
        StartCoroutine(Blink());
        yield return new WaitForSeconds(durationInvul);
        touchable = true;
    }

    IEnumerator Blink()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        while (!touchable)
        {
            mr.enabled = !mr.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        mr.enabled = true;
    }

    public void SetBonus(Bonus bonus)
    {
        spriteBonus.sprite = bonus.GetSprite();
        this.bonus = bonus;
    }

    public Bonus getBonus()
    {
        return bonus;
    }

    public void setLoading(bool loading)
    {
        this.loading = loading;
    }
}