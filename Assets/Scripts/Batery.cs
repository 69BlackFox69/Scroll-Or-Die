using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batery : MonoBehaviour
{
    public BateryBar batery;
    public float bateryRestoreAmount = 40f;
    // Start is called before the first frame update
    void Start()
    {
        batery = FindObjectOfType<BateryBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            batery.bateryLevel += bateryRestoreAmount;
            batery.UpdatebateryLevel();
            Destroy(gameObject);
        }
    }
}
