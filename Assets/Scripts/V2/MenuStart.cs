using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuStart : MonoBehaviour {

    public GameObject defaultButtonMain;
    public GameObject defaultButtonCreditsFromMain;
    public GameObject defaultButtonCredits;

    public GameObject pnlMain;
    public GameObject pnlCredits;

    public float timeLoading = 5.0f;
    private float speedRotation = 160.0f;
    public GameObject pnlLoading;
    private bool canRotate = false;

    public void Start()
    {
        if (defaultButtonMain != null)
            EventSystem.current.SetSelectedGameObject(defaultButtonMain);
    }

    public void Play()
    {
        pnlMain.SetActive(false);
        pnlLoading.SetActive(true);
        canRotate = true;
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
        if (defaultButtonCredits != null)
            EventSystem.current.SetSelectedGameObject(defaultButtonCredits);
    }

    public void ToMain()
    {
        pnlCredits.SetActive(false);
        pnlMain.SetActive(true);
        if (defaultButtonCreditsFromMain != null)
            EventSystem.current.SetSelectedGameObject(defaultButtonCreditsFromMain);


    }

    public void Quit()
    {
        Application.Quit();
    }
}
