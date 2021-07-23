using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRandTime : MonoBehaviour
{
    [SerializeField] private float min = 4;
    [SerializeField] private float max = 7;

    void Start()
    {
        Destroy(gameObject,Random.Range(min,max));
    }

    
}
