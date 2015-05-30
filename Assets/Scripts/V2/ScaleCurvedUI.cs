using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleCurvedUI : MonoBehaviour {

    //public bool checkForResize = true;
    //public float checkForResizeRate = 0.03f;

    //int lastWidth;
    //int lastHeight;
    //bool stay = true;

    // magic numbers : voir le tableau dans le drive/prog et le compléter pour des magic numbers plus précis
    private float angleA = 0.04207119741f;
    private float angleB = 99.45631068f;
    private float scaleA = -0.00006472491909f;
    private float scaleB = 0.2348474341f;

	// Use this for initialization
	void Start () {

        AdaptCylinderUI();

        //if(checkForResize) StartCoroutine(check_for_resize());

	}
	
	// Update is called once per frame
	void Update () {
	
    }

    void AdaptCylinderUI()
    {
        GetComponent<CylinderMapping>().m_angle = angleA * Screen.width + angleB;
        transform.localScale = new Vector3(scaleA * Screen.width + scaleB, transform.localScale.y, transform.localScale.z);
    }

    //IEnumerator check_for_resize()
    //{
    //    lastWidth = Screen.width;
    //    lastHeight = Screen.height;

    //    while (stay)
    //    {
    //        if (lastWidth != Screen.width || lastHeight != Screen.height)
    //        {
    //            AdaptCylinderUI();
    //            lastWidth = Screen.width;
    //            lastHeight = Screen.height;
    //        }
    //        yield return new WaitForSeconds(checkForResizeRate);
    //    }
    //}

    //void OnDestroy()
    //{
    //    stay = false;
    //}
}
