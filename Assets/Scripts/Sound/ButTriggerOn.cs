using UnityEngine;
using System.Collections;

public class ButTriggerOn : AkTriggerBase {

    GoalScriptF goal;

	// Use this for initialization
	void Start () {
        goal = GetComponent<GoalScriptF>();
        goal.OnActivationEvent += Activate;
	}
	
	// Update is called once per frame
	void Activate() {
        triggerDelegate(goal.gameObject);
	}
}
