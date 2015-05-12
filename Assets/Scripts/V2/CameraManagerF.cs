using UnityEngine;
using System.Collections;

public class CameraManagerF : MonoBehaviour {

    private Vector3 initPos;
    public float speed = 12.0f;
    public float fovMin = 50.0f;
    public float fovMax = 90.0f;
    public float latenceFollow = 4.0f;
    public float overFollow = 4.0f;

    private Camera mainCam;
    private GameObject monster;

    private float adjacent;
    private float alpha;
    private float oppose;

    private bool respawn = false;

	// Use this for initialization
	void Start () {
        initPos = transform.position;
        mainCam = GetComponent<Camera>();
        monster = GameControllerF.GetMonster();

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);

        adjacent = Vector3.Distance(hit.point, transform.position);
        alpha = Mathf.Tan(Mathf.Deg2Rad * mainCam.fieldOfView / 2);
        oppose = alpha * adjacent;


        
	}
	
	// Update is called once per frame
	void Update () {


       // Debug.Log(((monster.transform.position.x + oppose) + " " + ((GameControllerF.FieldSize().x) / 2 + overFollow)));

        //
        Debug.Log((transform.position.x + latenceFollow)+" "+monster.transform.position.x);


        if ((transform.position.x - latenceFollow) > monster.transform.position.x)
        {
            if (monster.transform.position.x - oppose > (GameControllerF.FieldSize().x * -1) / 2 - overFollow)//si la position.x de la caméra + jusqu'ou elle voit.x > la distancemax du terrain.x/2 + latenceMax
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(monster.transform.position.x + latenceFollow, transform.position.y, transform.position.z), speed * Time.deltaTime);
            
        }
        else if ((transform.position.x + latenceFollow) < monster.transform.position.x)
        {
            if (monster.transform.position.x + oppose < (GameControllerF.FieldSize().x) / 2 + overFollow)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(monster.transform.position.x - latenceFollow, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }

        if (((transform.position.z + Mathf.Abs(initPos.z)) - latenceFollow) > monster.transform.position.z)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, (monster.transform.position.z - Mathf.Abs(initPos.z)) + latenceFollow), speed * Time.deltaTime);
        else if (((transform.position.z + Mathf.Abs(initPos.z)) + latenceFollow) < monster.transform.position.z)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, (monster.transform.position.z - Mathf.Abs(initPos.z)) - latenceFollow), speed * Time.deltaTime);
        
        
        //on cherche opposé, on a l'adjacent et l'angle
        //float oppose = //Mathd

        //float Juliette = Mathf.Rad2Deg * Mathf.Tan(oppose / adjacent);
        //if (Juliette > 30 && Juliette < 100)
       //     cam.fieldOfView = Juliette + 20;
        

        //mainCam.fieldOfView = Mathf.Lerp(fovMin,fovMax,monster.GetComponent<MonsterControllerF>().GetActualSpeed()/10);

        if (respawn)
            Respawn();
	}

    public void Respawn()
    {
        respawn = true;
        transform.position = Vector3.MoveTowards(transform.position, initPos, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position,initPos) < 0.1f)
        {
            respawn = false;
        }
    }
}
