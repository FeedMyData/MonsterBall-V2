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
	
	// Update is called once per frame
	void Update () {
	    
	}

    //static void 
}
