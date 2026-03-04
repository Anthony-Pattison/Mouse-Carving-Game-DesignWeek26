using UnityEngine;

public class RayCastCollision : MonoBehaviour
{
    [Header("Raycast distance")]
    [SerializeField]
    float raycastDistance = 0.5f;
    MouseScript ms;
    // Update is called once per frame
    private void Start()
    {
        ms = GameObject.Find("mousePlayer").GetComponent<MouseScript>();
    }
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0,0,raycastDistance));
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastDistance))
        {
            ms.cheeseBlockToEat = hit.collider.gameObject;
            print(hit.collider.name);
        }
    }
}
