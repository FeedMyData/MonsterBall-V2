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
    public Text chooseYourPlayer;
    private static Text staticChooseYourPlayer;
	
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

    private static SoundManager sound;

    private static Dictionary<int, Vector3> positionsPlayersAtSelection = new Dictionary<int, Vector3>();
    private static Dictionary<int, PlayerControllerF> playerPositionsAtStart = new Dictionary<int, PlayerControllerF>();
	private static List<string>[] playerAwards;
    private static Vector3 positionXZNeutral = new Vector3(0.0f, 1.0f, 18.0f);
    private static float[] positionsY = new float[4] { 5.5f, 7.0f, 8.5f, 10.0f };
	private static string winner = "tie", finalScore = "0,0";

	// Use this for initialization
	void Awake () {
        tabObj = new GameObject[5];
        tabObj[0] = monster;
        tabObj[1] = player1;
        tabObj[2] = player2;
        tabObj[3] = player3;
        tabObj[4] = player4;

        positionsPlayersAtSelection.Clear();
        positionsPlayersAtSelection.Add(-2, player4.transform.position);
        positionsPlayersAtSelection.Add(-1, player2.transform.position);
        positionsPlayersAtSelection.Add(0, positionXZNeutral);
        positionsPlayersAtSelection.Add(1, player1.transform.position);
        positionsPlayersAtSelection.Add(2, player3.transform.position);

        playerPositionsAtStart.Clear();
        playerPositionsAtStart.Add(-2, player4.GetComponent<PlayerControllerF>());
        playerPositionsAtStart.Add(-1, player2.GetComponent<PlayerControllerF>());
        playerPositionsAtStart.Add(1, player1.GetComponent<PlayerControllerF>());
        playerPositionsAtStart.Add(2, player3.GetComponent<PlayerControllerF>());

        staticField = field;
        //staticTxtScore = txtScore;
        staticBlueScoreTxt = blueScoreTxt;
        staticRedScoreTxt = redScoreTxt;
        staticTxtDuration = txtDuration;
        staticChooseYourPlayer = chooseYourPlayer;
        staticBluGoal = bluGoal;
        staticRedGoal = redGoal;
        staticPosBluGoal = posBluGoal;
        staticPosRedGoal = posRedGoal;

        staticCake = cake;
        sound = GetComponent<SoundManager>();

        manager = GetComponent<GameManagerF>();

		playerAwards = new  List<string>[4];

	}

    public static SoundManager GetSound()
    {
        return sound;
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

       
        for (int i = 0; i < nearObj.Count ; i++)
        {
            for (int j = nearObj.Count - 1; j > i; j++)
            {
                if (Distance(obj, nearObj[j]) < Distance(obj, nearObj[j - 1]))
                {
                    GameObject tempNear = nearObj[j];
                    nearObj[j] = nearObj[j - 1];
                    nearObj[j - 1] = tempNear;
                }
            }
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
            if (tabObj[i].GetComponent<PlayerControllerF>().IsEatable())
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

    public static float InCircle(Vector3 position)
    {
        Vector3 fs = FieldSize();
        Vector3 objVec = position;

        return ((Mathf.Pow(objVec.x, 2) / Mathf.Pow(fs.x / 2, 2)) + (Mathf.Pow(objVec.z, 2) / Mathf.Pow(fs.z / 2, 2)));
    }

    //public static Text GetTxtScore()
    //{
    //    return staticTxtScore;
    //}

    public static GameObject GetTargetCharge()
    {
        int blueScore = manager.GetBlueScore();
        int redScore = manager.GetRedScore();

        GameObject targetPlayer;
        List<GameObject> playersEven = new List<GameObject>();
        int coupsDonnesMax = 0;

        foreach(GameObject player in tabObj) {

            PlayerControllerF playerScript = player.GetComponent<PlayerControllerF>();

            if (playerScript != null)
            {
                Team team = playerScript.team;

                if (blueScore > redScore && team == GameControllerF.Team.Blu) {
                    if (playerScript.coupsDonnesSurBalle > coupsDonnesMax)
                    {
                        coupsDonnesMax = playerScript.coupsDonnesSurBalle;
                        playersEven.Clear();
                        playersEven.Add(player);
                    }
                    else if (playerScript.coupsDonnesSurBalle == coupsDonnesMax)
                    {
                        playersEven.Add(player);
                    }
                }
                else if (blueScore < redScore && team == GameControllerF.Team.Red)
                {
                    if (playerScript.coupsDonnesSurBalle > coupsDonnesMax)
                    {
                        coupsDonnesMax = playerScript.coupsDonnesSurBalle;
                        playersEven.Clear();
                        playersEven.Add(player);
                    }
                    else if (playerScript.coupsDonnesSurBalle == coupsDonnesMax)
                    {
                        playersEven.Add(player);
                    }
                }
                else if (blueScore == redScore)
                {
                    if (playerScript.coupsDonnesSurBalle > coupsDonnesMax)
                    {
                        coupsDonnesMax = playerScript.coupsDonnesSurBalle;
                        playersEven.Clear();
                        playersEven.Add(player);
                    }
                    else if (playerScript.coupsDonnesSurBalle == coupsDonnesMax)
                    {
                        playersEven.Add(player);
                    }
                }
            }
        }

        targetPlayer = playersEven[Random.Range(0, playersEven.Count)];
        if (targetPlayer == null)
        {
            targetPlayer = GetPlayer(Random.Range(1, 5));
        }

        return targetPlayer;

    }

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

    public static Text GetChooseYourPlayer()
    {
        return staticChooseYourPlayer;
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

    public static Dictionary<int, Vector3> GetPositionsAtSelection()
    {
        return positionsPlayersAtSelection;
    }

    public static Dictionary<int, PlayerControllerF> GetPlayerPositionsAtStart()
    {
        return playerPositionsAtStart;
    }

	public static List<string>[] GetPlayerAwards(){
		if(playerAwards == null) playerAwards = new  List<string>[4];
		return playerAwards;
	}

    public static Vector3 GetPositionXZNeutral()
    {
        return positionXZNeutral;
    }

    public static float[] GetPositionsY()
    {
        return positionsY;
    }

    public static GameObject GetCake()
    {
        return staticCake;
    }
	public static void SetWinner(string winnerTeam){

		winner = winnerTeam;

	}
	public static string GetWinner(){
		return winner;
	}

	public static void SetFinalScore(int blueScore, int redScore){

		finalScore = blueScore.ToString () + "," + redScore.ToString ();

	}

	public static string GetFinalScore(){

		return finalScore;

	}

}

