using UnityEngine;
using System.Collections;

public class MonsterAnimationEvent : MonoBehaviour {

	public void StartCharge(){

        MonsterControllerF monsterScript = transform.parent.GetComponent<MonsterControllerF>();
		monsterScript.StartCharge ();
		
        // Feedback_Charge
		foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
		{
			if (ps.name == "Fumee_charge") ps.Play();
            if (ps.name == "Wind") ps.Play();
		}

	}

    public void ShakeCamera()
    {
        MonsterControllerF monsterScript = transform.parent.GetComponent<MonsterControllerF>();
        monsterScript.ShakeWhenAnimationPreCharge();
    }

}
