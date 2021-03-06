﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDetection : MonoBehaviour
{
    public GameObject door;
    public Animator doorAnimator;
    public Animator batteryAnimator;
    CircleCollider2D doorcollider;

    private bool hitLastframe = false;

    // Start is called before the first frame update
    void Start()
    {
        doorcollider = door.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(doorcollider.enabled == hitLastframe)
		{
            if(doorcollider.enabled == true)
			{
                doorAnimator.SetTrigger("open");
			}
			else
			{
                doorAnimator.SetTrigger("close");
            }
		}

        doorcollider.enabled = !hitLastframe;
        batteryAnimator.SetBool("On", hitLastframe);
        hitLastframe = false;
    }

    void HitByRay()
    {
        hitLastframe = true;
    }
}
