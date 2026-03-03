using UnityEngine;

public class TeethChange : MonoBehaviour
{
    public TeethObjects teeth;
    EventCore eventcore;
    public bool mouseHere;
    private void Start()
    {
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventcore.TeethChange.AddListener(changeTeeth);
    }
    private void OnTriggerStay(Collider other)
    {
        mouseHere = true;
    }
    private void OnTriggerExit(Collider other)
    {
        mouseHere = false;
    }
    void changeTeeth(MouseScript ms)
    {
        if (mouseHere)
        {
          ms.teethSet = teeth;
        }
    }
}
