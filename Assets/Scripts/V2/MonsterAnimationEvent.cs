using UnityEngine;
using System.Collections;

public class MonsterAnimationEvent : MonoBehaviour {

	public void StartCharge(){

		transform.parent.GetComponent<MonsterControllerF> ().StartCharge ();

		// Feedback_Charge

		foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
		{
			if (ps.name == "Fumee_charge") ps.Play();
		}

	}

}
