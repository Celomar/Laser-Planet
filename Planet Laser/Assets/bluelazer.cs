using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluelazer : MonoBehaviour
{
    public Transform firepoint;
    public LineRenderer lazerrenderer;
    public Vector2 lazerdirection;
    public Animator blueanimator;

    private RaycastHit2D hit;
    private RaycastHit2D previoushit;

    void Start()
    {
        blueanimator.SetFloat("X", lazerdirection.x);
        blueanimator.SetFloat("Y", lazerdirection.y);
        lazerrenderer.enabled = true;
        lazerrenderer.SetPosition(0, firepoint.position);
    }

    void Update()
    {
        hit = Physics2D.Raycast(firepoint.position, lazerdirection);
        if (hit)
        {
            if (hit.transform.tag == "hittable")
            {
                // hit.transform.SendMessage("HitByRay",lazerdirection);
                lazerrenderer.SetPosition(1, previoushit.point);
            }
            else
            {
                lazerrenderer.SetPosition(1, hit.point);
            }
        }
        previoushit = hit;
    }
}

