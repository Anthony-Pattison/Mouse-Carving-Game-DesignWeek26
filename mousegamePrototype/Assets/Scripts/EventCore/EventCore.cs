using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EventCore : MonoBehaviour
{
    public UnityEvent<GameObject> CarveItem;

    public UnityEvent<MouseScript> TeethChange;

    public UnityEvent<MouseScript> MouseEatingCheese;
    private void Start()
    {
        Application.targetFrameRate = 1000;
    }
}
