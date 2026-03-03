using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUiTeethName : MonoBehaviour
{
    EventCore eventCore;
    public TextMeshProUGUI hungerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.TeethChange.AddListener(callText);
        eventCore.MouseEatingCheese.AddListener(changeHunger);
    }

    void callText(MouseScript ms)
    {
        GetComponent<TextMeshProUGUI>().text = ms.teethSet.chosenTeethSet.ToString();
    }
    void changeHunger(MouseScript ms)
    {
        int foodMeterNum = ms.foodMeter;

        hungerText.text = foodMeterNum.ToString();
    }
}
