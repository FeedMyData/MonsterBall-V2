using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class CommentarySound : AbstractSound
{

    public AudioClip[] tabTroisDeuxUn;
    private AudioSource sourceTroisDeuxUn;

    public AudioClip[] tabBut;
    private AudioSource sourceBut;

    public AudioClip[] tabDebutMatch;
    private AudioSource sourceDebutMatch;

    public AudioClip[] tabDialogue;
    private AudioSource sourceDialogue;

    public AudioClip[] tabMatch;
    private AudioSource sourceMatch;

    public AudioMixerGroup mixerVoice;

    // Use this for initialization
    void Start()
    {
        sourceTroisDeuxUn = gameObject.AddComponent<AudioSource>();
        sourceTroisDeuxUn.outputAudioMixerGroup = mixerVoice;

        sourceBut = gameObject.AddComponent<AudioSource>();
        sourceBut.outputAudioMixerGroup = mixerVoice;

        sourceDebutMatch = gameObject.AddComponent<AudioSource>();
        sourceDebutMatch.outputAudioMixerGroup = mixerVoice;
        
        sourceDialogue = gameObject.AddComponent<AudioSource>();
        sourceDialogue.outputAudioMixerGroup = mixerVoice;
        
        sourceMatch = gameObject.AddComponent<AudioSource>();
        sourceMatch.outputAudioMixerGroup = mixerVoice;
    }

    public void PlayRandomSound(Action action)
    {
        switch (action)
        {
            case AbstractSound.Action.TroisDeuxUn:
                sourceTroisDeuxUn.clip = tabTroisDeuxUn[Random.Range(0, tabTroisDeuxUn.Length)];
                sourceTroisDeuxUn.Play();
                break;
            case AbstractSound.Action.But:
                sourceBut.clip = tabBut[Random.Range(0, tabBut.Length)];
                sourceBut.Play();
                break;
            case AbstractSound.Action.Debut:
                sourceDebutMatch.clip = tabDebutMatch[Random.Range(0, tabDebutMatch.Length)];
                sourceDebutMatch.Play();
                break;
            case AbstractSound.Action.Dialogue:
                sourceDialogue.clip = tabDialogue[Random.Range(0, tabDialogue.Length)];
                sourceDialogue.Play();
                break;
            case AbstractSound.Action.Match:
                sourceMatch.clip = tabMatch[Random.Range(0, tabMatch.Length)];
                sourceMatch.Play();
                break;

            default:
                Debug.LogError("L'action " + action + "n'existe pas dans ce contexte.");
                break;
        }

    }
    //static void 
}
