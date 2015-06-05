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

    public void Start()
    {
        if (defaultButtonMain != null)
            EventSystem.current.SetSelectedGameObject(defaultButtonMain);
    }

    public void Play()
    {
        Application.LoadLevel(1);
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
        if (defaultButtonMain != null)
            EventSystem.current.SetSelectedGameObject(defaultButtonCreditsFromMain);


    }

    public void Quit()
    {
        Application.Quit();
    }
}
