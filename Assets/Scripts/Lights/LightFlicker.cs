using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light pointLight;
    private float baseIntensity;

    [Header("Flicker Settings")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        pointLight = GetComponent<Light>();
        if (pointLight == null)
        {
            Debug.LogError("No Light component found on this GameObject!");
            return;
        }

        baseIntensity = pointLight.intensity;
        InvokeRepeating(nameof(Flicker), 0f, flickerSpeed);
    }

    void Flicker()
    {
        float randomIntensity = Random.Range(minIntensity, maxIntensity);
        pointLight.intensity = randomIntensity;
    }
}
