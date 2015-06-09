using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnjminSplashScreen : MonoBehaviour {

	public GameObject PnlMain;
	public Image CnamEnjmin;

	IEnumerator Start () 
	{
		yield return new WaitForSeconds (2.0f);
		StartCoroutine (FadeTo(0.0f,0.5f));
	}

	IEnumerator FadeTo(float aValue, float aTime) 
	{
		PnlMain.SetActive (true);
		float alpha = CnamEnjmin.color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
				CnamEnjmin.color = newColor;
				yield return null;
			}
		gameObject.SetActive (false);
	}
}
