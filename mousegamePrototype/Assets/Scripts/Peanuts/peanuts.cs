using UnityEngine;

public class peanuts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    EventCore eventCore;

    private void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            eventCore.collectPeanut.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
