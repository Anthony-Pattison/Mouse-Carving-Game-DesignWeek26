using UnityEngine;

public class TitleScreenCamera : MonoBehaviour
{
    public float cameraSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, cameraSpeed * Time.deltaTime, 0));
    }
}
