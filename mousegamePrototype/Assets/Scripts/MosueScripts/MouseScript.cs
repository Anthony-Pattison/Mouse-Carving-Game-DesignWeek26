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
    [Range(0.0f, 5.0f)]
    public int foodMeter;

    public GameObject throwUpBlock;
    // rigid
    Rigidbody rb;

    float xInput;
    float zInput;
    //rotation
    float yRotation = 0;
    float jumpPulse = 0;

    void Start()
    {
        Mathf.Clamp(foodMeter, 0f, 5.0f);
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        rb = GetComponent<Rigidbody>();

        eventcore.TeethChange.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        gettingInput();
        mouseMovement();
        swappingTeeth();
        gettingMouseInput();
        spittingOutFood();
    }


    private void OnTriggerEnter(Collider other)
    {
        eatingItem(other.gameObject);
    }
    void eatingItem(GameObject CarvedItem)
    {
        print("invoking");
        if (CarvedItem.CompareTag("Cheese"))
        {
            if (foodMeter < 5)
            {
                foodMeter++;
            }
            eventcore.MouseEatingCheese.Invoke(this);
        }
    }
    void swappingTeeth()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            eventcore.TeethChange.Invoke(this);
        }
    }
    void gettingInput()
    {
        // getting input

        // movement
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        //rotation
        yRotation = 0;
        jumpPulse = 0;
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
    }
    void mouseMovement()
    {
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

    void spittingOutFood()
    {
        if (foodMeter < 5)
            return;
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 scaleOffset = transform.lossyScale;
            Vector3 pos = transform.position;

            GameObject pukeBlock = Instantiate(throwUpBlock, pos + new Vector3(
                0,
                -0.75f,
                scaleOffset.z + throwUpBlock.transform.localScale.z + 1),
                Quaternion.identity);

            pukeBlock.GetComponent<CheesePrefabClass>().playerTransform = transform;
            foodMeter = 0;
        }
    }
}
