using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BateryBar : MonoBehaviour
{
    public float bateryLevel = 100f;
    public Image bateryImage;
    public MovePlayer setGameOver;

    [Header("Drain Settings")]
    public float drainPerSecond = 1f;  // сколько единиц убывает в секунду
    public bool isDraining = true;

    [SerializeField] private CalmnessEffectController calmness;

    void Update()
    {
        if (isDraining)
        {
            bateryLevel -= drainPerSecond * Time.deltaTime;
            bateryLevel = Mathf.Clamp(bateryLevel, 0f, 100f); // не уходит ниже 0
            UpdatebateryLevel();
        }
        if (bateryLevel <= 0f)
        {
            calmness.SetBateryDrain(true);

        }
        else
        {
            calmness.SetBateryDrain(false);
        }

    }

    public void UpdatebateryLevel()
    {
        bateryImage.fillAmount = bateryLevel / 100f;
    }
}
