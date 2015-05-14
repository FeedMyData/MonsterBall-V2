using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class NivekSound : AbstractSound {

    public AudioClip[] tabCoupBalle;
    private AudioSource sourceCoupBalle;

    public AudioClip[] tabCoupRecu;
    private AudioSource sourceCoupRecu;

    public AudioClip[] tabEjectBut;
    private AudioSource sourceEjectBut;

    public AudioClip[] tabMarqueBut;
    private AudioSource sourceMarqueBut;

    public AudioClip[] tabPoursuivi;
    private AudioSource sourcePoursuivi;

    public AudioClip[] tabVictoire;
    private AudioSource sourceVictoire;

    public AudioClip[] tabWilhemScream;
    private AudioSource sourceWilhemScream;

    public AudioClip[] tabYeah;
    private AudioSource sourceYeah;

    public AudioMixerGroup mixerVoice;

    // Use this for initialization
    void Start()
    {
        sourceCoupBalle = gameObject.AddComponent<AudioSource>();
        sourceCoupBalle.outputAudioMixerGroup = mixerVoice;

        sourceCoupRecu = gameObject.AddComponent<AudioSource>();
        sourceCoupRecu.outputAudioMixerGroup = mixerVoice;

        sourceEjectBut = gameObject.AddComponent<AudioSource>();
        sourceEjectBut.outputAudioMixerGroup = mixerVoice;

        sourceMarqueBut = gameObject.AddComponent<AudioSource>();
        sourceMarqueBut.outputAudioMixerGroup = mixerVoice;

        sourcePoursuivi = gameObject.AddComponent<AudioSource>();
        sourcePoursuivi.outputAudioMixerGroup = mixerVoice;

        sourceVictoire = gameObject.AddComponent<AudioSource>();
        sourceVictoire.outputAudioMixerGroup = mixerVoice;

        sourceWilhemScream = gameObject.AddComponent<AudioSource>();
        sourceWilhemScream.outputAudioMixerGroup = mixerVoice;

        sourceYeah = gameObject.AddComponent<AudioSource>();
        sourceYeah.outputAudioMixerGroup = mixerVoice;
    }

    public void PlayRandomSound(Action action)
    {
        switch (action)
        {
            case AbstractSound.Action.CoupBalle:
                sourceCoupBalle.clip = tabCoupBalle[Random.Range(0, tabCoupBalle.Length)];
                sourceCoupBalle.Play();
                break;
            case AbstractSound.Action.CoupRecu:
                sourceCoupRecu.clip = tabCoupRecu[Random.Range(0, tabCoupRecu.Length)];
                sourceCoupRecu.Play();
                break;
            case AbstractSound.Action.EjectBut:
                sourceEjectBut.clip = tabEjectBut[Random.Range(0, tabEjectBut.Length)];
                sourceEjectBut.Play();
                break;
            case AbstractSound.Action.MarqueBut:
                sourceMarqueBut.clip = tabMarqueBut[Random.Range(0, tabMarqueBut.Length)];
                sourceMarqueBut.Play();
                break;
            case AbstractSound.Action.Poursuivi:
                sourcePoursuivi.clip = tabPoursuivi[Random.Range(0, tabPoursuivi.Length)];
                sourcePoursuivi.Play();
                break;

            case AbstractSound.Action.Victoire:
                sourceVictoire.clip = tabVictoire[Random.Range(0, tabVictoire.Length)];
                sourceVictoire.Play();
                break;

            case AbstractSound.Action.WilhemScream:
                sourceWilhemScream.clip = tabWilhemScream[Random.Range(0, tabWilhemScream.Length)];
                sourceWilhemScream.Play();
                break;

            case AbstractSound.Action.Yeah:
                sourceYeah.clip = tabYeah[Random.Range(0, tabYeah.Length)];
                sourceYeah.Play();
                break;

            default:
                Debug.LogError("L'action " + action + "n'existe pas dans ce contexte.");
                break;
        }

    }
}
