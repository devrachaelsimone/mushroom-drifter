using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
   [SerializeField] float timeTilDestroy = 1f;
    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, timeTilDestroy);
    }
}
