using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerF : MonoBehaviour
{




    [Header("Moving")]
    public float speed = 12.5f;
    [Range(0.0f, 100.0f)]
    public float speedMax = 100.0f;
    [Range(0.0f, 100.0f)]
    public float speedDribbling = 66.0f;
    [Range(0.0f, 100.0f)]
    public float speedLoading = 50.0f;
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
    private bool canHit = true;
    private float chargingShoot = 0.0f;
    private float power;
    private bool loading = false;
    [Range(0.0f, 360.0f)]
    public float angleShoot = 180.0f;
    public float rangeShoot = 2.0f;
    //[Range(0.0f, 360.0f)]
    //public float angleDash = 35.0f;
    //public float rangeDash = 3.0f;
    [Range(0.0f, 360.0f)]
    public float angleHoming = 30.0f;

    public float multiImpact = 3.0f;
    public float durationBetweenTwoShoot = 1.0f;
    private bool waitBeforeNextShoot = false;

    //private Vector3 posDash = Vector3.zero;
    //private bool dash = false;

    [Header("Stun")]
    public float stunKick = 0.5f;
    public float stunGoal = 0.5f;
    private bool isStunned = false;
    private Vector3 impact = Vector3.zero;
    //public float stunDash = 0.5f;

    private bool magnet = false;

    [Header("Custom")]
    public GameControllerF.Jersey jersey;
    public GameControllerF.Team team;
    private string horizontal;
    private string vertical;
    private string fire;


    private bool projectionInGoal;
    private Transform goal;
    [Space(20)]
    public float durationInvul = 2.0f;
    public float durationInvulSiCoupRecu = 1.0f;
    private bool touchable = true;
    private SpriteRenderer spriteBonus;
    private SpriteRenderer spriteGround;

    private TeleportationF tp;

    private SoundManager sound;

    [HideInInspector]
    public bool isRunning;

    /* Statistiques */
    [HideInInspector]
    public int coupsDonnes = 0;
    [HideInInspector]
    public int coupsRecus = 0;
    [HideInInspector]
    public int coupsCharges = 0;
    [HideInInspector]
    public int joueurMange = 0;
    [HideInInspector]
    public int sautBut = 0;
    [HideInInspector]
    public int coupsVide = 0;
    [HideInInspector]
    public int marqueBut = 0;
    [HideInInspector]
    public int coupsDonnesSurBalle = 0;

    private GameManagerF manager;

    private Vector3 respawnPosition = new Vector3(0,5,0);

    [HideInInspector]
    public bool isEaten = false;
    [HideInInspector]
    public bool canCount = true;

    private int positionControllerSelection = 0;
    private float positionYControllerSelection;
    private Vector3 positionXZNeutral;
    private float[] positionsY;
    private Vector3 positionControllerSprite;
    private SpriteRenderer controllerSprite;
    private SpriteRenderer[] arrows = new SpriteRenderer[2];
    private Sprite[] spritesController;
    private string[] namesSpriteSheetController;
    private bool isValidated = false;
    private bool hasMadeInputHorizontal = false;
    private bool hasArrived = false;
    private bool hasBegunRunning = false;

    void Awake()
    {
        spritesController = Resources.LoadAll<Sprite>("Controller");
        namesSpriteSheetController = new string[spritesController.Length];

        for (int i = 0; i < namesSpriteSheetController.Length; i++)
        {
            namesSpriteSheetController[i] = spritesController[i].name;
        }
    }

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        tp = GetComponentInChildren<TeleportationF>();
        manager = GameControllerF.getManager();
        positionXZNeutral = GameControllerF.GetPositionXZNeutral();
        positionsY = GameControllerF.GetPositionsY();

        SpriteRenderer[] tabSprite = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < tabSprite.Length; i++)
        {

            if (tabSprite[i].name == "Circle")
                spriteGround = tabSprite[i];
            else if (tabSprite[i].name == "SpriteBonus")
                spriteBonus = tabSprite[i];
            else if (tabSprite[i].name == "ControllerSprite")
            {
                controllerSprite = tabSprite[i];
                controllerSprite.enabled = false;
                controllerSprite.sprite = spritesController[System.Array.IndexOf(namesSpriteSheetController, "whiteController")];
            }
            else if (tabSprite[i].name == "LeftA")
            {
                arrows[0] = tabSprite[i];
                arrows[0].enabled = false;
                arrows[0].sprite = spritesController[System.Array.IndexOf(namesSpriteSheetController, "leftArrow")];
            }
            else if (tabSprite[i].name == "RightA")
            {
                arrows[1] = tabSprite[i];
                arrows[1].enabled = false;
                arrows[1].sprite = spritesController[System.Array.IndexOf(namesSpriteSheetController, "rightArrow")];
            }
                
        }
        if (GameControllerF.getManager().state != GameManagerF.Step.quickTest)
        {
            initControllers();
        }
        initRespawnsAndColorTag();
        initPlayer();
        if (team == GameControllerF.Team.Blu)
            goal = GameControllerF.GetPosBluGoal();
        else
            goal = GameControllerF.GetPosRedGoal();


        sound = GetComponent<SoundManager>();
        sound.LoadBank();

        //nivekSound = GetComponent<NivekSound>();
        //angleDash *= Mathf.Deg2Rad;
        //angleHoming *= Mathf.Deg2Rad;
        //angleShoot *= Mathf.Deg2Rad;

        //tp.SetTeleportation(true);

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -3.0f) Respawn();

        if (bonus != null)
            spriteBonus.enabled = true;
        else
            spriteBonus.enabled = false;


		if (GameControllerF.getManager().state == GameManagerF.Step.inGame)
			Move();
		else if (GameControllerF.getManager ().state == GameManagerF.Step.playerPlacement)
        {
            MoveToSpawn();
        }
        else if (GameControllerF.getManager().state == GameManagerF.Step.choosePlayer)
        {
            if (Input.GetAxis(horizontal) > 0.4f && !isValidated && !hasMadeInputHorizontal) // droite
            {
                hasMadeInputHorizontal = true;
                MoveControllerSelection(true);
            }
            else if (Input.GetAxis(horizontal) < -0.4f && !isValidated && !hasMadeInputHorizontal) // gauche
            {
                hasMadeInputHorizontal = true;
                MoveControllerSelection(false);
            }
            else if (Input.GetAxis(horizontal) < 0.4f && Input.GetAxis(horizontal) > -0.4f)
            {
                hasMadeInputHorizontal = false;
            }

            if (Input.GetButtonDown(fire))
            {
                ValidInvalidSelection();
            }
        }
	
		if (canHit && !isStunned && GameControllerF.getManager().state == GameManagerF.Step.inGame && !waitBeforeNextShoot)
        {
//            Debug.Log("test");
            Attack();
        }
    }

    /**
     * Calcul la vitesse de la balle. le chargement d'un coup est prioritaire
     * 
     */
    float setSpeed()
    {
        float newSpeed = speed * speedMax / 100;
        if (magnet)
            newSpeed = speed * speedDribbling / 100;
        if (loading)
            newSpeed = speed * speedLoading / 100;


        return newSpeed;

    }

    void initControllers()
    {
        switch (jersey)
        {
            case GameControllerF.Jersey.player1:
                positionYControllerSelection = positionsY[3];
                break;
            case GameControllerF.Jersey.player2:
                positionYControllerSelection = positionsY[2];
                break;
            case GameControllerF.Jersey.player3:
                positionYControllerSelection = positionsY[1];
                break;
            case GameControllerF.Jersey.player4:
                positionYControllerSelection = positionsY[0];
                break;
            default:
                positionYControllerSelection = positionXZNeutral.y;
                break;
        }

        positionControllerSprite = new Vector3(positionXZNeutral.x, positionYControllerSelection, positionXZNeutral.z);
        controllerSprite.transform.position = positionControllerSprite;
        controllerSprite.enabled = true;

        Vector3 positionLArrow = new Vector3(positionXZNeutral.x - 1.5f, positionYControllerSelection, positionXZNeutral.z);
        arrows[0].transform.position = positionLArrow;
        arrows[0].enabled = true;

        Vector3 positionRArrow = new Vector3(positionXZNeutral.x + 1.5f, positionYControllerSelection, positionXZNeutral.z);
        arrows[1].transform.position = positionRArrow;
        arrows[1].enabled = true;

    }

    void initRespawnsAndColorTag()
    {
        switch (jersey)
        {
            case GameControllerF.Jersey.player1:
                respawnPosition = GameObject.Find("respawn player 1").transform.position;
                break;
            case GameControllerF.Jersey.player2:
                respawnPosition = GameObject.Find("respawn player 2").transform.position;
                break;
            case GameControllerF.Jersey.player3:
                respawnPosition = GameObject.Find("respawn player 3").transform.position;
                break;
            case GameControllerF.Jersey.player4:
                respawnPosition = GameObject.Find("respawn player 4").transform.position;
                break;
            default:
                positionYControllerSelection = positionXZNeutral.y;
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
     * Initialise les controles du joueur, son équipe et sa couleur
     */
    public void initPlayer()
    {
        switch (jersey)
        {
            case GameControllerF.Jersey.player1:
                horizontal = "Horizontal1";
                vertical = "Vertical1";
                fire = "Fire1";
                //if (team == GameControllerF.Team.Blu)
                //    spriteGround.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);
                //else
                //    spriteGround.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 255);
                break;
            case GameControllerF.Jersey.player2:
                horizontal = "Horizontal2";
                vertical = "Vertical2";
                fire = "Fire2";
                //if (team == GameControllerF.Team.Blu)
                //    spriteGround.GetComponent<Renderer>().material.color = new Color32(0, 128, 255, 255);
                //else
                //    spriteGround.GetComponent<Renderer>().material.color = new Color32(255, 128, 0, 255);
                break;
            case GameControllerF.Jersey.player3:
                horizontal = "Horizontal3";
                vertical = "Vertical3";
                fire = "Fire3";
                //if (team == GameControllerF.Team.Blu)
                //    spriteGround.GetComponent<Renderer>().material.color = new Color32(0, 255, 255, 255);
                //else
                //    spriteGround.GetComponent<Renderer>().material.color = new Color32(255, 115, 200, 255);
                break;
            case GameControllerF.Jersey.player4:
                horizontal = "Horizontal4";
                vertical = "Vertical4";
                fire = "Fire4";
                //if (team == GameControllerF.Team.Blu)
                //    spriteGround.GetComponent<Renderer>().material.color = new Color32(0, 0, 128, 255);
                //else
                //    spriteGround.GetComponent<Renderer>().material.color = new Color32(128, 0, 0, 255);
                break;
            default:
                Debug.LogError("L'objet " + name + " n'est attibué à aucun joueur ! Veuillez remplir le champ 'Jersey' !");
                break;

        }

    }

    /**
     * Gestion des déplacements et orientation à la manette
     */
    void Move()
    {
        actualSpeed = setSpeed();
        Vector3 directionMove = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical));
		/*if (Input.GetKeyDown (KeyCode.A))
		if (GetComponentInChildren<Animator> ())
			GetComponentInChildren<Animator> ().SetTrigger ("fly");*/
        if (impact.sqrMagnitude > 0.2f)
            directionMove = impact;
        /*else if (dash)
        {
            transform.position = Vector3.MoveTowards(transform.position, posDash, Time.deltaTime * 80);

            if (Vector3.Distance(transform.position, posDash) < 0.3f)
                dash = false;
        }*/
        else if (projectionInGoal)
        {
            transform.position = Vector3.MoveTowards(transform.position, goal.position, Time.deltaTime * 120);
        }
        else if (directionMove.sqrMagnitude > 0.2f && !isStunned /*&& !dash*/)
        {
            isRunning = true;
            transform.LookAt(transform.position + directionMove);
            directionMove = transform.forward * actualSpeed;
            direction = transform.forward;
            if (GetComponentInChildren<Animator>() && GetComponentInChildren<Animator>().GetBool("isRunning") == false)
                GetComponentInChildren<Animator>().SetBool("isRunning", true);
        }
        else
        {
            isRunning = false;
            directionMove = Vector3.zero;
            if (GetComponentInChildren<Animator>() && GetComponentInChildren<Animator>().GetBool("isRunning") == true)
                GetComponentInChildren<Animator>().SetBool("isRunning", false);
        }

        directionMove.y -= gravity;
        if (!isEaten)
        {
            controller.Move(directionMove * Time.deltaTime);
        }

        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    /**
     * Gestion de l'attaque lors de la pression du bouton
     */
    void Attack()
    {
        if (Input.GetButtonDown(fire))
        {

            sound.PlayEvent("SFX_Niveks_ChargeCoup",gameObject);
            loading = true;
            power = powerMin;
            chargingShoot = 0;
            if (GetComponentInChildren<Animator>() && GetComponentInChildren<Animator>().GetBool("isCharging") == false){

                GetComponentInChildren<Animator>().SetBool("isCharging", true);
				foreach(Animator animator in GetComponentsInChildren<Animator>())
                {
                    if(animator.name == "arme")
                        animator.SetTrigger("grow");
                }
			}

			//Feedback
			foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
				if (ps.name == "Particles_charge")ps.Play ();
			}
        }

        if (chargingShoot < 1)
        {
            chargingShoot += Time.deltaTime / delayPowerMax;

        }

        if (Input.GetButton(fire))
        {
            power = Mathf.Lerp(power, powerMax, chargingShoot);

            if (power >= powerMax)
            {
                sound.PlayEvent("SFX_Niveks_ChargeAttente",gameObject);
                sound.StopEvent("SFX_Niveks_ChargeCoup", gameObject,0);
			}

			// Feedback
			if (power >= powerMax)
			{
				foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
					if (ps.name == "Particles_charge")ps.startColor = new Color(1,0,1);
				}
			}

			if (power < powerMax && team == GameControllerF.Team.Red)
			{
				foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
					if (ps.name == "Particles_charge")ps.startColor = Color.red;
				}
			}
			if (power < powerMax && team == GameControllerF.Team.Blu)
			{
				foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
					if (ps.name == "Particles_charge")ps.startColor = Color.blue;
				}
			}
		}
        

        if (Input.GetButtonUp(fire))
        {
            //float valueCirclePlayer = GameControllerF.InCircle(gameObject);
            

            if (loading) // && valueCirclePlayer<0.70f
            {
                if (GetComponentInChildren<Animator>() && GetComponentInChildren<Animator>().GetBool("isCharging") == true)
                {

                    GetComponentInChildren<Animator>().SetBool("isCharging", false);

                    sound.StopEvent("SFX_Niveks_ChargeAttente", gameObject, 50);
                    sound.StopEvent("SFX_Niveks_ChargeCoup", gameObject, 50);

					//Feedback
						foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>()) {
							if (ps.name == "Particles_charge")ps.Stop();
					}
                }
                loading = false;
                if (bonus != null)
                {
                    bonus.Activated(transform.position);
                    bonus = null;
                }
                else
                {

					// Feedback
					foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
					{
						if (ps.name == "Batte_effet") ps.Play();
					}


                    List<GameObject> tabProxi = GameControllerF.PlayerView(this, rangeShoot, angleShoot);

                    if (tabProxi.Count > 0)
                    {
                        bool onePunch = false;
                        for (int i = 0; i < tabProxi.Count; i++)
                        {
                            if (Hit(tabProxi[i]))
                            {
                                onePunch = true;
                            }
                        }

                        if (onePunch)
                        {
                            if (power > (powerMax - powerMax * 0.1f))
                            {
                                sound.PlayEvent("SFX_Niveks_CoupFort", gameObject);
                                coupsCharges++;
                                coupsDonnes++;
                            }
                            else
                            {
                                sound.PlayEvent("SFX_Niveks_CoupFaible", gameObject);
                                coupsDonnes++;
                            }
                        }
                        else
                        {
                            sound.PlayEvent("SFX_Niveks_Woosh", gameObject);
                            coupsVide++;
                        }

                        /*if (GetComponentInChildren<Animator>())
                            GetComponentInChildren<Animator>().SetTrigger("hit");
                        return;*/
                    }
                    else
                    {
                        sound.PlayEvent("SFX_Niveks_Woosh", gameObject);
                        coupsVide++;
                    }

                    /*else
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
                                if (GetComponentInChildren<Animator>())
                                    GetComponentInChildren<Animator>().SetTrigger("dash");
                                return;
                            }
                        }
                    }*/
                    if (GetComponentInChildren<Animator>())
                    {

                        GetComponentInChildren<Animator>().SetTrigger("hit");
                        //Debug.Log("hit");
                        foreach (Animator animator in GetComponentsInChildren<Animator>())
                            if (animator.name == "arme")
                                animator.SetTrigger("shrink");
                    }

                }

                waitBeforeNextShoot = true;
                StartCoroutine(WaitBeforeNextShoot());

            }
            else
            {
                loading = false;
                if (GetComponentInChildren<Animator>() && GetComponentInChildren<Animator>().GetBool("isCharging") == true)
                {

                    GetComponentInChildren<Animator>().SetBool("isCharging", false);

                    sound.StopEvent("SFX_Niveks_ChargeAttente", gameObject, 50);
                    sound.StopEvent("SFX_Niveks_ChargeCoup", gameObject, 50);
                }

                //if (valueCirclePlayer >= 0.7f)
                //{
                //    sound.PlayEvent("SFX_Niveks_Woosh", gameObject);
                //}
                //relache la pression et casse l'anim
            }
        }
    }

    IEnumerator WaitBeforeNextShoot()
    {
        yield return new WaitForSeconds(durationBetweenTwoShoot);
        waitBeforeNextShoot = false;
    }

    public void FlyAway()
    {
        projectionInGoal = true;
		if (GetComponentInChildren<Animator> ()) {
			if (GetComponentInChildren<Animator>().GetBool("isCharging") == true)
				GetComponentInChildren<Animator>().SetBool("isCharging", false);
			if (GetComponentInChildren<Animator>().GetBool("isRunning") == true)
				GetComponentInChildren<Animator>().SetBool("isRunning", false);
			GetComponentInChildren<Animator> ().SetTrigger ("fly");
		}


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
        //OnClickHitEvent();

        Vector3 directionImpact = GetDirectionNormalize() * power;
        directionImpact.y = 0.1f;

        PlayerControllerF player = obj.GetComponent<PlayerControllerF>();
        MonsterControllerF monster = obj.GetComponent<MonsterControllerF>();

        if (player != null && obj.tag != this.tag)
        {
            if (player.IsTouchable())
            {
                sound.PlayEvent("VX_Niveks_Coup", player.gameObject);
                player.coupsRecus++;
                player.AddImpact(directionImpact * multiImpact);
                player.setLoading(false);
                player.callStun(stunKick);
                player.callIntouchable();

                // feedbacks player frappe autre player
                foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
                {
                    if (ps.name == "Choc") ps.Play();
                }
				if(player.GetComponentInChildren<Animator>()){
					player.GetComponentInChildren<Animator>().SetTrigger("hurt");
					player.GetComponentInChildren<Animator>().SetBool("isCharging", false);
				}
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
            if (!monster.IsMonsterForm() && !monster.IsTransforming())
            {
                if (monster.IsTouchable())
                {
                   // monster.OnClickHitEvent += ;

                    if (monster.GetMagnet() == null || monster.GetMagnet() == this.gameObject || (monster.GetMagnet() != null && monster.GetMagnet().GetComponent<PlayerControllerF>().team != team))
                    {
                        monster.callDisableMagnet();
                        monster.SetStriker(this);
                        sound.PlayEvent("VX_Balle_Coup", monster.gameObject);

                        if (GameControllerF.WhereIsMyAlly(this) != Vector3.zero)
                        {
                            directionImpact = GameControllerF.WhereIsMyAlly(this) - transform.position;
                            directionImpact.Normalize();
                            directionImpact.y = 0;
                            directionImpact *= power;
                        }
                        
                        monster.GetComponent<Rigidbody>().AddForce(directionImpact, ForceMode.Impulse);
                        //TODO: changer pour une valeur proportionnelle

                        monster.AddWrath((int)(power / coefPower));

                        coupsDonnesSurBalle++;

                        //feedbacks balle coup reçu par un joueur
                        if (power == powerMax)
                        {
                            Camera.main.GetComponent<CameraShake>().shake(0.6f, 0.4f, 1.0f);

                        }

                        return true;
                    }
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
        if(duration != 0.0f)
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
        impact += force;
    }

    /**
     * @return      le vecteur direction normalisé
     */
    public Vector3 GetDirectionNormalize()
    {
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
        transform.position = respawnPosition;
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
        canHit = false;
        StartCoroutine(Blink());
        yield return new WaitForSeconds(durationInvul);
        canHit = true;
        touchable = true;
    }

    public void callIntouchable()
    {
        StartCoroutine(IntouchableSiCoupRecu());
    }

    IEnumerator IntouchableSiCoupRecu()
    {
        touchable = false;
        yield return new WaitForSeconds(this.durationInvulSiCoupRecu);
        touchable = true;
    }

    IEnumerator Blink()
    {
        //MeshRenderer mr = GetComponent<MeshRenderer>();
        Renderer smr = GetComponentInChildren<Renderer>();
        Renderer arme = GetComponentInChildren<Renderer>();
        Renderer circle = GetComponentInChildren<Renderer>();

        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) // permet de tout faire blink dans le foreach si besoin (sprite au sol...) à l'exception du mesh capsule
        {
            if (renderer.name == "Nivek") { smr = renderer; }
            if (renderer.name == "arme") { arme = renderer; }
            if (renderer.name == "Circle") { circle = renderer; }
        }

        while (!touchable)
        {
            smr.enabled = !smr.enabled;
            arme.enabled = !arme.enabled;
            circle.enabled = !circle.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        smr.enabled = true;
        arme.enabled = true;
        circle.enabled = true;
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

    public void DesactivateControllerSprite()
    {
        controllerSprite.gameObject.SetActive(false);
        arrows[0].gameObject.SetActive(false);
        arrows[1].gameObject.SetActive(false);
    }

	private void MoveToSpawn(){

        if (!hasBegunRunning)
        {
            if (GetComponentInChildren<Animator>())
            {
                GetComponentInChildren<Animator>().SetBool("isRunning", true);
                hasBegunRunning = true;
                hasArrived = false;
            }

            DesactivateControllerSprite();
        }


		if (transform.position != this.respawnPosition) {

			transform.position = Vector3.MoveTowards (transform.position, this.respawnPosition, Time.deltaTime * speed);
			transform.LookAt (this.respawnPosition);

		} else {
            if (!hasArrived)
            {
                if (GetComponentInChildren<Animator>())
                    GetComponentInChildren<Animator>().SetBool("isRunning", false);
                transform.LookAt(GameControllerF.GetMonster().transform.position);
                GameControllerF.getManager().validNextState(true);
                hasArrived = true;
            }
		}


	}

    void MoveControllerSelection(bool onRight)
    {

        List<int> positionsAvailable = new List<int>() {-2, -1, 0, 1, 2};

        int nextPosition = positionControllerSelection;

        for(int i = 1; i < 5; i++) {

            GameObject playerTested = GameControllerF.GetPlayer(i);
            int positionPlayerTested = playerTested.GetComponent<PlayerControllerF>().positionControllerSelection;

            if (playerTested.tag != this.tag && positionPlayerTested != 0)
            {
                if (positionsAvailable.Contains(positionPlayerTested))
                {
                    positionsAvailable.Remove(positionPlayerTested);
                }
            }

        }

        //foreach (int a in positionsAvailable)
        //{
        //    Debug.Log(a);
        //}

        if (onRight && positionControllerSelection != 2)
        {
            int minimalNewPos = 3;

            foreach (int positionA in positionsAvailable)
            {
                if (positionA > nextPosition && positionA < minimalNewPos)
                {
                    minimalNewPos = positionA;
                }
            }
            if (minimalNewPos < 3)
            {
                nextPosition = minimalNewPos;
            }
        }
        else if (!onRight && positionControllerSelection != -2)
        {
            int minimalNewPos = -3;

            foreach (int positionA in positionsAvailable)
            {
                if (positionA < nextPosition && positionA > minimalNewPos)
                {
                    minimalNewPos = positionA;
                }
            }
            if (minimalNewPos > -3)
            {
                nextPosition = minimalNewPos;
            }
        }

        if (nextPosition < 3 && nextPosition > -3)
        {

            positionControllerSelection = nextPosition;

            if (GameControllerF.GetPositionsAtSelection().ContainsKey(positionControllerSelection))
            {
                Vector3 newVectorPosition = GameControllerF.GetPositionsAtSelection()[positionControllerSelection];

                positionControllerSprite = new Vector3(newVectorPosition.x, positionYControllerSelection, newVectorPosition.z);
                controllerSprite.transform.position = positionControllerSprite;
            }
            else
            {
                Debug.Log("Bug on movement selection for player : " + gameObject.name + "with : " + positionControllerSelection);
            }


        }



    }

    void ValidInvalidSelection()
    {
        if (!isValidated && positionControllerSelection != 0)
        {
            bool canValidate = true;

            for (int i = 1; i < 5; i++) // check if other player on this position
            {

                GameObject playerTested = GameControllerF.GetPlayer(i);
                int positionPlayerTested = playerTested.GetComponent<PlayerControllerF>().positionControllerSelection;

                if (playerTested.tag != this.tag && positionPlayerTested == positionControllerSelection)
                {
                    canValidate = false;
                }

            }

            if (canValidate)
            {
                isValidated = true;

                string colorController = "blueController";
                if (GameControllerF.GetPlayerPositionsAtStart()[positionControllerSelection].team == GameControllerF.Team.Red)
                {
                    colorController = "redController";
                }
                controllerSprite.sprite = spritesController[System.Array.IndexOf(namesSpriteSheetController, colorController)];
                GameControllerF.getManager().validNextState(true);
            }

        }
        else if (isValidated && positionControllerSelection != 0)
        {
            isValidated = false;

            controllerSprite.sprite = spritesController[System.Array.IndexOf(namesSpriteSheetController, "whiteController")];
                
            GameControllerF.getManager().validNextState(false);
        }
    }

    public int GetPositionControllerSelection()
    {
        return positionControllerSelection;
    }

}