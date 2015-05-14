using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public abstract class AbstractSound : MonoBehaviour {

    public enum Action{
        Hit,
        Course
    }

	public void PlayRandomSound(Action action){

    }
}
