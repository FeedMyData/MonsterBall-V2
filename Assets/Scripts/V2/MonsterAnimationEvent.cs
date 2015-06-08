using UnityEngine;
using System.Collections;

public class MonsterAnimationEvent : MonoBehaviour {

	public void StartCharge(){

		transform.parent.GetComponent<MonsterControllerF> ().StartCharge ();

	}

}
