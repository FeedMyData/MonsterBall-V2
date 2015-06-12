using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CracksGUI : MonoBehaviour {

    private Texture[] spritesCracks;
    private GameObject[] objectsCracks;
    private List<RawImage> cracksAvailable;
    private List<RawImage> cracksEnabled;

    private float minAlpha = 0.1f;
    private float maxAlpha = 1.0f;

    private Color baseColor = new Color(1, 1, 1, 0);
    private Color currentAlphaColor = new Color(1, 1, 1, 0.0f);

    void Awake()
    {
        spritesCracks = Resources.LoadAll<Texture>("Crackcamera");
        objectsCracks = new GameObject[spritesCracks.Length];
        cracksAvailable = new List<RawImage>();
        cracksEnabled = new List<RawImage>();

        for (int i = 0, nb = objectsCracks.Length; i < nb; i++)
        {
            objectsCracks[i] = new GameObject("crackCamera" + i);
            objectsCracks[i].transform.SetParent(transform, false);
            RawImage textureRaw = objectsCracks[i].AddComponent<RawImage>();
            textureRaw.enabled = false;
            textureRaw.texture = spritesCracks[i];
            textureRaw.color = baseColor;

            cracksAvailable.Add(textureRaw);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DesactivateCracks()
    {
        RemoveAllCracks();
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
                RawImage newCrack = cracksAvailable[Random.Range(0, cracksAvailable.Count)];
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
        foreach (RawImage crack in cracksEnabled)
        {
            crack.color = currentAlphaColor;
        }
    }

    void RemoveAllCracks()
    {
        foreach (RawImage crack in cracksEnabled)
        {
            crack.enabled = false;
            crack.color = baseColor;
            cracksEnabled.Remove(crack);
            cracksAvailable.Add(crack);
        }
    }
}
