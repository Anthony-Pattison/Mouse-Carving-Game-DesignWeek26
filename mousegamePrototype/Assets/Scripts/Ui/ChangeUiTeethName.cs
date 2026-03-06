using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUiTeethName : MonoBehaviour
{
    EventCore eventCore;
    public TextMeshProUGUI hungerText;
    public Image teethImage;
    public Sprite cheeseTeeth;
    public Sprite fabricTeeth;
    public Sprite woodTeeth;
    public Sprite chainSawTeeth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.TeethChange.AddListener(callText);
        eventCore.MouseEatingCheese.AddListener(changeHunger);

    }

    void callText(TeethObjects ms)
    {
        GetComponent<TextMeshProUGUI>().text = ms.chosenTeethSet.ToString();
        if (ms.chosenTeethSet.ToString() == "cheeseTeeth")
        {
            teethImage.sprite = cheeseTeeth;
        }
        if (ms.chosenTeethSet.ToString() == "clothTeeth")
        {
            teethImage.sprite = fabricTeeth;
        }
        if (ms.chosenTeethSet.ToString() == "woodTeeth")
        {
            teethImage.sprite = woodTeeth;
        }
        if (ms.chosenTeethSet.ToString() == "chainSawTeeth")
        {
            teethImage.sprite = chainSawTeeth;
        }
    }
    void changeHunger(MouseScript ms)
    {
        int foodMeterNum = ms.foodMeter;

        hungerText.text = foodMeterNum.ToString();
    }
}
