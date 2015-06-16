using UnityEngine;
using System.Collections;

public class MonstreGlowSound : MonoBehaviour {

    public SoundManager sound;

    public void PlayGlow()
    {
        sound.PlayEvent("SFX_Monstre_Grow", gameObject);
    }
}
