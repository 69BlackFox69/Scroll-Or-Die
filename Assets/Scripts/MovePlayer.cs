using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGameOver = false;
    public GameObject GameOverUI;
    public GameObject RadUI;
    public GameObject PlayerUI;



    private bool canJump = false;

    private float yaw = 0f;

    public WiFiBar wiFiBar;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        yaw = transform.eulerAngles.y;
    }

    public LayerMask groundLayer; // Установи в инспекторе
    public float groundCheckDistance = 0.1f;

    void Update()
    {
        if (isGameOver) return;

        // Проверка земли через raycast
        canJump = IsGrounded();

        if (!Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            yaw += mouseX * rotationSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // Проверка из центра капсулы вниз
        float capsuleHeight = GetComponent<CapsuleCollider>().height;
        float capsuleRadius = GetComponent<CapsuleCollider>().radius;

        // Проверяем чуть дальше чем размер коллайдера
        return Physics.Raycast(transform.position, Vector3.down,
                              (capsuleHeight / 3f) + groundCheckDistance,
                              groundLayer);
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * moveY + right * moveX;
        rb.velocity = new Vector3(move.x * speed, rb.velocity.y, move.z * speed);

        Quaternion playerRotation = Quaternion.Euler(0, yaw, 0);
        rb.MoveRotation(playerRotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            setGameOver(true);
        }
    }

    public void setGameOver(bool state)
    {
        isGameOver = state;

        if (isGameOver)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            GameOverUI.SetActive(true);
            RadUI.SetActive(false);
            PlayerUI.SetActive(false);
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wifizone"))
            wiFiBar.isDraining = false; // останавливаем убывание
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("wifizone"))
            wiFiBar.isDraining = true; // возобновляем убывание
    }
}
