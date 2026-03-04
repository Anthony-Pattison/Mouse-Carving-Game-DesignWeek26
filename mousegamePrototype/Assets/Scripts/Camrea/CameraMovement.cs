using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Transform Orientation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Orientation.position;
        movingView();
    }

    void movingView()
    {
        float mouseX = Input.GetAxis("Mouse Y");
        float mouseY = Input.GetAxis("Mouse X");
        mouseX = Mathf.Clamp(mouseX, -90, 90);
        transform.Rotate(mouseX * -1, mouseY, 0);
        Vector3 rot = transform.eulerAngles;
        rot.z = 0;
        transform.eulerAngles = rot;

        Vector3 playerRotation = transform.eulerAngles;
        playerRotation.z = 0;
        playerRotation.x = 0;

        playerTransform.eulerAngles = playerRotation;
    }
}
