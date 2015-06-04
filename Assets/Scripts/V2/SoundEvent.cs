using UnityEngine;
using System.Collections;

public class SoundEvent : MonoBehaviour {

    private SoundManager sound;

    public void PlayRun()
    {
        sound = GameControllerF.GetSound();
        sound.LoadBank();
        sound.PlayEvent("SFX_Niveks_Footsteps", gameObject);
    }

    /*public void PlayWoosh()
    {
        sound = GameControllerF.GetSound();
        sound.LoadBank();
        sound.PlayEvent("SFX_Niveks_Woosh", gameObject);
    }*/
}
