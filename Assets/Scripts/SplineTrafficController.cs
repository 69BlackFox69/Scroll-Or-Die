using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineTrafficController : MonoBehaviour
{
    private SplineAnimate[] cars;

    void Awake()
    {
        cars = GetComponentsInChildren<SplineAnimate>();
    }

    void OnEnable()
    {
        TrafficLightController.OnLightChanged += HandleLightChange;
    }

    void OnDisable()
    {
        TrafficLightController.OnLightChanged -= HandleLightChange;
    }

    void HandleLightChange(bool isGreen)
    {
        foreach (var car in cars)
        {
            if (isGreen)
                car.Pause();
            else
                car.Play();
        }
    }
}
