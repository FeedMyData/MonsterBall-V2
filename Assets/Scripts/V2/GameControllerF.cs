using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameControllerF : MonoBehaviour {

    public enum Jersey
    {
        player1,
        player2,
        player3,
        player4
    };
    public enum Team
    {
        Blu,
        Red
    };

    [Header("Elements")]
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject monster;
    [Space(20)]
    public GameObject field;
    public GameObject bluGoal;
    public Transform posBluGoal;
    public GameObject redGoal;
    public Transform posRedGoal;

    [Header("UI")]
    //public Text txtScore;
    //private static Text staticTxtScore;
    public Text blueScoreTxt;
    private static Text staticBlueScoreTxt;
    public Text redScoreTxt;
    private static Text staticRedScoreTxt;
    public Text txtDuration;
    private static Text staticTxtDuration;

    [Header("Bonus")]
    public GameObject cake;
    private static GameObject staticCake;


    private static GameManagerF manager;

    private static GameObject[] tabObj;
    private static GameObject staticField;
    private static GameObject staticBluGoal;
    private static GameObject staticRedGoal;

    private static Transform staticPosBluGoal;
    private static Transform staticPosRedGoal;


	// Use this for initialization
	void Awake () {
        tabObj = new GameObject[5];
        tabObj[0] = monster;
        tabObj[1] = player1;
        tabObj[2] = player2;
        tabObj[3] = player3;
        tabObj[4] = player4;

        staticField = field;
        //staticTxtScore = txtScore;
        staticBlueScoreTxt = blueScoreTxt;
        staticRedScoreTxt = redScoreTxt;
        staticTxtDuration = txtDuration;
        staticBluGoal = bluGoal;
        staticRedGoal = redGoal;
        staticPosBluGoal = posBluGoal;
        staticPosRedGoal = posRedGoal;

        staticCake = cake;

        manager = GetComponent<GameManagerF>();
	}

    public static GameManagerF getManager()
    {
        return manager;
    }

    public static Vector3 FieldSize()
    {
        MeshFilter mf = staticField.GetComponent<MeshFilter>();

        Mesh mesh = mf.sharedMesh;

        Vector3 size = mesh.bounds.size;
        Vector3 scale = staticField.transform.localScale;
        Vector3 scaleParent = staticField.transform.parent.localScale;

        return new Vector3(size.x * scale.x * scaleParent.x, size.z * scale.y * scaleParent.y, size.y * scale.z * scaleParent.z);
    }

    /**
     * Calcul la distance entre deux gameObjects
     * 
     * @return      la distance entre les deux gameObjects
     */
    public static float Distance(GameObject obj1, GameObject obj2)
    {
        return Vector3.Distance(obj1.transform.position,obj2.transform.position);
    }

    /**
     * Permet d'obtenir une liste des gameObject à proximité
     * 
     * @param   obj         le gameObject dont on veut connaitre les objects à proximité
     * @param   distance    la distance maximum de proximité de l'obj
     * @return              un tableau avec tous les object dans la distance à obj est inférieur à distance
     */
    public static List<GameObject> NearTo(GameObject obj, float distance)
    {
        List<GameObject> nearObj = new List<GameObject>();
        for (int i = 0; i < tabObj.Length; i++)
        {
            if(obj != tabObj[i])
                if(Distance(obj,tabObj[i])<distance)
                    nearObj.Add(tabObj[i]);
        }
        return nearObj;
    }

    /**
     * Permet d'obtenir la liste des objets vus
     * 
     * @param   obj         le gameObject dont on veut connaitre les objects à proximité
     * @param   distance    la distance maximum de proximité de l'obj
     * @param   angle       L'angle de recherche des objets à proximité
     * @return      La liste des objets dans le champ de vision de obj
     */
    public static List<GameObject> PlayerView(PlayerControllerF obj, float distance, float angle)
    {
        List<GameObject> nearObj = new List<GameObject>();
        for (int i = 0; i < tabObj.Length; i++)
        {
            if (obj.gameObject != tabObj[i])
                if (Distance(obj.gameObject, tabObj[i]) < distance && Vector3.Angle(obj.GetDirectionNormalize(),tabObj[i].transform.position-obj.transform.position) <= angle/2)
                {
                    nearObj.Add(tabObj[i]);
                }
        }
        return nearObj;
    }

    public static List<GameObject> FieldOfView(GameObject obj, float distance, float angle)
    {
        List<GameObject> nearObj = new List<GameObject>();
        for (int i = 0; i < tabObj.Length; i++)
        {
            if (obj != tabObj[i])
                if (Distance(obj.gameObject, tabObj[i]) < distance && Vector3.Angle(obj.transform.forward, tabObj[i].transform.position - obj.transform.position) <= angle / 2)
                {
                    nearObj.Add(tabObj[i]);
                }
        }

        for (int i = 0; i < nearObj.Count; i++)
        {
            //tri du plus proche au plus loin ?
        }

        return nearObj;
    }

    /**
     * Regarde si le joueur à un allié dans sa zone d'auto-guidage
     * 
     * @return  la position de l'allié le plus proche dans la zone d'autoguidage
     * 
     */
    public static Vector3 WhereIsMyAlly(PlayerControllerF obj)
    {
        Vector3 posAlly = Vector3.zero;
        float distanceAlly = float.PositiveInfinity;

        for (int i = 0; i < tabObj.Length; i++)
        {
            if (obj.tag == tabObj[i].tag && obj != tabObj[i])
            {
                if(Vector3.Angle(obj.GetDirectionNormalize(),tabObj[i].transform.position - obj.transform.position) <= obj.angleHoming/2)
                {
                    float rainbowDash = Distance(obj.gameObject, tabObj[i]);
                    if ( rainbowDash < distanceAlly)
                    {
                       distanceAlly = rainbowDash;
                       posAlly = tabObj[i].transform.position;
                    }
                }
            }
        }

        return posAlly;
    }

    /**
     * Cherche l'objet le plus loin de l'objet passé en paramètre
     * 
     * @param   obj L'objet de référence
     * @return  La position de l'élément le plus loin d'obj
     */
    public static Vector3 WhereIsTheFarthest(GameObject obj)
    {
        Vector3 posFarthest = Vector3.zero;
        float farthest = 0.0f;

        for (int i = 0; i < tabObj.Length; i++)
        {
            float taMere = Distance(tabObj[i],obj);
            if (taMere > farthest)
            {
                farthest = taMere;
                posFarthest = tabObj[i].transform.position;
            }
        }

        return posFarthest;
    }

    public static GameObject NearestTo(GameObject obj, float distance)
    {
        float KatyPerry = float.PositiveInfinity;
        GameObject nearest = null;

        for (int i = 0; i < tabObj.Length; i++)
        {
            KatyPerry = Distance(tabObj[i], obj);
            if (KatyPerry < distance && tabObj[i] != obj)
            {
                distance = KatyPerry;
                nearest = tabObj[i];
            }
        }

        return nearest;
    }

    public static GameObject NearestTouchableByMonster()
    {
        float distance = float.PositiveInfinity;
        float KatyPerry = 0;
        GameObject nearest = null;

        for (int i = 1; i < tabObj.Length; i++)
        {
            KatyPerry = Distance(tabObj[i], tabObj[0]);
            if (tabObj[i].GetComponent<PlayerControllerF>().IsTouchable())
            {
                if (KatyPerry < distance)
                {
                    distance = KatyPerry;
                    nearest = tabObj[i];
                }
            }
        }

        return nearest;
    }

    public static GameObject GetMonster()
    {
        return tabObj[0];
    }

    public static float InCircle(GameObject obj)
    {
        Vector3 fs = FieldSize();
        Vector3 objVec = obj.transform.position;

        return ((Mathf.Pow(objVec.x, 2) / Mathf.Pow(fs.x/2, 2)) + (Mathf.Pow(objVec.z, 2) / Mathf.Pow(fs.z/2, 2)));
    }

    //public static Text GetTxtScore()
    //{
    //    return staticTxtScore;
    //}

    public static Text GetBlueScoreTxt()
    {
        return staticBlueScoreTxt;
    }

    public static Text GetRedScoreTxt()
    {
        return staticRedScoreTxt;
    }

    public static Text GetTxtDuration()
    {
        return staticTxtDuration;
    }

    public static GameObject GetBluGoal()
    {
        return staticBluGoal;
    }

    public static GameObject GetRedGoal()
    {
        return staticRedGoal;
    }

    public static Transform GetPosBluGoal()
    {
        return staticPosBluGoal;
    }

    public static Transform GetPosRedGoal()
    {
        return staticPosRedGoal;
    }

    public static GameObject GetPlayer(int i)
    {
        return tabObj[i];
    }

    public static GameObject GetCake()
    {
        return staticCake;
    }
}
