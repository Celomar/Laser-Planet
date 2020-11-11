using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public Transform firepoint;
    public LineRenderer lazerrenderer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void HitByRay(Vector2 lazerdirection)
    {

        Vector2 reflectedLazer = Vector2.zero;
        //Izquierda
        if (lazerdirection.x > 0 && lazerdirection.y == 0) reflectedLazer = -Vector2.up;
        //Derecha
        if (lazerdirection.x < 0 && lazerdirection.y == 0) reflectedLazer = Vector2.up;
        //Arriba
        if (lazerdirection.x == 0 && lazerdirection.y < 0) reflectedLazer = -Vector2.right;
        //Abajo
        if (lazerdirection.x == 0 && lazerdirection.y > 0) reflectedLazer = Vector2.right;
        Debug.Log(reflectedLazer);
       RaycastHit2D hit = Physics2D.Raycast(firepoint.position, reflectedLazer);
        lazerrenderer.enabled = true;
        lazerrenderer.SetPosition(0, firepoint.position);
        lazerrenderer.SetPosition(1, hit.point);
    }    
}

