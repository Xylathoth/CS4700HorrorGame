using UnityEngine;

public class EmptyBottle : MonoBehaviour
{
    private GameManager gameManager;
    private bool init;
    private DrunkQuest drunk;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        drunk = FindFirstObjectByType<DrunkQuest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!init && gameManager.currentRoundIndex == 2)
        {
            init = true;
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            Debug.Log("Empty bottle spawned");
        }
    }

    // Obtain and destroy empty bottle on collsion with player
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Empty bottle obtained. Removing this object.");
            Destroy(gameObject);
            drunk.hasBottle = true;
        }
    }
}
