using UnityEngine;

public class TeethChange : MonoBehaviour
{
    public TeethObjects teeth;
    EventCore eventcore;
    public bool mouseHere;
    private void Start()
    {
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        
    }
    
    public void changeTeeth()
    {
        eventcore.TeethChange.Invoke(teeth);
    }
}
