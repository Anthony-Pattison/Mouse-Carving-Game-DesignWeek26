using Unity.VisualScripting;
using UnityEngine;

public class Particals : MonoBehaviour
{
    [Header("Partical System")]
    public ParticleSystem PS;
    EventCore eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.mousePuking.AddListener(playPukePartical);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playPukePartical()
    {
        PS.Play();
    }
}
