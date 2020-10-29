using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluelazer : MonoBehaviour
{
    public Transform firepoint;
    public LineRenderer lazerrenderer;

    private RaycastHit2D hit;

    void Start()
    {
        hit = Physics2D.Raycast(firepoint.position, -Vector2.up);
        lazerrenderer.enabled = true;
        lazerrenderer.SetPosition(0, firepoint.position);
        lazerrenderer.SetPosition(1, hit.point);
    }

    void Update()
    {
        hit = Physics2D.Raycast(firepoint.position, -Vector2.up);
        if (hit)
        {
            if (hit.transform.tag == "hittable")
            {
                hit.transform.SendMessage("HitByRay");
            }
        }
    }
}

