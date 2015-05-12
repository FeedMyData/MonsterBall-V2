using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour {

    private Vector3 initPosition;
    private bool reset;
    public float latenceFollow = 4.0f;
    public float speed = 12.0f;
    public GameObject monster;

    private Camera cam;
	// Use this for initialization
	void Start ()
	{
        cam = GetComponent<Camera>();
        initPosition = transform.position;
	}
	// Update is called once per frame
	void Update ()
	{
        zoom();
        //recherche le joueur le plus éloigné
        

        //Deplacement lateral
		
	}

    void LateralMove()
    {
        if (!reset)
        {
            if ((transform.position.x - latenceFollow) > monster.transform.position.x)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(monster.transform.position.x + latenceFollow, transform.position.y, transform.position.z), speed * Time.deltaTime);
            else if ((transform.position.x + latenceFollow) < monster.transform.position.x)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(monster.transform.position.x - latenceFollow, transform.position.y, transform.position.z), speed * Time.deltaTime);

            if (((transform.position.z + Mathf.Abs(initPosition.z)) - latenceFollow) > monster.transform.position.z)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, (monster.transform.position.z - Mathf.Abs(initPosition.z)) + latenceFollow), speed * Time.deltaTime);
            else if (((transform.position.z + Mathf.Abs(initPosition.z)) + latenceFollow) < monster.transform.position.z)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, (monster.transform.position.z - Mathf.Abs(initPosition.z)) - latenceFollow), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initPosition, speed * Time.deltaTime);
            if (monster.transform.position.z < latenceFollow || Mathf.Abs(monster.transform.position.x) > latenceFollow)
                reset = false;
        }
    }

    void zoom()
    {
        //remplacer les vector3.zero par monster.transform.position
        float adjacent = Vector3.Distance(Vector3.zero,this.transform.position);
        float oppose = Vector3.Distance(GameControllerF.WhereIsTheFarthest(monster),Vector3.zero);

        float Juliette = Mathf.Rad2Deg*Mathf.Tan(oppose/adjacent);
        if (Juliette > 30 && Juliette < 100)
            cam.fieldOfView = Juliette+20;
    }

    public void SetMoveMode(bool reset)
    {
        this.reset = reset;
    }


}