using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    uint bankID;

	// Use this for initialization
	public void LoadBank() {
        AkSoundEngine.LoadBank("MainSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
	}
	
    public void PlayEvent(string eventName, GameObject source)
    {
        AkSoundEngine.PostEvent(eventName,source);
    }

    public void StopEvent(string eventName, GameObject source, int fadeout)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Stop, source, fadeout, AkCurveInterpolation.AkCurveInterpolation_Sine);
    }

    public void ResumeEvent(string eventName, GameObject source, int fadeout)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Resume, source, fadeout, AkCurveInterpolation.AkCurveInterpolation_Sine);
    }

    public void PauseEvent(string eventName, GameObject source, int fadeout)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Pause, source, fadeout, AkCurveInterpolation.AkCurveInterpolation_Sine);
    }
}
