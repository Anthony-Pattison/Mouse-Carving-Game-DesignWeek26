using UnityEngine;
using UnityEngine.Rendering;

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

    [Space(10.0f)]
    [Header("For Resetting game State")]
    public KeyCode gameReset;

    public GameObject throwUpBlock;

    public GameObject cheeseBlockToEat;
    Vector3 playerStartPosition;
    // rigid
    Rigidbody rb;

    float xInput;
    float zInput;
    //rotation
    float yRotation = 0;
    float jumpPulse = 0;

    void Start()
    {
        playerStartPosition = transform.position;

        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        rb = GetComponent<Rigidbody>();

        eventcore.ResetTheGame.AddListener(resetPlayState);
        Cursor.lockState = CursorLockMode.Locked;
        eventcore.TeethChange.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        gettingInput();
        mouseMovement();
        swappingTeeth();
        spittingOutFood();
        mouseInput();
        if (Input.GetKeyDown(gameReset))
        {
            eventcore.ResetTheGame.Invoke();
        }

    }

    void mouseInput()
    {
        if (cheeseBlockToEat == null)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            eatingItem(cheeseBlockToEat);
        }
    }

    public void eatingItem(GameObject CarvedItem)
    {

        CarvedItem.GetComponent<CheesePrefabClass>().turnOffCheese();

        if (foodMeter < 5)
        {
            foodMeter++;
        }
        eventcore.MouseEatingCheese.Invoke(this);

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

        transform.position += movement * Time.deltaTime;
        sceneCamera.position += movement * Time.deltaTime;

        // apply jump
        rb.AddForce(new Vector3(0, jumpPulse * 20, 0), ForceMode.Impulse);
    }



    void spittingOutFood()
    {
        if (foodMeter < 5)
            return;
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 scaleOffset = transform.lossyScale;
            Vector3 pos = transform.position + transform.forward;

            GameObject pukeBlock = Instantiate(throwUpBlock, pos, Quaternion.identity);

            pukeBlock.GetComponent<CheesePrefabClass>().playerTransform = transform;
            foodMeter = 0;
            eventcore.MouseEatingCheese.Invoke(this);
        }
    }

    void resetPlayState()
    {
        transform.position = playerStartPosition;
        foodMeter = 0;
        eventcore.MouseEatingCheese.Invoke(this);
    }
}
