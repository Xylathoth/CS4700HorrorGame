using System.Collections;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    public float redLightDuration = 5f;
    public float yellowLightDuration = 2f;
    public float greenLightDuration = 5f;

    private void Start()
    {
        StartCoroutine(TrafficLightCycle());
    }

    IEnumerator TrafficLightCycle()
    {
        while (true)
        {
            // Red light on
            redLight.SetActive(true);
            yellowLight.SetActive(false);
            greenLight.SetActive(false);
            yield return new WaitForSeconds(redLightDuration);

            // Green light on
            redLight.SetActive(false);
            yellowLight.SetActive(false);
            greenLight.SetActive(true);
            yield return new WaitForSeconds(greenLightDuration);

            // Yellow light on
            redLight.SetActive(false);
            yellowLight.SetActive(true);
            greenLight.SetActive(false);
            yield return new WaitForSeconds(yellowLightDuration);
        }
    }
}