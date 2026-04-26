using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    public float floatSpeed = 2f;      // скорость левитации
    public float floatHeight = 0.2f;   // высота колебания
    public float rotationSpeed = 50f;  // скорость вращения

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Левитация вверх-вниз
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Плавное вращение вокруг оси Y
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
