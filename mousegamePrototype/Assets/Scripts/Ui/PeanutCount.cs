using TMPro;
using UnityEngine;

public class PeanutCount : MonoBehaviour
{
    int maxPeanuts = 0;
    int currentPeanuts = 0;
    public TextMeshProUGUI text;
    public GameObject peanutsParents;
    EventCore eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.collectPeanut.AddListener(updateText);
        maxPeanuts = peanutsParents.transform.childCount;
        text.text = $"{currentPeanuts}/{maxPeanuts}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateText()
    {
        currentPeanuts++;
        text.text = $"{currentPeanuts}/{maxPeanuts}";
    }
}
