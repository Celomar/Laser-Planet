using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redlazer : MonoBehaviour
{
    public float lazerTimer = 1.5f;
    public Transform firepoint; 
    void Start()
    {
        InvokeRepeating("Lazer", 2, lazerTimer);
    }

    void Lazer()
    {
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, -Vector2.up);

        Debug.Log(hit.transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
