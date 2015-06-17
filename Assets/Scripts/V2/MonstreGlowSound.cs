using UnityEngine;
using System.Collections;

public class MonstreGlowSound : MonoBehaviour {

    private SoundManager sound;

    void Start()
    {
        sound = GetComponent<SoundManager>();
        sound.LoadBank();
    }

    public void PlayGlow()
    {
        sound.PlayEvent("SFX_Monstre_Grow", gameObject);
    }
}
