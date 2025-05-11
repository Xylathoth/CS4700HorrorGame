using UnityEngine;
using TMPro;

public class BarrierMessageTrigger : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public string message = "This area is not yet accessible";
    public GameObject forestBarrier; // to check if it's still active

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && forestBarrier.activeInHierarchy)
        {
            messageText.text = message;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = "";
        }
    }
}
