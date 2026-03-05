using UnityEngine;

public class TeethChange : MonoBehaviour
{
    public TeethObjects teeth;
    EventCore eventcore;
    public bool mouseHere;
    public bool lookAtPlayer;
    public Transform player;
    private void Start()
    {
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        player = GameObject.Find("mousePlayer").transform;
    }
    private void Update()
    {
        if (lookAtPlayer)
        {
            transform.LookAt(player);
        }
    }
    public void changeTeeth()
    {
        eventcore.TeethChange.Invoke(teeth);
       
    }
}
