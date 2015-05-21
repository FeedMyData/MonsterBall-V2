using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuiEffects : MonoBehaviour {

    public RawImage redTextureObject;
    public RawImage blueTextureObject;
    private RawImage textureToFlash;

    private bool newFlash = false;
    private bool isFlashing = false;

    private float indexFlashing = 0.0f;
    private float indexDeFlashing = 0.0f;

    public float endAlpha = 0.3f;
    public float duration = 0.2f;
    [Range(0.0f, 1.0f)]
    public float timeProportionAppearing = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.W))
        {

            //StartCoroutine(FlashWhenHit());
            flashGoal("TeamRed");

        }
        if (Input.GetKeyDown(KeyCode.X))
        {

            //StartCoroutine(FlashWhenHit());
            flashGoal("TeamBlue");

        }

        if (isFlashing)
        {

            if (newFlash)
            {
                newFlash = false;
                indexFlashing = 0.0f;
                indexDeFlashing = 0.0f;
                textureToFlash.enabled = true;
            }

            Color newAlphaColor = textureToFlash.color;

            if (indexFlashing < duration * timeProportionAppearing)
            {
                newAlphaColor.a = Mathf.Lerp(0.0f, endAlpha, indexFlashing / (duration * timeProportionAppearing));
                indexFlashing += Time.deltaTime;
            }
            else if (indexFlashing < duration)
            {
                newAlphaColor.a = Mathf.Lerp(endAlpha, 0.0f, indexDeFlashing / (duration * (1.0f - timeProportionAppearing)));
                indexDeFlashing += Time.deltaTime;
                indexFlashing += Time.deltaTime;
            }
            else
            {
                isFlashing = false;
                textureToFlash.enabled = false;
            }

            textureToFlash.color = newAlphaColor;

        }

	}

    //IEnumerator Fade(float start, float end, float length, RawImage textureObject)
    //{

    //    if (textureObject.color.a == start)
    //    {

    //        Color newAlphaColor = textureObject.color;

    //        for (float i = 0.0f; i < length; i += Time.deltaTime)
    //        {

    //            newAlphaColor.a = Mathf.Lerp(start, end, i / length);
    //            textureObject.color = newAlphaColor;
    //            yield return null;

    //        }

    //        newAlphaColor.a = end;
    //        textureObject.color = newAlphaColor;

    //    }

    //}

    //IEnumerator FlashWhenHit()
    //{

    //    StartCoroutine(Fade(0.0f, 0.5f, 2.2f, redTextureObject));
    //    yield return new WaitForSeconds(2.25f);
    //    StartCoroutine(Fade(0.5f, 0.0f, 2.2f, redTextureObject));
    
    //}

    public void flashGoal(string color)
    {

        if (color == "TeamBlu")
        {
            textureToFlash = blueTextureObject;
        }
        else
        {
            textureToFlash = redTextureObject;
        }

        newFlash = true;
        isFlashing = true;

    }

}
