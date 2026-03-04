using UnityEngine;

public class CheeseSpawner : MonoBehaviour
{
    [Header("Cheese")]
    public GameObject cheeseToSpawn;

    [Header("Teeth needed to eat material")]
    [SerializeField]
    TeethObjects teethNeeded;

    [Space(10.0f)]
    [Header("How much cheese per axis")]
    [SerializeField]
    float xCheese;
    [SerializeField]
    float yCheese;
    [SerializeField]
    float zCheese;

    public MouseScript ms;

    EventCore eventcore;

    public void Start()
    {
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventcore.ResetTheGame.AddListener(resetPlayState);
    }
    [ContextMenu("Spawn Cheese")]
    private void spawnCheese()
    {
        for (int t = 0; t < zCheese; t++) {
            for (int e = 0; e < yCheese; e++) {
                for (int i = 0; i < xCheese; i++)
                {
                    GameObject _tempCheese = Instantiate(cheeseToSpawn, transform.position +
                        new Vector3(
                        cheeseToSpawn.transform.localScale.x * i, 
                        cheeseToSpawn.transform.localScale.z * t,
                        cheeseToSpawn.transform.localScale.z * e), Quaternion.identity, transform);
                    _tempCheese.GetComponent<CheesePrefabClass>().playerTransform = GameObject.Find("mousePlayer").transform;
                    cheeseToSpawn.GetComponent<CheesePrefabClass>().neededTeeth = teethNeeded;
                    cheeseToSpawn.GetComponent<CheesePrefabClass>().ms = ms;
                    _tempCheese.isStatic = true;
                }
            }
        }
    }

    public void resetPlayState()
    {
        int childCount = gameObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
