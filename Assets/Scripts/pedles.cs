using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedles : MonoBehaviour
{
    [SerializeField] private CalmnessEffectController calmness;
    public float stresAmount = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Уменьшаем спокойствие на 20 напрямую
            calmness.AddStres(stresAmount);
        }
    }
}
