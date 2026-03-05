using UnityEngine;

public class RayCastCollision : MonoBehaviour
{
    [Header("Raycast distance")]
    [SerializeField]
    float raycastDistance = 0.5f;
    MouseScript ms;
    EventCore eventcore;
    // Update is called once per frame
    private void Start()
    {
        ms = GameObject.Find("mousePlayer").GetComponent <MouseScript>();
        eventcore = GameObject.Find("EventCore").GetComponent <EventCore>();
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.forward * raycastDistance);

            if (hitObject.CompareTag("TeethSwap"))
            {
                if (Input.GetMouseButton(0)) {
                    hitObject.GetComponent<TeethChange>().changeTeeth();
                }
            }
            ms.cheeseBlockToEat = hitObject;
        }
    }
}
