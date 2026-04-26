using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalmnessEffectController : MonoBehaviour
{
    [Header("Overlay")]
    [SerializeField] private Image redOverlay;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxShakeIntensity = 0.4f;

    [Header("Calmness")]
    [SerializeField] private float maxCalmness = 100f;
    [SerializeField] private float decreaseSpeed = 20f;
    [SerializeField] private float recoverSpeed = 15f;

    public GameObject GameOverUI;
    public GameObject RadUI;
    public GameObject PlayerUI;

    private float currentCalmness;

    private bool isRunning;
    private bool externalDrainWifi;
    private bool externalDrainBatery;

    private Vector3 originalCamPos;

    void Start()
    {
        currentCalmness = maxCalmness;
        originalCamPos = cameraTransform.localPosition;
    }

    void Update()
    {
        HandleCalmness();
        UpdateVisuals();
    }

    // 📞 Вызывается из PlayerRun скрипта
    public void SetRunningState(bool running)
    {
        isRunning = running;
    }

    // 📞 Можно вызвать если другая шкала = 0
    public void SetWifiDrain(bool active)
    {
        externalDrainWifi = active;
    }

    public void SetBateryDrain(bool active)
    {
        externalDrainBatery = active;
    }

    void HandleCalmness()
    {
        // Drain если хотя бы одна шкала на нуле
        bool shouldDrain = isRunning || externalDrainWifi || externalDrainBatery;

        if (shouldDrain)
            currentCalmness -= decreaseSpeed * Time.deltaTime;
        else
            currentCalmness += recoverSpeed * Time.deltaTime;

        currentCalmness = Mathf.Clamp(currentCalmness, 0f, maxCalmness);
    }

    void UpdateVisuals()
    {
        float percent = currentCalmness / maxCalmness;

        // Чем меньше спокойствие — тем больше эффект
        float intensity = 0.8f - percent;

        //  Красный появляется постепенно
        Color c = redOverlay.color;
        c.a = intensity;
        redOverlay.color = c;

        //  Тряска только если ниже 30%
        if (percent <= 0.3f)
        {
            float shakeFactor = (0.3f - percent) / 0.3f;
            float shakeAmount = shakeFactor * maxShakeIntensity;

            cameraTransform.localPosition =
                originalCamPos + Random.insideUnitSphere * shakeAmount;
        }
        else
        {
            cameraTransform.localPosition = originalCamPos;
        }
        GameOver();
    }

    void GameOver()
    {
        if(currentCalmness <= 0f)
        {
            GameOverUI.SetActive(true);
            RadUI.SetActive(false);
            PlayerUI.SetActive(false);
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void AddStres(float amount)
    {
        currentCalmness -= amount;
        currentCalmness = Mathf.Clamp(currentCalmness, 0f, maxCalmness);
    }
}
