using System;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            Debug.Log("flashlight toggled");
        }
    }
}
