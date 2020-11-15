using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class GreenPlant : MonoBehaviour
{
    private Shooter shooter = null;
    private float nextBurst = 0.0f;
    public float burstRate = 0.3f;

    public float projSpeed = 10.0f;
    public int projCount = 8;
    public float projAngleOffset = 0.0f;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
        nextBurst = Time.time + burstRate;
        Debug.Log(Time.time);
    }

    void Update()
    {
        // shoot something every x seconds
    	if( Time.time >= nextBurst )
        {
            Burst();
        }
    }

    private void Burst()
    {
        shooter.ShootRadial(projSpeed, projCount, 0.7f, projAngleOffset);
        nextBurst = Time.time + burstRate;
    }
}
