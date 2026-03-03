using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EventCore : MonoBehaviour
{
   public UnityEvent<GameObject> CarveItem;

    private void Start()
    {
        Application.targetFrameRate = 1000;
    }
}
