using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MonsterControllerF : MonoBehaviour {

    private Rigidbody body;

    [Header("Move")]
    //public float movingMaxBall = 3.0f;
    public float speedBall = 12f;
    public float speedRotationBall = 10f;
    public float speedMonster = 12.0f;
    //private bool restMoving = false;
    public float angleChangeDirection = 45f;
    public float speedMaxToChooseDirection = 5.0f;
    public float speedDivisionFactor = 8.0f;
    public float spaceBetweenBallPlayer = 2.0f;
    public float fovBall = 110f;
    public float angleAvoidPlayer = 45f;
    private Vector3 baseScaleBall = new Vector3(0.4f, 0.4f, 0.4f);
    public float maxScaleFactor = 2.0f;
    private Transform childBall;
    
    [Header("Magnet")]
    public float areaMagnet = 1.0f;
    public float speedMagnet = 10.0f;
    private Collider colliderMagnet;
    private GameObject magnet;
    private GameObject previousMagnet;

    [Header("Wrath")]
    public int wrathMax = 100;
    public int wrathDribblingValue = 1;
    public float wrathDribblingEachTime = 1.0f;
    public float summon = 0.3f;
    public float durationFirstPart = 15.0f;
    public float durationSecondPart = 5.0f;
    private int wrath = 0;
    private bool monsterForm = false;
    //private bool monsterModeCharge = false;
    public float monsterScale = 3.0f;
    public float monsterMass = 20.0f;
    [Range(0.0f,1.0f)]
    public float safeTransform = 0.6f;
    private bool transforming = false;
    private float timeTransforming;
    public int nbCycleMonster = 3;

    [Header("Charge_Phase2")]
    public float durationLoadingCharge = 2.0f;
    //public float durationUpSpeedCharge = 8.0f;
    public float numberOfReboundsToMaxSpeed = 10.0f;
    public int maxNumberOfChargeRebounds = 10;
    public float minSpeedCharge = 8.0f;
    public float maxSpeedCharge = 25.0f;
    private float speedMonsterCharge;
    //private float timeCharge;
    private bool monsterModeCharge = false;
    private int currentNumberOfRebounds = 0;
	private int totalNumberOfRebounds = 0;
    [Header("Respawn")]
    public float durationInvul = 2.0f;
    private bool touchable = true;
    public float durationMiniRandomRespawn = 1.0f;
    public float durationMaxiRandomRespawn = 5.0f;
    public float speedRotationRespawn = 20f;
    public float respawnDistanceToCenter = 5.0f;
    private bool chooseNewRespawn = false;
    private Vector3 newRespawn = Vector3.zero;
    public Light lightRespawn;

    [Space(20)]
    public float coefColliderMonster = 1.05f;
    [Space(20)]
    public float durationEatingPlayer = 2.0f;
    private bool eatPlayer = false;
	private int currentNumberOfChargeEatenPlayers = 0;
    private float rotateToGoal = 0.0f;
    private Transform goal;
    private Vector3 DirectionMonster = Vector3.zero;

    private GameObject playerAte;

    [Header("Skin")]
    public GameObject skinBall;
    public GameObject skinMonster;

    [Header("Spotlights")]
    public GameObject ballSpotlight;
    public GameObject monsterSpotlight;

    [Header("Ambiant light")]
    public Light ambiantLight;
    public float intensityWhenBall = 1.2f;
    public float intensityWhenMonster = 0.8f;

    private bool seeACake = false;
    private Vector3 cakePos;

    private SoundManager sound;
    [Header("Sound")]
    public float distanceSoundChase = 5.0f;
    [Range(0, 100)]
    public float rngSoundChase = 1.0f;
    public float timeBetweenTwoChaseSound = 5.0f;

    private bool canYell;

    private PlayerControllerF striker;

    private TeleportationF tp;

    private TextCommentaries commentariesScript;
    private bool loadingChargeEnd = false;
    private GameObject targetCharge;
    private bool moveCharge = false;
    private bool canEat = true;
    
    public Vector3 respawnPositionBall;

    [HideInInspector]
    public bool canCount= true;

    private ParticleSystem smokeFury;
    private ParticleSystem transfoFury;
    private ParticleSystem saliveDroite;
    private ParticleSystem saliveGauche;
    private ParticleSystem ragingFx;
    //public delegate void OnClickHit();
    //public event OnClickHit OnClickHitEvent;


	// Use this for initialization
	void Start () {
	    body = GetComponent<Rigidbody>();
        tp = GetComponentInChildren<TeleportationF>();
        colliderMagnet = GetComponent<Collider>();
        sound = GetComponent<SoundManager>();
        sound.LoadBank();

        childBall = transform.Find("ball_monster");
        baseScaleBall = childBall.localScale;

        commentariesScript = GameObject.Find("Commentaries").GetComponent<TextCommentaries>();

        smokeFury = GameObject.Find("FumeeTransfo").GetComponent<ParticleSystem>();
        transfoFury = GameObject.Find("Transformation_particules").GetComponent<ParticleSystem>();
        saliveDroite = GameObject.Find("Salive droite").GetComponent<ParticleSystem>();
        saliveGauche = GameObject.Find("Salive gauche").GetComponent<ParticleSystem>();
        ragingFx = GameObject.Find("raging fx").GetComponent<ParticleSystem>();

        if (!monsterForm)
        {
            ballSpotlight.SetActive(true);
            monsterSpotlight.SetActive(false);
            ambiantLight.intensity = intensityWhenBall;
        }
        else
        {
            ballSpotlight.SetActive(false);
            monsterSpotlight.SetActive(true);
            ambiantLight.intensity = intensityWhenMonster;
        }


        //tp.SetTeleportation(true);
	}
	
	// Update is called once per frame
	void Update() {

        if (chooseNewRespawn)
        {
            Debug.DrawRay(lightRespawn.transform.position, transform.forward);
            lightRespawn.transform.RotateAround(Vector3.zero, Vector3.up, speedRotationRespawn * Time.deltaTime);
        }

		if (GameControllerF.getManager ().state != GameManagerF.Step.inGame) return;

        if (transform.position.y < -3.0f && !monsterForm && !transforming) RespawnBall();

        if (
            (monsterForm || transforming) && ( 
            transform.position.y > 8.0f || //26
            transform.position.y < -2.0f ||
            transform.position.z > 21.0f ||
            transform.position.z < -21.0f ||
            transform.position.x > 44.0f || //39
            transform.position.x < -44.0f)
            )
        {
            SafeRespawn();
            Debug.Log("saferespawn by position");
        }

        if (magnet == null)
        {
            if (!monsterForm)
            {
                if (seeACake)
                {
                    EatACake();
                }
                else
                {
                    
                    MoveBall();

                    if (touchable)
                    {
                        SetMagnet();
                    }
                }
            }
            else
            {
                MoveMonster();
                //if un joueur est à portée
                //prendre le plus proche
                GameObject proxiPlayer = GameControllerF.NearestTo(this.gameObject, this.transform.localScale.x * coefColliderMonster);
                if (proxiPlayer != null)
                {

                    if (proxiPlayer.GetComponent<PlayerControllerF>().IsTouchable() && !eatPlayer)
                    {
                        playerAte = proxiPlayer;

                        if (canEat)
                        {
                            StartCoroutine(EatingPlayer(proxiPlayer));

                            rotateToGoal = Time.time + durationEatingPlayer;
                        }
                    }
                }
            }
                
        }
        else
        {
            if (seeACake)
            {
                callDisableMagnet();
                EatACake();
            }
            else
            {
                MagnetManager();
            }
        }

        if (!transforming && !monsterForm && wrath >= wrathMax && GameControllerF.InCircle(this.gameObject)<safeTransform)
        {
            StartCoroutine(NotHappy());
        }
	}

    void MoveBall()
    {
        //choisis aléatoirement un vector et s'y déplace avec une impulsion. Plus il va loin plus il attends pour choisir une nouvelle position

        /*Vector3 positionReach = UnityEngine.Random.insideUnitSphere * movingMaxBall;
        positionReach.y = Mathf.Abs(positionReach.y) * 3;

        
        body.AddForce(positionReach,ForceMode.Impulse);


        StartCoroutine(RestMoveBall(Vector3.SqrMagnitude(positionReach)/speedDivisionFactor));*/

        if (transforming)
        {
            //float delayTransforming = Mathf.Abs(((timeTransforming - Time.time) / (summon / 2)) - 1);
            if (timeTransforming < summon / 2)
            {
                timeTransforming += Time.deltaTime;
                float percentageL = timeTransforming / (summon / 2);
                //percentageL = Mathf.Sin(percentageL * Mathf.PI * 0.5f);
                body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, percentageL);

                body.angularVelocity = Vector3.Lerp(body.angularVelocity, Vector3.zero, percentageL);

                ambiantLight.intensity = Mathf.Lerp(intensityWhenBall, intensityWhenMonster, percentageL);
            }

        }
        else
        {
            if (canCount)
            {
                if (GetActualSpeed() < speedMaxToChooseDirection && isGround())
                {
                    body.velocity = Vector3.zero;
                    body.angularVelocity = Vector3.zero;
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                    childBall.localScale = baseScaleBall;

                    Vector3 newPosition = transform.forward;
                    //newPosition.y = transform.localScale.y/2;

                    transform.position += newPosition * Time.deltaTime * speedBall;


                    if (GameControllerF.InCircle(gameObject) > 0.80f)
                    {
                        float newDirection = transform.eulerAngles.y + 180;

                        newDirection += GetAngleBounce(transform.position);

                        transform.eulerAngles = new Vector3(0, newDirection, 0);
                    }
                    else
                    {
                        //regarde s'il y a un joueur devant lui et se décale pour l'éviter
                        try
                        {
                            if (GameControllerF.FieldOfView(gameObject, 20, 90)[0] != null)
                            {
                                Vector3 relativePoint = transform.InverseTransformPoint(GameControllerF.FieldOfView(gameObject, 20, 90)[0].transform.position);
                                if (relativePoint.x < 0)
                                    transform.RotateAround(transform.position, Vector3.up, angleAvoidPlayer * Time.deltaTime);
                                else
                                    transform.RotateAround(transform.position, Vector3.up, -angleAvoidPlayer * Time.deltaTime);
                            }
                        }
                        catch (Exception e) { }
                    }
                }
                else // balle volle, sans volonté propre
                {
                    // scale de la balle
                    transform.forward = body.velocity;
                    float percentageL = (body.velocity.magnitude - 40.0f) / (80.0f - 40.0f);
                    float newScaleBalleX = Mathf.Lerp(baseScaleBall.x, baseScaleBall.x / (maxScaleFactor / 2), percentageL);
                    float newScaleBalleZ = Mathf.Lerp(baseScaleBall.z, baseScaleBall.z / (maxScaleFactor / 2), percentageL);
                    float newScaleBalleY = Mathf.Lerp(baseScaleBall.y, baseScaleBall.y * maxScaleFactor, percentageL);
                    childBall.localScale = new Vector3(newScaleBalleX, newScaleBalleY, newScaleBalleZ);
                }
            }
        }
    }

    public IEnumerator WrathWhileDribbling()
    {
        while (magnet != null)
        {
            yield return new WaitForSeconds(wrathDribblingEachTime);
            wrath += wrathDribblingValue;
            ragingFx.Play();
        }
        
    }

    /*IEnumerator RestMoveBall(float duration)
    {
        restMoving = true;
        yield return new WaitForSeconds(duration);
        restMoving = false;
    }*/

    bool isGround()
    {
        return Physics.Raycast(transform.position,Vector3.down,transform.localScale.y/1.5f);
    }

    void MoveMonster()
    {
        Vector3 rotationToGoal = Vector3.zero;
        body.velocity = Vector3.zero;

        if(eatPlayer)
        {
            //Rotation progressive vers le but
            //Debug.Log(Mathf.Abs(((rotateToGoal - Time.time)/ durationEatingPlayer)-1)+ " "+DirectionMonster+" "+goal.position);
            //DirectionMonster = Vector3.Lerp(DirectionMonster, goal.position, Mathf.Abs(((rotateToGoal - Time.time) / durationEatingPlayer) - 1));
            //DirectionMonster = Camera.main.transform.position - transform.position; // avant : goal.position
			DirectionMonster = goal.position;
			rotationToGoal = Vector3.RotateTowards(transform.forward, DirectionMonster, Mathf.PI * Mathf.Abs(((rotateToGoal - Time.time) / durationEatingPlayer) - 1),0.0f);
           // Debug.Log(Mathf.Lerp(0,1, Mathf.Abs(((rotateToGoal - Time.time) / durationEatingPlayer) - 1)));

            //positionReach = goal.position;
        }
        else if(seeACake)
        {
            DirectionMonster = cakePos;
        }
        else if (monsterModeCharge)
        {

            if (!moveCharge)
            {
                //Debug.Log("vise");
                canEat = false;
                //s'il se déplace pas, il vise un joueur
                DirectionMonster = targetCharge.transform.position;// -transform.position;
                //Vector3 rotationToTarget = Vector3.RotateTowards(transform.forward, targetDir, 100 * Time.deltaTime, 0.0f);
                //transform.rotation = Quaternion.LookRotation(rotationToTarget);

                if (!loadingChargeEnd)
                {
                    loadingChargeEnd = true;
					if(GetComponentInChildren<Animator>()) GetComponentInChildren<Animator>().SetTrigger("chargeAnnouncement");
                   // StartCoroutine(LoadingCharge());
                }
            }
            else
            {
                body.angularVelocity = Vector3.zero;
                //Debug.Log("charge");
                //avance tout droit et change de direction dans les coins
                if (GameControllerF.InCircle(gameObject) > 0.50f)
                {
                    float newDirection = transform.eulerAngles.y + 180;
                    newDirection += GetAngleBounce(transform.position);
                    transform.eulerAngles = new Vector3(0, newDirection, 0);
                    if (currentNumberOfRebounds < numberOfReboundsToMaxSpeed) currentNumberOfRebounds++;
                    //feedbacks rebond sur bord quand charge
                    Camera.main.GetComponent<CameraShake>().shake(0.8f, 2.0f, 1.0f);
					totalNumberOfRebounds++;
                }

                //float delayCharge = Mathf.Abs((timeCharge - Time.time) / durationUpSpeedCharge - 1);
                float percentageLSpeed = currentNumberOfRebounds / numberOfReboundsToMaxSpeed;
                speedMonsterCharge = Mathf.Lerp(minSpeedCharge, maxSpeedCharge, percentageLSpeed);

				if (totalNumberOfRebounds >= maxNumberOfChargeRebounds  && currentNumberOfChargeEatenPlayers >= 3 )
       			{ 
          		 	monsterModeCharge = false;
           			loadingChargeEnd = false;
           			moveCharge = false;
            		TransformationMonstreBall();
        		}

            }
            
        }
        else
        {
            try
            {
                GameObject nearest = GameControllerF.NearestTouchableByMonster();
                DirectionMonster = nearest.transform.position;

                if (Vector3.Distance(nearest.transform.position, transform.position) < distanceSoundChase && canYell)
                {
                    float rng = UnityEngine.Random.value;

                    if (rng <= rngSoundChase/100){
                        sound.PlayEvent("VX_Monstre_Course", gameObject);
                        canYell = false;
                        StartCoroutine(Yelling());
                    }

                    else if (rng > rngSoundChase / 100 && rng <= (rngSoundChase / 100 * 2))
                    {
                        nearest.GetComponent<SoundManager>().PlayEvent("VX_Niveks_Poursuivi", nearest);
                        canYell = false;
                        StartCoroutine(Yelling());
                    }
                        
                }

            }
            catch (NullReferenceException e) { /*Si pas de plus proche on fait rien, dans le pire des cas à dure 3 secondes*/ }
        }


        //transform.Translate(transform.forward*Time.deltaTime/*speedMonster*/); //Marche pas
        if(eatPlayer)
        {
            transform.rotation = Quaternion.LookRotation(rotationToGoal);
        }
        else if(monsterModeCharge)
        {
            //Debug.Log("charge");
            if (!moveCharge)
                transform.LookAt(DirectionMonster, Vector3.up);
            else
                transform.position += transform.forward * Time.deltaTime * speedMonsterCharge;
        }
        else
        {
            transform.LookAt(DirectionMonster, Vector3.up);
            transform.position += transform.forward * Time.deltaTime * speedMonster;
        }
            
    }

    private IEnumerator Yelling(){
        yield return new WaitForSeconds(timeBetweenTwoChaseSound);
        canYell = true;
    }

    public void RespawnBall()
    {
        GetComponentInChildren<TeleportationF>().InstantTP(true);
        canCount = false;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        transform.eulerAngles = new Vector3(0,UnityEngine.Random.Range(0,360),0);

        ballSpotlight.SetActive(false);
        monsterSpotlight.SetActive(false);
        ragingFx.gameObject.SetActive(false);

        transform.position = respawnPositionBall;
        body.useGravity = false;

        StartCoroutine(WaitRespawn());
        //Camera cam = Camera.allCameras[0];
        //if(cam.GetComponent<CameraManagerF>() != null)
        //{
        //    cam.GetComponent<CameraManagerF>().Respawn();
        //}

        
    }

    IEnumerator WaitRespawn()
    {
        chooseNewRespawn = true;
        lightRespawn.transform.LookAt(new Vector3(0,0,respawnDistanceToCenter));
        lightRespawn.gameObject.SetActive(true);
        yield return new WaitForSeconds(UnityEngine.Random.Range(durationMiniRandomRespawn,durationMaxiRandomRespawn));
        chooseNewRespawn = false;
        RaycastHit hit;
        Physics.Raycast(lightRespawn.transform.position,lightRespawn.transform.forward,out hit);
        

        Debug.Log(hit.transform.name);

        lightRespawn.gameObject.SetActive(false);
        sound.PlayEvent("VX_Balle_RemiseEnJeu", gameObject);
        transform.position = new Vector3(hit.point.x, 5, hit.point.z);
        body.useGravity = true;
        canCount = true;
        if (monsterForm)
        {
            ballSpotlight.SetActive(false);
            monsterSpotlight.SetActive(true);
        }
        else
        {
            ballSpotlight.SetActive(true);
            monsterSpotlight.SetActive(false);
        }
        
        ragingFx.gameObject.SetActive(true);
        GetComponentInChildren<TeleportationF>().SetTeleportation(false);
        StartCoroutine(Intouchable());

    }

    public void SafeRespawn()
    {
        transform.position = respawnPositionBall;
    }

    /*IEnumerator LoadingCharge()
    {
        yield return new WaitForSeconds(durationLoadingCharge);
        //regarde la cible
        //timeCharge = Time.time + durationUpSpeedCharge;
        Camera.main.GetComponent<CameraShake>().shake(0.8f, 3.0f, 1.5f);
        canEat = true;
        moveCharge = true;
    }*/
	public void StartCharge(){
		Camera.main.GetComponent<CameraShake>().shake(0.8f, 3.0f, 1.5f);
		canEat = true;
		moveCharge = true;
	}
	
	void TransformationBallMonstre()
    {
        // feedbacks pré-transformation
        Camera.main.GetComponent<CameraShake>().shake(2.0f, 0.15f, 0.01f);
        commentariesScript.WriteCommentary("both", "monsterP");

        transforming = true;
        childBall.localScale = baseScaleBall;
        timeTransforming = 0;
        //taille + magnet + variable
        wrath = 0;
        if (magnet != null)
            callDisableMagnet();

        
        sound.PlayEvent("Transfo_BalleMonstre", gameObject);
        sound.PlayEvent("Music_Monstre", gameObject);
        striker = null;
    }
    
    void TransformationMonstreBall()
    {
        sound.PlayEvent("Transfo_MonstreBalle", gameObject);
        sound.StopEvent("Music_Monstre", gameObject, 1000);
        skinBall.SetActive(true);
        skinMonster.SetActive(false);
        wrath = 0;

        if (eatPlayer)
        {
            Vector3 expulsePos = UnityEngine.Random.insideUnitSphere;
            expulsePos.y = Mathf.Abs(expulsePos.y);
            playerAte.GetComponent<PlayerControllerF>().AddImpact(expulsePos * 100);
            playerAte = null;
        }

        transform.localScale /= monsterScale;

        monsterForm = false;

        //feedbacks fin monstre
        ballSpotlight.GetComponent<ballSpotlight>().position();
        ballSpotlight.SetActive(true);
        monsterSpotlight.SetActive(false);
        ambiantLight.intensity = intensityWhenBall;

        commentariesScript.WriteCommentary("both", "ballP");
    }

    IEnumerator NotHappy()
    {

        TransformationBallMonstre();

        transfoFury.Play();

        yield return new WaitForSeconds(summon / 2);

        smokeFury.transform.position = transform.position;
        smokeFury.Play();

        yield return new WaitForSeconds(summon/2);

        transforming = false;

        //feedbacks post-transformation
        Camera.main.GetComponent<CameraShake>().shake(0.8f, 3.0f, 1.5f);

        monsterSpotlight.GetComponent<ballSpotlight>().position();
        ballSpotlight.SetActive(false);
        monsterSpotlight.SetActive(true);
        //ambiantLight.intensity = intensityWhenMonster;

        monsterForm = true;

        skinBall.SetActive(false);
        skinMonster.SetActive(true);
        transform.localScale *= monsterScale;

        transfoFury.Stop();
        smokeFury.Stop();


        //int actualCycleMonster = 0;

       // while (actualCycleMonster<nbCycleMonster)
        //{
        yield return new WaitForSeconds(durationFirstPart);
            if (!monsterModeCharge)
            {
                targetCharge = GameControllerF.GetTargetCharge();
                Debug.Log(targetCharge.name);
                //actualCycleMonster++;
                monsterModeCharge = true;
                currentNumberOfRebounds = 0;
			totalNumberOfRebounds = 0;
				currentNumberOfChargeEatenPlayers = 0;
            }
       // }

    }

    void MagnetManager()
    {
        colliderMagnet.enabled = false;
        body.useGravity = false;

        Vector3 reposition = magnet.transform.position;
        reposition.x += magnet.transform.forward.x * spaceBetweenBallPlayer;
        reposition.y = magnet.transform.position.y - magnet.transform.localScale.y/3f;
        reposition.z += magnet.transform.forward.z * spaceBetweenBallPlayer;
        transform.position = reposition;

        if (magnet.GetComponent<PlayerControllerF>().isRunning)
        {
            body.AddTorque(magnet.transform.forward * 10,ForceMode.Acceleration);
        }
        else
        {
            body.angularVelocity = Vector3.zero;
        }
    }

    void SetMagnet()
    {
        GameObject potentialMagnet = GameControllerF.NearestTo(this.gameObject, areaMagnet);
        if (GetActualSpeed() <= speedMagnet && potentialMagnet!=null && !transforming)
        {
            //Debug.Log(potentialMagnet);
            if (potentialMagnet != previousMagnet && potentialMagnet.GetComponent<PlayerControllerF>().getBonus() == null)
            {
                magnet = potentialMagnet;
                StartCoroutine(WrathWhileDribbling());
                magnet.GetComponent<PlayerControllerF>().SetMagnet(true);

                body.velocity = Vector3.zero;

                childBall.localScale = baseScaleBall;

            }        
        }
    }

    public GameObject GetMagnet()
    {
        return magnet;
    }

    public void callDisableMagnet()
    {
        colliderMagnet.enabled = true;
        body.useGravity = true;
        StopCoroutine(WrathWhileDribbling());

        if (magnet != null)
        {
            previousMagnet = magnet;
            magnet.GetComponent<PlayerControllerF>().SetMagnet(false);
            
        }

        magnet = null;

        StartCoroutine(DisableMagnet());
    }

    

    IEnumerator EatingPlayer(GameObject player)
    {

        eatPlayer = true;
        player.GetComponent<PlayerControllerF>().isEaten = true;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<Collider>().enabled = false;
        player.GetComponent<PlayerControllerF>().joueurMange++;
        //faire disparaitre le joueur, jouer l'anim du monstre qui mache et téléporter le joueur dans le monstre et le stun
        player.transform.position = this.transform.position;
        //player.GetComponent<Renderer>().enabled = false;
        //player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        foreach (Renderer renderer in player.GetComponentsInChildren<Renderer>()) {
            if (renderer.name == "arme" || renderer.name == "Nivek" || renderer.name == "Circle") { renderer.enabled = false; }
        }
        player.GetComponent<PlayerControllerF>().callStun(durationEatingPlayer);


        if (player.GetComponent<PlayerControllerF>().team == GameControllerF.Team.Blu)
            goal = GameControllerF.GetPosBluGoal();
        else
            goal = GameControllerF.GetPosRedGoal();

		if (!monsterModeCharge) {

			if (GetComponentInChildren<Animator> ())
				GetComponentInChildren<Animator> ().SetTrigger ("spit");

			//feedbacks chewing
			saliveDroite.Play ();
			saliveGauche.Play ();

			yield return new WaitForSeconds (durationEatingPlayer);

			saliveDroite.Stop ();
			saliveGauche.Stop ();

			//Faire réapparaitre le joueur
			//player.GetComponent<Renderer>().enabled = true;
			//player.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		}
		else currentNumberOfChargeEatenPlayers++;
        foreach (Renderer renderer in player.GetComponentsInChildren<Renderer>())
        {
            if (renderer.name == "arme" || renderer.name == "Nivek" || renderer.name == "Circle") { renderer.enabled = true; }
        }

        if (player == playerAte)
        {
            sound.PlayEvent("VX_Monstre_Crache", gameObject);
            sound.PlayEvent("VX_Niveks_Wilhem", player);
            player.GetComponent<PlayerControllerF>().FlyAway();

        }

        yield return new WaitForSeconds(0.1f);

       /* if (monsterModeCharge)
        {
            monsterModeCharge = false;
            loadingChargeEnd = false;
            moveCharge = false;
            TransformationMonstreBall();
        }*/

        eatPlayer = false;
        player.GetComponent<PlayerControllerF>().isEaten = false;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<Collider>().enabled = true;
    }

    IEnumerator DisableMagnet()
    {
        yield return new WaitForSeconds(1f);
        previousMagnet = null;
    }

    public void AddWrath(int wrath)
    {
        this.wrath += wrath;
        ragingFx.Play();
    }

    /**
     * La balle s'arrête et fait un bond
     * 
     * @param   height  Hauteur du saut
     * 
     */
    public void Jump(float height)
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        Vector3 jump = Vector3.zero;
        jump.y = height;

        body.AddForce(jump,ForceMode.Impulse);
    }

    public bool IsMonsterForm()
    {
        return monsterForm;
    }

    public bool IsTransforming()
    {
        return transforming;
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
        if (!monsterForm)
        {
            MeshRenderer[] mr = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < mr.Length; i++)
            {
                if (mr[i].name == "ball_monster")
                {
                    while (!touchable)
                    {
                        mr[i].enabled = !mr[i].enabled;
                        yield return new WaitForSeconds(0.1f);
                    }
                    mr[i].enabled = true;
                }
            }
        }
    }

    float GetAngleBounce(Vector3 position)
    {
        float angle = Vector3.Angle(transform.forward, position - Vector3.zero);

        Vector3 perp = Vector3.Cross(transform.forward, position - Vector3.zero);
        float dir = Vector3.Dot(perp, Vector3.up);

        if (dir >= 0f)
        {
            angle *= 1;
        }
        else if (dir < 0f)
        {
            angle *= -1;
        }

        return angle;
    }

    public bool IsTouchable()
    {
        return touchable;
    }

    public float GetActualSpeed()
    {
        return Vector3.Magnitude(body.velocity);
    }

    public void ChargeOnCake(Vector3 position)
    {
        seeACake = true;
        cakePos = position;
    }

    public void EatACake()
    {
        body.AddForce(cakePos-transform.position, ForceMode.Acceleration);
    }

    public void GoodCake()
    {
        seeACake = false;
    }

    public int GetWrath()
    {
        return wrath;
    }

    public void SetStriker(PlayerControllerF striker)
    {
        this.striker = striker;
    }

    public PlayerControllerF GetStriker()
    {
        return striker;
    }

    void OnCollisionEnter(Collision other)
    {

        if (monsterForm && transforming && other.gameObject.transform.parent.name == "stadium 21")
        {
            Vector3 averageContactPoint = Vector3.zero;
            Vector3 averageContactNormal = Vector3.zero;

            foreach (ContactPoint contact in other.contacts)
            {
                averageContactPoint += contact.point;
                averageContactNormal += contact.normal;
            }

            averageContactPoint = averageContactPoint / other.contacts.Length;
            averageContactNormal = averageContactNormal / other.contacts.Length;
            averageContactNormal.Normalize();

            if (
                    (averageContactPoint.x > 0 && averageContactNormal.x > 0) ||
                    (averageContactPoint.x < 0 && averageContactNormal.x < 0) ||
                    (averageContactPoint.z > 0 && averageContactNormal.z > 0) ||
                    (averageContactPoint.z < 0 && averageContactNormal.z < 0)
                )
            {
                SafeRespawn();
                Debug.Log("saferespawn by collision");
            }
        }
    }
}
