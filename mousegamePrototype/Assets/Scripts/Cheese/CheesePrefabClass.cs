using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CheesePrefabClass : MonoBehaviour
{
    public float distanceForDisable = 5;
    public Transform playerTransform;
    BoxCollider myBoxCollider;
    Material fadeMaterial;
    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
        fadeMaterial = new Material(GetComponent<MeshRenderer>().material);
    }
    private void Start()
    {
        myBoxCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > distanceForDisable)
        {
            myBoxCollider.enabled = false;
        }
        else
        {
            myBoxCollider.enabled = true;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) > distanceForDisable * 10)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
