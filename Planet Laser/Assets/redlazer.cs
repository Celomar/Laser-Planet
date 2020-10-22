﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redlazer : MonoBehaviour
{
    public float lazerTimer = 1.5f;
    public Transform firepoint;
    public LineRenderer lazerrenderer;

    private RaycastHit2D hit;
    private bool lazerOn = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lazerOn)
		{
            hit = Physics2D.Raycast(firepoint.position, -Vector2.up);
            lazerrenderer.enabled = true;
            lazerrenderer.SetPosition(0, firepoint.position);
            lazerrenderer.SetPosition(1, hit.point);
            Mint hitmint = hit.transform.GetComponent<Mint>();
            if (hitmint != null)
            {
                hitmint.Die();
            }
		}
		else
		{
            StartCoroutine(Lazer());
        }
    }

    IEnumerator Lazer()
    {
        yield return new WaitForSeconds(lazerTimer);

        lazerOn = true;

        yield return new WaitForSeconds(lazerTimer);

        lazerrenderer.enabled = false;
        lazerOn = false;

    }

}
