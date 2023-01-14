using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private enum Type
    {
        Left,
        Right, 
        Finish
    }

    [SerializeField] private Type _type;
    public static event Action<string> OnTurn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            OnTurn?.Invoke(_type.ToString());
        }
    }
}
