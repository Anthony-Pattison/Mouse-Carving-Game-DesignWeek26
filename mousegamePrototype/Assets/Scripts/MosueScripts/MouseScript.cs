using UnityEngine;

public class MouseScript : MonoBehaviour
{
    EventCore eventcore;

    [Header("For mouse movement")]
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    float rotationAmount = 5.0f;
    [SerializeField]
    float jumpAmount = 10.0f;

    [Space(10.0f)]
    [Header("Current Teeth set on the mouse")]
    public TeethObjects teethSet;

    [Space(10.0f)]
    [Header("For moving the camera")]
    public Transform sceneCamera;

    [Space(10.0f)]
    [Header("For eating cheese")]
    public BoxCollider eatingCollider;
    // rigid
    Rigidbody rb;
    void Start()
    {
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseMovement();
        gettingMouseInput();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        eatingItem(collision.gameObject);
    }
    void eatingItem(GameObject CarvedItem)
    {
        print("invoking");
        eventcore.CarveItem.Invoke(CarvedItem);
    }
    void mouseMovement()
    {
        // getting input

        // movement
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        //rotation
        float yRotation = 0;
        float jumpPulse = 0;
        if (Input.GetKey(KeyCode.E))
        {
            yRotation = rotationAmount;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            yRotation = -rotationAmount;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPulse = jumpAmount;
        }
        // calculating movement
        Vector3 movement = (transform.right * xInput + transform.forward * zInput) * speed;
        Vector3 rotation = new Vector3(0, yRotation, 0);

        // applying movement and rotation
        transform.eulerAngles += rotation * Time.deltaTime;
        sceneCamera.eulerAngles += rotation * Time.deltaTime;

        transform.position += movement * Time.deltaTime;
        sceneCamera.position += movement * Time.deltaTime;

        // apply jump
        rb.AddForce(new Vector3(0, jumpPulse, 0), ForceMode.Impulse);
    }

    void gettingMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            eatingCollider.enabled = true;
            return;
        }
        eatingCollider.enabled = false;
    }
}
