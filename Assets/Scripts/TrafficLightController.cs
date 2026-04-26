using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrafficLightController : MonoBehaviour
{
    [Header("Light Objects")]
    public GameObject redLight;
    public GameObject greenLight;

    [Header("Timing")]
    public float redDuration = 15f;
    public float greenDuration = 5f;

    public bool IsGreen { get; private set; }

    public static event Action<bool> OnLightChanged;

    void Start()
    {
        StartCoroutine(TrafficLightCycle());
    }

    IEnumerator TrafficLightCycle()
    {
        while (true)
        {
            //  Красный
            redLight.SetActive(true);
            greenLight.SetActive(false);
            IsGreen = false;

            OnLightChanged?.Invoke(IsGreen);

            yield return new WaitForSeconds(redDuration);

            //  Зелёный
            redLight.SetActive(false);
            greenLight.SetActive(true);
            IsGreen = true;

            OnLightChanged?.Invoke(IsGreen);

            yield return new WaitForSeconds(greenDuration);
        }
    }
}
