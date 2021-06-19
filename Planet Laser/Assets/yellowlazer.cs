﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowlazer : MonoBehaviour
{
    public GameObject Mint;
    public Vector2 lazerdirection;
    public Animator yellowanimator;
    public Laser laser = null;

    void Start()
    {
        yellowanimator.SetFloat("X", lazerdirection.x);
        yellowanimator.SetFloat("Y", lazerdirection.y);
    }

    void Update()
    {
        bool laserOn = laser.canShoot;
        yellowanimator.SetBool("Awake", laserOn);
        if (laserOn)
        {
            laser.Shoot(lazerdirection);
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1.5f);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject == Mint && Input.GetKeyDown(KeyCode.X))
            {
                this.laser.canShoot = !laserOn;
            }
        }
    }

    public bool isShooting
    {
        get{ return this.laser.canShoot; }
        set{ this.laser.canShoot = value; }
    }

}
