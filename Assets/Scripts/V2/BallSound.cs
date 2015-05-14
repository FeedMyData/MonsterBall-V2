using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class BallSound : AbstractSound {

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

    public void PlayRandomSound(Action action)
    {
        if (action == Action.Hit)
        {
            sourceHit.clip = tabHit[Random.Range(0, tabHit.Length)];
            sourceHit.Play();
        }
    }
}
