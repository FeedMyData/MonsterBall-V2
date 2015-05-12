using UnityEngine;
using System.Collections;

public class MonsterControllerV2 : MonoBehaviour
{

    private int wrath;

    private GameObject magnet = null;

    public GameController controller;
    private Rigidbody body;
    private Collider collider;

    private bool bigMonster = false;

    private bool isMagnetic = true;

    public float speed = 5f;

    private GameObject target;

    private GameObject[] listPlayer;

    public int jaugeColere = 100;

    public float dureeTransformation = 15f;
    private float revocation;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        listPlayer = new GameObject[4];
        listPlayer[0] = GameObject.Find("Player 1");
        listPlayer[1] = GameObject.Find("Player 2");
        listPlayer[2] = GameObject.Find("Player 3");
        listPlayer[3] = GameObject.Find("Player 4");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -2)
        {
            //TODO: Relancer comme pour un but, noter la sortie de terrain
            Respawn(new Vector3(0, 10, 0), new Vector3(0, 0, 0));
        }

        if (wrath >= jaugeColere)
        {
            body.mass = 3;
            //TODO: Tranformation
            collider.isTrigger = false;
            if (!bigMonster)
            {
                transform.Translate(transform.position.x, 5, transform.rotation.z);
                transform.localScale = new Vector3(3, 3, 3);
                isMagnetic = false;

                revocation = dureeTransformation + Time.time;
                
                bigMonster = true;
            }

            float distanceBetweenMNP = float.PositiveInfinity;
            for (int i = 0; i < listPlayer.Length; i++)
            {
                float distanceTemp = Vector3.Distance(transform.position, listPlayer[i].transform.position);

                if (distanceTemp < distanceBetweenMNP)
                {
                    target = listPlayer[i];
                    distanceBetweenMNP = distanceTemp;
                }
                    
            }
            
            body.AddForce((target.transform.position-transform.position)*speed);

            if(distanceBetweenMNP < 2.5f)
            {
                //Projeter le joueur
                target.GetComponent<PlayerController>().Expulse();

            }

            Debug.Log(revocation + " " + Time.time);

            if (revocation < Time.time)
            {
                Debug.Log("lol");
                body.mass = 1;
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                isMagnetic = true;
                bigMonster = false;

                wrath = 0;

                //TODO: afficher le temps restant
            }
        }
        else
        {
            if (magnet != null)
            {
                Vector3 glue = magnet.transform.position;
                //Debug.Log(magnet.transform.forward.ToString());
                glue.x += magnet.transform.forward.x;
                glue.z += magnet.transform.forward.z;
                glue.y = transform.localScale.y / 2;
                transform.position = glue;
            }
            else
            {
                collider.isTrigger = false;
            }
        }

        /*if (impact.sqrMagnitude > 0.2f)
            transform.Translate(impact * Time.deltaTime);
        impact = Vector3.Lerp(impact, Vector3.zero, 20* Time.deltaTime);*/
    }

    /*void AddImpact(Vector3 direction, float power){
        direction.Normalize();
        impact += direction * power / mass;
    }*/

    public bool SetMagnet(GameObject player = null)
    {
        if (magnet == null && isMagnetic)
        {
            magnet = player;
            collider.isTrigger = true;
            return true;
        }
        if (player == null)
        {
            collider.isTrigger = false;
            magnet = null;
        }


        return false;
    }

    IEnumerator DisableMagnet()
    {
        isMagnetic = false;
        yield return new WaitForSeconds(0.1f);
        isMagnetic = true;
    }

    public void HitByPlayer(Vector3 directionForce, float powerOfAttack)
    {
        if (!bigMonster)
        {
            SetMagnet();
            powerOfAttack /= 3;
            directionForce.y = 0f;

            directionForce.Normalize();
            directionForce.y = 0.2f;
            wrath += 10;
            controller.SetWrathMonster(wrath);

            transform.rotation = Quaternion.identity;
            body.angularVelocity = Vector3.zero;
            StartCoroutine(DisableMagnet());

            body.AddForce((directionForce) * powerOfAttack, ForceMode.VelocityChange);
        }
    }

    public void Respawn(Vector3 newPosition, Vector3 newDirection)
    {
        transform.position = newPosition;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.AddForce(newDirection, ForceMode.Impulse);
    }

    public bool isBigMonster()
    {
        return bigMonster;
    }
}
