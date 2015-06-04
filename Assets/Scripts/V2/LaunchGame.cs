using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LaunchGame : MonoBehaviour {

	public void Play()
	{
		Debug.Log ("caca");
		GameObject.Find ("MBL_logo").GetComponent<Image> ().enabled = true;
		Application.LoadLevel(1);
	}

}
