﻿using UnityEngine;
using System.Collections;

public class TrapBoxScript : MonoBehaviour
{
    public float x = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(x * Time.deltaTime, 0f, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        x = x * -1;
    }
}
