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

    public static void Play()
    {
        Application.LoadLevel(1);
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
