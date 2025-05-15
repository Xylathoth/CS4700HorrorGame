using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class DrunkQuest : MonoBehaviour
{
    private GameManager gameManager;
    private bool questStarted = false;
    private bool questComplete = false;
    private bool messageShown = false;
    public bool hasBottle = false;
    private Collider myCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<Collider>(); 
        gameManager = FindFirstObjectByType<GameManager>();
        myCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!messageShown && gameManager.currentRoundIndex == 2) // Alcohol round
        {
            UIManager.Instance.ShowEffectMessage("Hey boy, come to the bench.");
            myCollider.enabled = true;
            messageShown = true;
        }
    }

    
    public void OnTriggerEnter(Collider other)
    {
        // Give alcohol quest when gets near drunk
        if (other.CompareTag("Player") && !questStarted)
        {
            questStarted = true;
            StartCoroutine(GivingQuest());
        }

        // When player returns with bottle, end 
        if (other.CompareTag("Player") && hasBottle && !questComplete)
        {
            StartCoroutine(QuestComplete());
            questComplete = true;
        }
    }


    // Giving quest dialogue
    IEnumerator GivingQuest()
    {
        Debug.Log("Displaying start quest messages");


        UIManager.Instance.ShowEffectMessage("I need to you to get me something.");
        yield return new WaitForSeconds(UIManager.Instance.messageDisplayTime);
        UIManager.Instance.ShowEffectMessage("Grab that bottle behind the gas station.");
        yield return new WaitForSeconds(UIManager.Instance.messageDisplayTime);
        UIManager.Instance.ShowEffectMessage("I'll fill it up with some moonshine for you.");
    }

    // Quest complete dialogue
    IEnumerator QuestComplete()
    {
        Debug.Log("Displaying quest complete messages");

        UIManager.Instance.ShowEffectMessage("Nice job boy.");
        yield return new WaitForSeconds(UIManager.Instance.messageDisplayTime);
        UIManager.Instance.ShowEffectMessage("Lemme fill 'er up.");
        yield return new WaitForSeconds(UIManager.Instance.messageDisplayTime);


        gameManager.SpawnDrug();
    }
}
