using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollingCredits : MonoBehaviour {

    Text[] txtCredit;
    public float speedCredit = 20f;

	// Use this for initialization
	void Start () {
        txtCredit = GetComponentsInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < txtCredit.Length; i++)
        {
            txtCredit[i].transform.position += -Vector3.right*Time.deltaTime * speedCredit;

            if (txtCredit[i].transform.position.x < -3210)
            {
                txtCredit[i].transform.position = new Vector3(-txtCredit[i].transform.position.x, txtCredit[i].transform.position.y, txtCredit[i].transform.position.z);
            }
        }
	}
}
