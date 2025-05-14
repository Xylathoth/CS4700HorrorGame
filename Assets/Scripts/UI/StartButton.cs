using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private GameManager gameManager;
    public Button startButton;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Starts game on start button
    public void StartGame()
    {
        // Hide button
        startButton.gameObject.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null) controller.enabled = true;

            var look = player.GetComponentInChildren<MouseLook>();
            if (look != null) look.enabled = true;
            Cursor.lockState = CursorLockMode.Locked; // Disable cursor after game starts
        }

        gameManager.StartNextRound();
        StartCoroutine(StartMessages());
        Debug.Log("button pressed");
    }

    public IEnumerator StartMessages()
    {
        Debug.Log("Displaying starting messages");

        UIManager.Instance.ShowEffectMessage("Look for drugs.");
        yield return new WaitForSeconds(UIManager.Instance.messageDisplayTime);
        UIManager.Instance.ShowEffectMessage("Survive.");
        yield return new WaitForSeconds(UIManager.Instance.messageDisplayTime);
        UIManager.Instance.ShowEffectMessage("Don't overdose.");
    }
}
