using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 flatDir = player.position - transform.position;
        flatDir.y = 0f;

        if (flatDir.sqrMagnitude > 0.0001f)
        {
            float yaw = Mathf.Atan2(flatDir.x, flatDir.z) * Mathf.Rad2Deg;

            Vector3 e = transform.eulerAngles;
            e.y = yaw;

            transform.eulerAngles = e;
        }
    }
}
