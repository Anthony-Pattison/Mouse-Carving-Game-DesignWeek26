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
        Debug.DrawRay(transform.position, transform.forward, Color.red);


        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastDistance))
        {

            if (hit.collider != null)
            {
                ms.eatingItem(hit.collider.gameObject);
                print(hit.collider.name);
            }
        }
    }
}
