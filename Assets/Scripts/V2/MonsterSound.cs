using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class MonsterSound : AbstractSound {

    public AudioClip[] tabGrognement;
    private AudioSource sourceGrognement;

    public AudioClip[] tabImpact;
    private AudioSource sourceImpact;

    public AudioClip[] tabRecracheJoueur;
    private AudioSource sourceRecracheJoueur;

    public AudioMixerGroup mixerVoice;

	// Use this for initialization
	void Start () {
        sourceGrognement = gameObject.AddComponent<AudioSource>();
        sourceGrognement.outputAudioMixerGroup = mixerVoice;

        sourceImpact = gameObject.AddComponent<AudioSource>();
        sourceImpact.outputAudioMixerGroup = mixerVoice;

        sourceRecracheJoueur = gameObject.AddComponent<AudioSource>();
        sourceRecracheJoueur.outputAudioMixerGroup = mixerVoice;
	}

    public void PlayRandomSound(Action action)
    {
        switch (action)
        {
            case AbstractSound.Action.Grognement:
                sourceGrognement.clip = tabGrognement[Random.Range(0, tabGrognement.Length)];
                sourceGrognement.Play();
                break;
            case AbstractSound.Action.RecracheJoueur:
                sourceRecracheJoueur.clip = tabRecracheJoueur[Random.Range(0, tabRecracheJoueur.Length)];
                sourceRecracheJoueur.Play();
                break;
            case AbstractSound.Action.Impact:
                sourceImpact.clip = tabImpact[Random.Range(0, tabImpact.Length)];
                sourceImpact.Play();
                break;

            default:
                Debug.LogError("L'action " + action + "n'existe pas dans ce contexte.");
                break;
        }
        
    }
    //static void 
}
