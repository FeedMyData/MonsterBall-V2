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

    private SoundManager sound;

    private float durationFade = 0.5f;
    private float timePanelEnjmin = 2.0f;

    public void Start()
    {

        sound = GetComponent<SoundManager>();
        sound.LoadBank();

        if (GameControllerF.IsFirstLaunched())
        {
            ButtonPlay.GetComponent<Button>().interactable = false;
            ButtonBack.GetComponent<Button>().interactable = false;
            ButtonCredits.GetComponent<Button>().interactable = false;
            StartCoroutine(WaitButtonActivation());

            pnlEnjmin.SetActive(true);
            pnlMain.SetActive(false);
            StartCoroutine(EnjminScreen());
        }
        else
        {
            MenuDisplayed();
            if (ButtonPlay != null)
                EventSystem.current.SetSelectedGameObject(ButtonPlay);
        }

        pnlCredits.SetActive(false);
        pnlLoading.SetActive(false);

    }

    private IEnumerator WaitButtonActivation()
    {
        yield return new WaitForSeconds(durationFade + timePanelEnjmin);
        ButtonPlay.GetComponent<Button>().interactable = true;
        ButtonBack.GetComponent<Button>().interactable = true;
        ButtonCredits.GetComponent<Button>().interactable = true;
        if (ButtonPlay != null)
            EventSystem.current.SetSelectedGameObject(ButtonPlay);

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
        sound.PlayEvent("Menu_Valid",Camera.main.gameObject);
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
        GameControllerF.SetIsFirstLaunched(false);
        yield return new WaitForSeconds(timePanelEnjmin);
        pnlMain.SetActive(true);
        StartCoroutine(FadeTo(0.0f, durationFade));
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
        MenuDisplayed();
    }

    void MenuDisplayed()
    {
        pnlMain.SetActive(true);
        pnlEnjmin.SetActive(false);
        Debug.Log("caca");
        sound.PlayEvent("Music_Menu", Camera.main.gameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
