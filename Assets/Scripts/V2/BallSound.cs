using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class BallSound : AbstractSound {

    public AudioClip[] tabBut;
    private AudioSource sourceBut;

    public AudioClip[] tabCourse;
    private AudioSource sourceCourse;

    public AudioClip[] tabImpact;
    private AudioSource sourceImpact;

    public AudioClip[] tabObjet;
    private AudioSource sourceObjet;

    public AudioClip[] tabRemiseEnJeu;
    private AudioSource sourceRemiseEnJeu;

    public AudioClip[] tabTransformationBalleMonstre;
    private AudioSource sourceTransformationBalleMonstre;

    public AudioClip[] tabTransformationMonstreBall;
    private AudioSource sourceTransformationMonstreBall;

    public AudioMixerGroup mixerVoice;

	// Use this for initialization
	void Start () {
        sourceBut = gameObject.AddComponent<AudioSource>();
        sourceBut.outputAudioMixerGroup = mixerVoice;

        sourceCourse = gameObject.AddComponent<AudioSource>();
        sourceCourse.outputAudioMixerGroup = mixerVoice;

        sourceImpact = gameObject.AddComponent<AudioSource>();
        sourceImpact.outputAudioMixerGroup = mixerVoice;

        sourceObjet = gameObject.AddComponent<AudioSource>();
        sourceObjet.outputAudioMixerGroup = mixerVoice;

        sourceRemiseEnJeu = gameObject.AddComponent<AudioSource>();
        sourceRemiseEnJeu.outputAudioMixerGroup = mixerVoice;

        sourceTransformationBalleMonstre = gameObject.AddComponent<AudioSource>();
        sourceTransformationBalleMonstre.outputAudioMixerGroup = mixerVoice;

        sourceTransformationMonstreBall = gameObject.AddComponent<AudioSource>();
        sourceTransformationMonstreBall.outputAudioMixerGroup = mixerVoice;
	}

    public void PlayRandomSound(Action action)
    {

        switch (action){
            case AbstractSound.Action.But:
                sourceBut.clip = tabBut[Random.Range(0, tabBut.Length)];
                sourceBut.Play();
                break;
            case AbstractSound.Action.Course:
                sourceCourse.clip = tabCourse[Random.Range(0, tabCourse.Length)];
                sourceCourse.Play();
                break;
            case AbstractSound.Action.Impact:
                sourceImpact.clip = tabImpact[Random.Range(0, tabImpact.Length)];
                sourceImpact.Play();
                break;
            case AbstractSound.Action.Objet:
                sourceObjet.clip = tabObjet[Random.Range(0, tabObjet.Length)];
                sourceObjet.Play();
                break;
            case AbstractSound.Action.RemiseEnJeu:
                sourceRemiseEnJeu.clip = tabRemiseEnJeu[Random.Range(0, tabRemiseEnJeu.Length)];
                sourceRemiseEnJeu.Play();
                break;
            case AbstractSound.Action.TransformationBalleMonstre:
                sourceTransformationBalleMonstre.clip = tabTransformationBalleMonstre[Random.Range(0, tabTransformationBalleMonstre.Length)];
                sourceTransformationBalleMonstre.Play();
                break;
            case AbstractSound.Action.TransformationMonstreBall:
                sourceTransformationMonstreBall.clip = tabTransformationMonstreBall[Random.Range(0, tabTransformationMonstreBall.Length)];
                sourceTransformationMonstreBall.Play();
                break;

            default:
                Debug.LogError("L'action "+action+"n'existe pas dans ce contexte.");
                break;
        }
    }
}
