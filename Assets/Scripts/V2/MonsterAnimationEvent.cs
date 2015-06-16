using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MonsterAnimationEvent : MonoBehaviour {

    [Header("Vibration")]
    public float durationVibr = 0.3f;
    [Range(0f, 1f)]
    public float powerVibr = 0.4f;

	public void StartCharge(){

        MonsterControllerF monsterScript = transform.parent.GetComponent<MonsterControllerF>();
		monsterScript.StartCharge ();
		
        // Feedback_Charge
		foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
		{
            if (ps.name == "Fumee_charge")
            {
                ps.gameObject.SetActive(false);
                ps.gameObject.SetActive(true);
                ps.Play();
            }
			if (ps.name == "Wind")ps.Play();
		}

	}

    public void ShakeCamera()
    {
        MonsterControllerF monsterScript = transform.parent.GetComponent<MonsterControllerF>();
        monsterScript.ShakeWhenAnimationPreCharge();
    }

    IEnumerator Vibration()
    {
        GamePad.SetVibration((PlayerIndex)0, powerVibr, powerVibr);
        GamePad.SetVibration((PlayerIndex)1, powerVibr, powerVibr);
        GamePad.SetVibration((PlayerIndex)2, powerVibr, powerVibr);
        GamePad.SetVibration((PlayerIndex)3, powerVibr, powerVibr);
        yield return new WaitForSeconds(durationVibr);
        GamePad.SetVibration((PlayerIndex)0, powerVibr, powerVibr);
        GamePad.SetVibration((PlayerIndex)1, powerVibr, powerVibr);
        GamePad.SetVibration((PlayerIndex)2, powerVibr, powerVibr);
        GamePad.SetVibration((PlayerIndex)3, powerVibr, powerVibr);
    }

}
