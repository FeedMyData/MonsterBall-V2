﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MonsterControllerF : MonoBehaviour {

    private Rigidbody body;

    [Header("Move")]
    public float movingMaxBall = 3.0f;
    public float speedMonster = 12.0f;
    private bool restMoving = false;
    public float speedMaxToChooseDirection = 5.0f;
    public float speedDivisionFactor = 8.0f;
    
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
    public float revocation = 20.0f;
    private int wrath = 0;
    private bool monsterForm = false;
    public float monsterScale = 3.0f;
    public float monsterMass = 20.0f;
    [Range(0.0f,1.0f)]
    public float safeTransform = 0.7f;

    [Header("Respawn")]
    public float durationInvul = 2.0f;
    private bool touchable = true;

    [Header("Sound")]
    private BallSound ballSound;
    private MonsterSound monsterSound;

    [Space(20)]
    public float coefColliderMonster = 1.05f;
    [Space(20)]
    public float durationEatingPlayer = 2.0f;
    private bool eatPlayer = false;
    private float rotateToGoal = 0.0f;
    private Transform goal;
    private Vector3 DirectionMonster = Vector3.zero;


    private bool seeACake = false;
    private Vector3 cakePos;


	// Use this for initialization
	void Start () {
	    body = GetComponent<Rigidbody>();
        colliderMagnet = GetComponent<Collider>();
        monsterSound = GetComponent<MonsterSound>();
        ballSound = GetComponent<BallSound>();
	}
	
	// Update is called once per frame
	void Update() {
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
                    if (!restMoving && GetActualSpeed() < speedMaxToChooseDirection && isGround())
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
                        Debug.Log(proxiPlayer.name+" "+proxiPlayer.GetComponent<PlayerControllerF>().IsTouchable());
                        StartCoroutine(EatingPlayer(proxiPlayer));

                        rotateToGoal = Time.time + durationEatingPlayer;
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

        if (wrath >= wrathMax && GameControllerF.InCircle(this.gameObject)<safeTransform)
        {
            StartCoroutine(NotHappy());
        }
	}

    void MoveBall()
    {
        //choisis aléatoirement un vector et s'y déplace avec une impulsion. Plus il va loin plus il attends pour choisir une nouvelle position

        Vector3 positionReach = UnityEngine.Random.insideUnitSphere * movingMaxBall;
        positionReach.y = Mathf.Abs(positionReach.y) * 3;

        
        body.AddForce(positionReach,ForceMode.Impulse);

        if (UnityEngine.Random.value < 0.1f)
            PlayRandomSound(AbstractSound.Action.Course);

        StartCoroutine(RestMoveBall(Vector3.SqrMagnitude(positionReach)/speedDivisionFactor));

    }

    public IEnumerator WrathWhileDribbling()
    {
        while (magnet != null)
        {
            yield return new WaitForSeconds(wrathDribblingEachTime);
            wrath += wrathDribblingValue;
        }
        
    }

    IEnumerator RestMoveBall(float duration)
    {
        restMoving = true;
        yield return new WaitForSeconds(duration);
        restMoving = false;
    }

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
            DirectionMonster = goal.position - transform.position;

            rotationToGoal = Vector3.RotateTowards(transform.forward, DirectionMonster, Mathf.PI * Mathf.Abs(((rotateToGoal - Time.time) / durationEatingPlayer) - 1),0.0f);
           // Debug.Log(Mathf.Lerp(0,1, Mathf.Abs(((rotateToGoal - Time.time) / durationEatingPlayer) - 1)));

            //positionReach = goal.position;
        }
        else if(seeACake)
        {
            DirectionMonster = cakePos;
        }
        else
        {
            try
            {
                GameObject nearest = GameControllerF.NearestTouchableByMonster();
                DirectionMonster = nearest.transform.position;
            }
            catch (NullReferenceException e) { /*Si pas de plus proche on fait rien, dans le pire des cas à dure 3 secondes*/ }
        }

        

        Debug.DrawLine(transform.position, DirectionMonster);

        //transform.Translate(transform.forward*Time.deltaTime/*speedMonster*/); //Marche pas
        if (!eatPlayer)
        {
            transform.LookAt(DirectionMonster, Vector3.up);
            transform.position += transform.forward * Time.deltaTime * speedMonster;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(rotationToGoal);
        }
            
    }

    public void Respawn()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0,5,0);

        PlayRandomSound(AbstractSound.Action.RemiseEnJeu);

        Camera cam = Camera.allCameras[0];
        if(cam.GetComponent<CameraManagerF>() != null)
        {
            cam.GetComponent<CameraManagerF>().Respawn();
        }

        StartCoroutine(Intouchable());
    }

    IEnumerator NotHappy()
    {
        //taille + magnet + variable
        wrath = 0;
        if (magnet != null)
            callDisableMagnet();

        yield return new WaitForSeconds(summon);
        monsterForm = true;
        transform.localScale *= monsterScale;
        PlayRandomSound(AbstractSound.Action.TransformationBalleMonstre);

        yield return new WaitForSeconds(revocation);
        wrath = 0;
        PlayRandomSound(AbstractSound.Action.TransformationMonstreBall);

        transform.localScale /= monsterScale;

        monsterForm = false;

    }

    void MagnetManager()
    {
        colliderMagnet.enabled = false;
        body.useGravity = false;

        Vector3 reposition = magnet.transform.position;
        reposition.x += magnet.transform.forward.x;
        reposition.y = magnet.transform.position.y - transform.localScale.y;
        reposition.z += magnet.transform.forward.z;
        transform.position = reposition;
    }

    void SetMagnet()
    {
        GameObject potentialMagnet = GameControllerF.NearestTo(this.gameObject, areaMagnet);
        if (GetActualSpeed() <= speedMagnet && potentialMagnet!=null)
        {
            if (potentialMagnet != previousMagnet && potentialMagnet.GetComponent<PlayerControllerF>().getBonus() == null)
            {
                magnet = potentialMagnet;
                StartCoroutine(WrathWhileDribbling());
                magnet.GetComponent<PlayerControllerF>().SetMagnet(true);

                body.velocity = Vector3.zero;
            }        
        }
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
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<Collider>().enabled = false;
        //faire disparaitre le joueur, jouer l'anim du monstre qui mache et téléporter le joueur dans le monstre et le stun
        player.transform.position = this.transform.position;
        player.GetComponent<Renderer>().enabled = false;
        player.GetComponent<PlayerControllerF>().callStun(durationEatingPlayer);

        Debug.Log(player.GetComponent<PlayerControllerF>().team);

        if (player.GetComponent<PlayerControllerF>().team == GameControllerF.Team.Blu)
            goal = GameControllerF.GetPosBluGoal();
        else
            goal = GameControllerF.GetPosRedGoal();

        Debug.Log(goal.position);

        yield return new WaitForSeconds(durationEatingPlayer);
        
        //Faire réapparaitre le joueur
        player.GetComponent<Renderer>().enabled = true;
        player.GetComponent<PlayerControllerF>().FlyAway();
        
        PlayRandomSound(AbstractSound.Action.RecracheJoueur);

        yield return new WaitForSeconds(0.1f);
        eatPlayer = false;
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

    public void PlayRandomSound(AbstractSound.Action action)
    {
        if (!monsterForm)
        {
            ballSound.PlayRandomSound(action);
        }
        else
        {
            monsterSound.PlayRandomSound(action);
        }
    }

    public int GetWrath()
    {
        return wrath;
    }
}
