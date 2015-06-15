using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuStart : MonoBehaviour {

    public GameObject ButtonPlay;
    public GameObject ButtonBack;
    public GameObject ButtonCredits;

    public GameObject pnlMain;
    public GameObject pnlCredits;
    public GameObject pnlEnjmin;
    public GameObject pnlLoading;

    public Image CnamEnjmin;

    public float timeLoading = 5.0f;

    public void Start()
    {

        pnlMain.SetActive(false);
        pnlCredits.SetActive(false);
        pnlLoading.SetActive(false);
        pnlEnjmin.SetActive(true);

        StartCoroutine(EnjminScreen());
    }

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null) {
            if (Input.GetAxis("Vertical") > 0.3f || Input.GetAxis("Vertical") < -0.3f)
            {
                if (pnlMain.activeSelf)
                {
                    EventSystem.current.SetSelectedGameObject(ButtonPlay);
                }
                else if (pnlCredits.activeSelf)
                {
                    EventSystem.current.SetSelectedGameObject(ButtonBack);
                }
            }
        }
    }

    public void Play()
    {
        pnlMain.SetActive(false);
        pnlLoading.SetActive(true);
        StartCoroutine(PlayCoroutine());
    }

    IEnumerator PlayCoroutine()
    {
        AsyncOperation aop = Application.LoadLevelAsync(1);
        aop.allowSceneActivation = false;
        yield return new WaitForSeconds(timeLoading);
        aop.allowSceneActivation = true;
    }

    public void ToCredits()
    {
        pnlMain.SetActive(false);
        pnlCredits.SetActive(true);
        if (ButtonBack != null)
            EventSystem.current.SetSelectedGameObject(ButtonBack);
    }

    public void ToMain()
    {
        pnlCredits.SetActive(false);
        pnlMain.SetActive(true);
        if (ButtonCredits != null)
            EventSystem.current.SetSelectedGameObject(ButtonCredits);

    }

    IEnumerator EnjminScreen()
    {
        yield return new WaitForSeconds(2.0f);
        pnlMain.SetActive(true);
        if (ButtonPlay != null)
            EventSystem.current.SetSelectedGameObject(ButtonPlay);
        StartCoroutine(FadeTo(0.0f, 0.5f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = CnamEnjmin.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            CnamEnjmin.color = newColor;
            yield return null;
        }
        pnlEnjmin.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
