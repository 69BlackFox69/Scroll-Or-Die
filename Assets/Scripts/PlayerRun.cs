using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    public GameObject idleModel;
    public GameObject runModel;
    private MovePlayer move;

    [SerializeField] private CalmnessEffectController calmness;

    void Start()
    {
        move = GetComponent<MovePlayer>();
    }
    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        move.speed = isRunning ? 9f : 5f;
        calmness.SetRunningState(isRunning);

        idleModel.SetActive(!isRunning);
        runModel.SetActive(isRunning);
    }
}
