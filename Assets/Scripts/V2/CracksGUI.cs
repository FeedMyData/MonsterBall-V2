using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CracksGUI : MonoBehaviour {

    private Sprite[] spritesCracks;
    private GameObject[] objectsCracks;
    private List<Image> cracksAvailable;
    private List<Image> cracksEnabled;

    public float minAlpha = 0.2f;
    public float maxAlpha = 0.8f;
    public float timeDisappearing = 1.0f;

    private Color baseColor = new Color(1, 1, 1, 0);
    private Color currentAlphaColor = new Color(1, 1, 1, 0.0f);

    void Awake()
    {
        spritesCracks = Resources.LoadAll<Sprite>("Crackcamera");
        objectsCracks = new GameObject[spritesCracks.Length];
        cracksAvailable = new List<Image>();
        cracksEnabled = new List<Image>();

        for (int i = 0, nb = objectsCracks.Length; i < nb; i++)
        {
            objectsCracks[i] = new GameObject("crackCamera" + i);
            objectsCracks[i].transform.SetParent(transform, false);
            Image textureRaw = objectsCracks[i].AddComponent<Image>();
            textureRaw.enabled = false;
            textureRaw.sprite = spritesCracks[i];
            textureRaw.color = baseColor;
            textureRaw.rectTransform.sizeDelta = new Vector2(spritesCracks[i].rect.width, spritesCracks[i].rect.height);

            cracksAvailable.Add(textureRaw);
        }
    }

    public void DesactivateCracks()
    {
        StartCoroutine(RemoveAllCracks(timeDisappearing));
    }

    public void AddCrack(int currentRebound, float maxRebounds)
    {
        float percentageL = currentRebound / maxRebounds;
        currentAlphaColor.a = Mathf.Lerp(minAlpha, maxAlpha, percentageL); ;
        AddAlphaOnCracks();

        int numberToDisplay = Mathf.FloorToInt(objectsCracks.Length * percentageL);
        int numberToAdd = numberToDisplay - cracksEnabled.Count;
        if (numberToAdd > 0)
        {
            StartCoroutine(DisplayNewCracks(numberToAdd));
        }
    }

    IEnumerator DisplayNewCracks(int number)
    {
        for (int i = 0, nb = number; i < nb; i++)
        {
            if (cracksAvailable.Count > 0)
            {
                Image newCrack = cracksAvailable[Random.Range(0, cracksAvailable.Count)];
                newCrack.color = currentAlphaColor;
                newCrack.enabled = true;
                cracksEnabled.Add(newCrack);
                cracksAvailable.Remove(newCrack);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void AddAlphaOnCracks()
    {
        foreach (Image crack in cracksEnabled)
        {
            crack.color = currentAlphaColor;
        }
    }

    IEnumerator RemoveAllCracks(float aTime)
    {
        Color beginAlpha = currentAlphaColor;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            currentAlphaColor = new Color(1, 1, 1, Mathf.Lerp(beginAlpha.a, 0.0f, t));
            foreach (Image crack in cracksEnabled)
            {
                crack.color = currentAlphaColor;
            }
            yield return null;
        }
        currentAlphaColor = new Color(1, 1, 1, 0.0f);
        foreach (Image crack in cracksEnabled)
        {
            crack.enabled = false;
            crack.color = currentAlphaColor;
            cracksAvailable.Add(crack);
        }
        
        cracksEnabled.Clear();
    }
}
