using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float delay;
    void Start()
    {
        Destroy(gameObject, delay);
    }


}
