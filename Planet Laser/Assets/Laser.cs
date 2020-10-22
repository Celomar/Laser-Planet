using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform firepoint;
    public GameObject lazer;
    public LineRenderer lazerrenderer;
    
    void Start()
    {
        RecalculateLazer();


    }
    void RecalculateLazer()
    {
        // Ray2D ray = new Ray2D (firepoint.position, -transform.up);

        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, -Vector2.up);

        Debug.Log(hit.transform.position);
        // Instantiate(lazer, firepoint.position, Quaternion.identity);

        lazerrenderer.SetPosition(0, firepoint.position);
        lazerrenderer.SetPosition(1, hit.point);
        lazerrenderer.SetWidth(2, 2);

    }

  
    void Update()
    {
        
    }
}
