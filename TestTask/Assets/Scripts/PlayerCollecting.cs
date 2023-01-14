using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    static public event Action<string> OnCollecting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            OnCollecting?.Invoke(other.tag);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("sign"))
        {
            OnCollecting?.Invoke(other.tag);
            Destroy(other.gameObject);
        }
    }
}
