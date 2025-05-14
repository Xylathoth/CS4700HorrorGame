using System.Collections;
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

    public GameObject forestBarrier;
    //public GameObject forestMonsterPrefab;
    //public Transform forestMonsterSpawnPoint;

    public GameObject abandonedBouseBarrier;

    void Start()
    {
        // Get player refs at start
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        mouseLook = player.GetComponentInChildren<MouseLook>();

        // Don't allow player control initially
        playerController.enabled = false;
        mouseLook.enabled = false;
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
        HandleRoundEvents(currentRoundIndex);   // initiate round-specific logic

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

    public void DeadPlayer()
    {
        Debug.Log("Game Over. Died from overdose.");

        // show a UI win message
        UIManager.Instance.ShowEffectMessage("RIP. You died from overdose.");

        // disable player movement / look
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null) controller.enabled = false;

            var look = player.GetComponentInChildren<MouseLook>();
            if (look != null) look.enabled = false;
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over. You win!");

        // show a UI win message
        UIManager.Instance.ShowEffectMessage("You Win!");

        // disable player movement / look
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null) controller.enabled = false;

            var look = player.GetComponentInChildren<MouseLook>();
            if (look != null) look.enabled = false;
        }

        // freeze time
        // Time.timeScale = 0f;
    }


    private void HandleRoundEvents(int roundIndex)
    {
        switch (roundIndex)
        {
            case 4:
                // after consuming shrooms (round 5), open forest area
                if (forestBarrier != null)
                {
                    forestBarrier.SetActive(false);
                    Debug.Log("Forest barrier removed.");
                }

                // Spawn forest monster
                //if (forestMonsterPrefab != null && forestMonsterSpawnPoint != null)
                //{
                //    Instantiate(forestMonsterPrefab, forestMonsterSpawnPoint.position, forestMonsterSpawnPoint.rotation);
                //    Debug.Log("Forest monster spawned.");
                //}

                break;

            case 5:
                // after consuming meth (round 6), open abandoned house
                if (abandonedBouseBarrier != null)
                {
                    abandonedBouseBarrier.SetActive(false);
                    Debug.Log("Abandoned house barrier removed.");
                }

                break;

                //case 6:
                //    // example future case: spawn monsters
                //    //SpawnMonsters();
                //    break;

                // add more cases for other round events
        }
    }


}