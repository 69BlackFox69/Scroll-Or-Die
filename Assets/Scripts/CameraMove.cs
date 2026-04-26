using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public float height = 3f;      
    public float distance = 6f;    
    public float smoothSpeed = 5f; 

    public float mouseSensitivity = 3f;
    private float pitch = 20f; 
    private float freeCameraYaw = 0f; 
    private bool isFreeLook = false;

    public float pitchMin = 5f;   
    public float pitchMax = 60f;  

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        HandleInput();
        UpdateCameraPosition(isFreeLook ? freeCameraYaw : player.eulerAngles.y);
    }

    void HandleInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        isFreeLook = Input.GetMouseButton(2); 

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        if (isFreeLook)
        {
            freeCameraYaw += mouseX;
        }
        else
        {
            freeCameraYaw = player.eulerAngles.y; 
        }
    }

    void UpdateCameraPosition(float currentYaw)
    {
        Quaternion rotation = Quaternion.Euler(pitch, currentYaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        Vector3 targetPosition = player.position + offset + Vector3.up * height;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(player.position + Vector3.up * 1f);
    }
}
