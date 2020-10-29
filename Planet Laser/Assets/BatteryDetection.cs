﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDetection : MonoBehaviour
{
    public GameObject door;
    CircleCollider2D doorcollider;

    //private bool powerOn = false;

    // Start is called before the first frame update
    void Start()
    {
        doorcollider = door.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        doorcollider.enabled = true;
    }

    void HitByRay()
    {
        doorcollider.enabled = false;
    }
}