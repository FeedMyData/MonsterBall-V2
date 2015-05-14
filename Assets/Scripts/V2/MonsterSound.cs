using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class MonsterSound : AbstractSound {

    //public int soundSize = 0;
    public AudioClip[] tabHit;
    private AudioSource sourceHit;

    public AudioMixerGroup mixerVoice;

    

	// Use this for initialization
	void Start () {
        sourceHit = gameObject.AddComponent<AudioSource>();
        sourceHit.outputAudioMixerGroup = mixerVoice;
	}

    public void PlayRandomSound(Action action)
    {
        if (action == Action.Impact)
        {
            sourceHit.clip = tabHit[Random.Range(0, tabHit.Length)];
            sourceHit.Play();
        }
    }
}
