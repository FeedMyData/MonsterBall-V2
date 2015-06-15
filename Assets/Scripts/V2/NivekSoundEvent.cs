using UnityEngine;
using System.Collections;

public class NivekSoundEvent : MonoBehaviour {

	// Use this for initialization
	public void PlayStep(){

		gameObject.GetComponentInParent<SoundManager> ().PlayEvent ("SFX_Niveks_Footsteps", gameObject);
	}
}
