using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // singleton

    public TextMeshProUGUI effectMessageText;
    public float messageDisplayTime = 2f;

    private void Awake()
    {
        // singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowEffectMessage(string message)
    {
        StopAllCoroutines(); // stop previous message timer if needed
        effectMessageText.text = message;
        StartCoroutine(ClearMessageAfterDelay());
    }

    private System.Collections.IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDisplayTime);
        effectMessageText.text = "";
    }
}
