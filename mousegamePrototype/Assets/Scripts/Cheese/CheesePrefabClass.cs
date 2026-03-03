using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CheesePrefabClass : MonoBehaviour
{
    EventCore eventcore;
    [Header("Distance from the play until the mesh renderer gets turned off")]
    public float distanceForDisable = 5;

    [Space(10.0f)]
    [Header("Players transform")]
    public Transform playerTransform;
    
    [Space(10.0f)]
    [Header("Player transform")]
    public int sidesWidthCubes = 0;

    [Space(10.0f)]
    [Header("Teeth needed")]
    public TeethObjects neededTeeth;

    [Space(10.0f)]
    [Header("How much food it give to the mouse")]
    public int foodPoints = 0;

    BoxCollider myBoxCollider;
    Material fadeMaterial;
   
    private void Start()
    {
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        fadeMaterial = new Material(GetComponent<MeshRenderer>().material);
        myBoxCollider = GetComponent<BoxCollider>();

        eventcore.CarveItem.AddListener(checkSurroundings);
        //eventcore.MouseEatingCheese.AddListener(giveFoodToMouse);
    }
    private void OnTriggerEnter(Collider other)
    {
        MouseScript ms;
        if (other.gameObject.GetComponent<MouseScript>() != null)
        {
            ms = other.gameObject.GetComponent<MouseScript>();
        }
        else
        {
            return;
        }
        if (ms.teethSet == neededTeeth)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        
    }
    private void Update()
    {
        checkSurroundings(null);

        disableBoxCollider();
        disableRenderer();
    }
    void checkSurroundings(GameObject _nothin)
    {
        turnOnCollider();
        Vector3[] checkingSides = new Vector3[6];
        checkingSides[0] = transform.forward;
        checkingSides[1] = transform.forward * -1;
        checkingSides[2] = transform.right;
        checkingSides[3] = transform.right * -1;
        checkingSides[4] = transform.up;
        checkingSides[5] = transform.up * -1;
        Vector3 checkDistance;
        sidesWidthCubes = 0;
        foreach (Vector3 _Var in checkingSides)
        {
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + _Var * 1);
            if (Physics.Raycast(transform.position + _Var, _Var, out hit, 0.5f))
            {
                if (hit.collider.gameObject.CompareTag("Cheese") && hit.collider.gameObject != this.gameObject)
                {
                  sidesWidthCubes++;
                }
            }
            
        }

        if (sidesWidthCubes == 6)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
    void disableBoxCollider()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > distanceForDisable)
        {
            myBoxCollider.enabled = false;
        }
        else
        {
            myBoxCollider.enabled = true;
        }
    }
    void turnOnCollider()
    {
        myBoxCollider.enabled = true;
    }
    void disableRenderer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > distanceForDisable * 10)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
    void giveFoodToMouse(MouseScript ms)
    {
        if (ms.foodMeter > 5)
        {
            return;
        }
        ms.foodMeter += foodPoints;
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
