using UnityEngine;
using System.Collections;

public class NivekSoundEvent : MonoBehaviour {

	// Use this for initialization
	public void PlayStep(){

		gameObject.GetComponentInParent<SoundManager> ().PlayEvent ("SFX_Niveks_Footsteps", gameObject);
	}



	public void PlayWin(){
		
		gameObject.GetComponentInParent<SoundManager> ().PlayEvent ("SFX_Niveks_Footsteps", gameObject);
	}
	public void PlayWinAlternative(){
		
		gameObject.GetComponentInParent<SoundManager> ().PlayEvent ("SFX_Niveks_Footsteps", gameObject);
	}
	public void PlayLose(){
		
		gameObject.GetComponentInParent<SoundManager> ().PlayEvent ("SFX_Niveks_Footsteps", gameObject);
	}
	public void PlayLoseAlternative(){
		
		gameObject.GetComponentInParent<SoundManager> ().PlayEvent ("SFX_Niveks_Footsteps", gameObject);
	}
}
