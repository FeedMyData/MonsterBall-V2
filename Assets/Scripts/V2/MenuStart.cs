using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuStart : MonoBehaviour {

    public GameObject defaultButton;

    public void Start()
    {
        if (defaultButton != null)
            EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    public void Play()
    {
        Application.LoadLevel(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
