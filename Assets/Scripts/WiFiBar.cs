using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WiFiBar : MonoBehaviour
{
    public float wifiLevel = 100f;
    public Image wifiImage;
    public MovePlayer setGameOver;


    [Header("Drain Settings")]
    public float drainPerSecond = 1f;  // сколько единиц убывает в секунду
    public bool isDraining = true;
    public float restorePerSecond = 10f;

    [SerializeField] private CalmnessEffectController calmness;

    void Update()
    {
        
        DrainWiFi();
        RestoreWiFi();

        if (wifiLevel <= 0f)
        {
            calmness.SetWifiDrain(true);
             
        }
        else
        {
            calmness.SetWifiDrain(false);
        }

    }

    public void DrainWiFi()
    {
        if (isDraining)
        {
            wifiLevel -= drainPerSecond * Time.deltaTime;
            wifiLevel = Mathf.Clamp(wifiLevel, 0f, 100f);
            UpdateWiFiLevel();
        }
    }

    public void UpdateWiFiLevel()
    {
        wifiImage.fillAmount = wifiLevel / 100f;
    }

    public void RestoreWiFi()
    {
        if (!isDraining)
        {
            wifiLevel += restorePerSecond * Time.deltaTime;
            wifiLevel = Mathf.Clamp(wifiLevel, 0f, 100f);
            UpdateWiFiLevel();
        }
    }
}
