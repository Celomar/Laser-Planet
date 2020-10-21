using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform firepoint;
    
    void Start()
    {
        RecalculateLazer();


    }
    void RecalculateLazer()
    {
        // Ray2D ray = new Ray2D (firepoint.position, -transform.up);

        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, -Vector2.up);

        Debug.Log(hit.transform.position);

    }

  
    void Update()
    {
        
    }
}
