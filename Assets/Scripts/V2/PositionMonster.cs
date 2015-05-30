using UnityEngine;
using System.Collections;

public class PositionMonster : MonoBehaviour {


    private GameObject monster;
	// Use this for initialization
	void Start () {
        monster = GameControllerF.GetMonster();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = monster.transform.position;
	}
}
