using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform firepoint;
    public GameObject lazer;
    public LineRenderer lazerrenderer;

    private RaycastHit2D hit;

    void Start()
    {
        RecalculateLazer();
        lazerrenderer.enabled = true;
        lazerrenderer.SetPosition(0, firepoint.position);
        lazerrenderer.SetPosition(1, hit.point);
    }

    void Update()
    {
        RecalculateLazer();
    }

    void RecalculateLazer()
    {
        hit = Physics2D.Raycast(firepoint.position, -Vector2.up);
        Debug.Log(hit.transform.name);
    }
}

