using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using NUnit.Framework.Constraints;
using UnityEngine.UI;

public class VomitMeter : MonoBehaviour
{
    MouseScript ms;
    public List<GameObject> vomitMaterials;
    public List<GameObject> uiElements;
    public Color yellow;
    public Color red;
    public Color brown;
    public Sprite cheeseSprite;
    public Sprite clothSprite;
    public Sprite woodSprite;

    public TeethObjects cheeseTeeth;
    public TeethObjects clothTeeth;
    public TeethObjects woodTeeth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ms = GameObject.Find("mousePlayer").GetComponent<MouseScript>();
        vomitMaterials = ms.previousEatenMaterials;

        cheeseTeeth.chosenTeethSet = Teeth.cheeseTeeth;
        clothTeeth.chosenTeethSet = Teeth.clothTeeth;
        woodTeeth.chosenTeethSet = Teeth.woodTeeth;
        foreach (GameObject obj in uiElements)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeUi();
    }

    void changeUi()
    {
        int index = 0;
        foreach (GameObject go in vomitMaterials)
        {
            if (go.GetComponent<CheesePrefabClass>().neededTeeth == cheeseTeeth)
            {
                uiElements[index].SetActive(true);
                uiElements[index].GetComponent<Image>().sprite = cheeseSprite;
                index++;
            }
            if (go.GetComponent<CheesePrefabClass>().neededTeeth == clothTeeth)
            {
                uiElements[index].SetActive(true);
                uiElements[index].GetComponent<Image>().sprite = clothSprite;
                index++;
            }
            if (go.GetComponent<CheesePrefabClass>().neededTeeth == woodTeeth)
            {
                uiElements[index].SetActive(true);
                uiElements[index].GetComponent<Image>().sprite = woodSprite;
                index++;
            }
        }
        for (int i = 5 - index; i > 0; i--)
        {
            uiElements[uiElements.Count - i].SetActive(false);
        }
    }
}
