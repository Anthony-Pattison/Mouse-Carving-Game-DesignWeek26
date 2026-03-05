using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    float clothJumpBonus = 2.0f;
    public bool canJump = false;
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

    [Space(5.0f)]
    [Header("Prefab for throwing up")]
    public GameObject throwUpBlock;

    [Space(5.0f)]
    [Header("list of the previous eaten objects")]
    public List<GameObject> previousEatenMaterials;

    [HideInInspector]
    public GameObject cheeseBlockToEat;
    public Vector3 rayCastOffset;
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
        eventcore.TeethChange.AddListener(mouseChangeTeethSet);

        Cursor.lockState = CursorLockMode.Locked;
        eventcore.TeethChange.Invoke(teethSet);
    }

    // Update is called once per frame
    void Update()
    {
        gettingInput();
        mouseMovement();
        spittingOutFood();
        mouseInput();
        manageEatenMaterial();

        if (Input.GetKeyDown(gameReset))
        {
            eventcore.ResetTheGame.Invoke();
        }

    }

    void mouseChangeTeethSet(TeethObjects newTeeth)
    {
        teethSet = newTeeth;
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
        CheesePrefabClass che = CarvedItem.GetComponent<CheesePrefabClass>();

        if (CarvedItem.GetComponent<CheesePrefabClass>() == null)
        {
            return;
        }
        if (!che.turnOffCheese())
            return;

        previousEatenMaterials.Add(CarvedItem);


        if (foodMeter < 5)
        {
            foodMeter++;
        }
        eventcore.MouseEatingCheese.Invoke(this);

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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && canJump)
        {
            jumpPulse = jumpAmount;
        }
    }
    void mouseMovement()
    {
        // calculating movement


        Vector3 movement = (transform.right * xInput + transform.forward * zInput) * speed;

        movement.y = checkBlockOfCheese();
        jumpPulse += clothBlockCheck();
        Vector3 rotation = new Vector3(0, yRotation, 0);

        transform.position += movement * Time.deltaTime;
        sceneCamera.position += movement * Time.deltaTime;

        // apply jump
        rb.AddForce(new Vector3(0, jumpPulse * 10, 0), ForceMode.Impulse);
    }



    void spittingOutFood()
    {
        if (Input.GetMouseButtonDown(1))
        {

            // remove form eaten items
            int itemPlace = previousEatenMaterials.Count - 1;
            if (itemPlace < 0)
            {
                return;
            }
            GameObject materialThrowingUp = previousEatenMaterials[itemPlace];
            previousEatenMaterials.RemoveAt(itemPlace);

            // spawn block
            Vector3 scaleOffset = transform.lossyScale;
            Vector3 pos = transform.position + rayCastOffset + new Vector3(0, 0.5f, 0); ;

            GameObject pukeBlock = Instantiate(throwUpBlock, pos, Quaternion.identity);
            pukeBlock.tag = "puke";
            pukeBlock.GetComponent<CheesePrefabClass>().playerTransform = transform;
            pukeBlock.GetComponent<CheesePrefabClass>().eaten = true;
            setThrowUpBlock(pukeBlock, materialThrowingUp);
            foodMeter--;
            eventcore.MouseEatingCheese.Invoke(this);
        }
    }
    void setThrowUpBlock(GameObject _throwingUpBlock, GameObject _lastEatenMaterial)
    {
        _throwingUpBlock.GetComponent<MeshRenderer>().material = _lastEatenMaterial.GetComponent<MeshRenderer>().material;
        _throwingUpBlock.GetComponent<CheesePrefabClass>().neededTeeth = _lastEatenMaterial.GetComponent<CheesePrefabClass>().neededTeeth;
    }
    void manageEatenMaterial()
    {
        if (previousEatenMaterials.Count > 5)
        {
            previousEatenMaterials.Remove(previousEatenMaterials[0]);
        }
    }
    bool isGrounded()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hit, 0.5f))
        {
            return true;
        }
        return false;
    }

    float clothBlockCheck()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hit, 0.5f))
        {
            if (hit.collider.gameObject.GetComponent<CheesePrefabClass>() == null)
            {
                return 0;
            }

            CheesePrefabClass Cheese = hit.collider.gameObject.GetComponent<CheesePrefabClass>();

            if (Cheese.neededTeeth.chosenTeethSet == Teeth.clothTeeth && Cheese.eaten)
            {
                return clothJumpBonus;
            }
        }
        return 0;
    }
    float checkBlockOfCheese()
    {
        Debug.DrawLine(transform.position - new Vector3(0, .25f, 0), (transform.position - new Vector3(0, .2f, 0)) + transform.forward, Color.red);
        if (Physics.Raycast(transform.position - new Vector3(0, .3f, 0), transform.forward, out RaycastHit hit, 1))
        {
            if (hit.collider.gameObject.GetComponent<CheesePrefabClass>() != null)
            {
                CheesePrefabClass Cheese = hit.collider.gameObject.GetComponent<CheesePrefabClass>();
                if (Cheese.neededTeeth.chosenTeethSet == Teeth.cheeseTeeth && Cheese.eaten)
                {

                    StopAllCoroutines();
                    rb.isKinematic = true;
                    StartCoroutine(turnOnRigidBody());
                }
            }

        }

        if (Physics.Raycast(transform.position - new Vector3(0, .2f, 0), transform.forward, out RaycastHit _hit, 1))
        {
            if (_hit.collider.gameObject.GetComponent<CheesePrefabClass>() == null)
            {
                return 0;
            }

            CheesePrefabClass Cheese = _hit.collider.gameObject.GetComponent<CheesePrefabClass>();

            if (Cheese.neededTeeth.chosenTeethSet == Teeth.cheeseTeeth && Cheese.eaten)
            {
                return 5;
            }

        }
        return 0;
    }
    IEnumerator turnOnRigidBody()
    {
        yield return new WaitForSeconds(.25f);
        rb.isKinematic = false;

    }
    void resetPlayState()
    {
        transform.position = playerStartPosition;
        foodMeter = 0;
        eventcore.MouseEatingCheese.Invoke(this);
    }
}
