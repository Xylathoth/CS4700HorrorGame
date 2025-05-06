using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Round
    {
        public string roundName;
        public GameObject consumablePrefab;  // Prefab for this round
        public Transform spawnPoint;         // Specific spawn point for this round
    }

    public List<Round> rounds; // Set these in Inspector

    private int currentRoundIndex = -1;
    private GameObject currentConsumable;
    private IConsumableEffect currentEffect;

    private PlayerController playerController;
    private MouseLook mouseLook;

    void Start()
    {
        // Get player refs at start
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        mouseLook = player.GetComponentInChildren<MouseLook>();

        StartNextRound();
    }

    public void StartNextRound()
    {
        currentRoundIndex++;

        if (currentRoundIndex >= rounds.Count)
        {
            Debug.Log("All rounds completed!");
            return;
        }

        Round round = rounds[currentRoundIndex];
        Debug.Log($"Starting Round {currentRoundIndex + 1}: {round.roundName}");

        // destroy old consumable
        if (currentConsumable != null)
        {
            Destroy(currentConsumable);
        }

        // spawn at the specific spawn point for this round
        if (round.spawnPoint == null)
        {
            Debug.LogError($"No spawn point set for round {currentRoundIndex + 1}!");
            return;
        }

        currentConsumable = Instantiate(round.consumablePrefab, round.spawnPoint.position, round.spawnPoint.rotation);

        // Initialize the consumable with the GameManager
        var consumableScript = currentConsumable.GetComponent<BaseConsumable>();
        if (consumableScript != null)
        {
            consumableScript.Initialize(this);
        }
    }

    public void OnConsumableConsumed(IConsumableEffect newEffect)
    {
        Debug.Log("Consumable consumed!");

        // remove previous effect
        if (currentEffect != null)
        {
            currentEffect.RemoveEffect(playerController, mouseLook);
        }

        // apply and track new effect
        currentEffect = newEffect;
        Debug.Log("New effect applied!");

        // start next round
        StartNextRound();
    }
}



//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    [System.Serializable]
//    public class Round
//    {
//        public string roundName;
//        public GameObject consumablePrefab;  // specific prefab/consumable for this round
//    }

//    public List<Round> rounds;
//    public Transform[] spawnPoints; // spawn locations for each drug

//    private int currentRoundIndex = -1; // start before first round
//    private GameObject currentConsumable;
//    private IConsumableEffect currentEffect; // currently active effect

//    private PlayerController playerController;
//    private MouseLook mouseLook;

//    void Start()
//    {
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        playerController = player.GetComponent<PlayerController>();
//        mouseLook = player.GetComponentInChildren<MouseLook>();

//        StartNextRound();
//    }

//    public void StartNextRound()
//    {
//        currentRoundIndex++;

//        if (currentRoundIndex >= rounds.Count)
//        {
//            Debug.Log("All rounds completed!");
//            return;
//        }

//        Round round = rounds[currentRoundIndex];
//        Debug.Log($"Starting Round {currentRoundIndex + 1}: {round.roundName}");

//        // Clean up any leftover consumable
//        if (currentConsumable != null)
//        {
//            Destroy(currentConsumable);
//        }

//        // Pick random spawn point
//        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

//        // Spawn the round's specific consumable
//        currentConsumable = Instantiate(round.consumablePrefab, spawnPoint.position, spawnPoint.rotation);

//        // Register the GameManager to the consumable
//        var consumableScript = currentConsumable.GetComponent<BaseConsumable>();
//        if (consumableScript != null)
//        {
//            consumableScript.Initialize(this);
//        }
//    }

//    // Called when a consumable is eaten
//    public void OnConsumableConsumed(IConsumableEffect newEffect)
//    {
//        Debug.Log("Consumable consumed!");

//        // Remove previous effect if any
//        if (currentEffect != null)
//        {
//            currentEffect.RemoveEffect(playerController, mouseLook);
//        }

//        // Apply and store new effect
//        currentEffect = newEffect;

//        Debug.Log("New effect applied!");

//        // Start next round
//        StartNextRound();
//    }
//}
