using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redlazer : MonoBehaviour
{
    public Vector2 lazerdirection;
    public Animator redanimator;
    public Laser laser = null;
    public float lazerTimer = 1.5f;
    
    private bool lazerOn = false;

    void Start()
    {
        redanimator.SetFloat("X", lazerdirection.x);
        redanimator.SetFloat("Y", lazerdirection.y);
    }

    // Update is called once per frame
    void Update()
    {
        redanimator.SetBool("Open",lazerOn);
        if (lazerOn)
		{
            // hit = Physics2D.Raycast(firepoint.position, lazerdirection);
            // lazerrenderer.enabled = true;
            // lazerrenderer.SetPosition(0, firepoint.position);
            // lazerrenderer.SetPosition(1, hit.point);

            // if (hit)
            // {
            //     if (hit.transform.tag == "hittable")
            //     {
            //         hit.transform.SendMessage("HitByRay",lazerdirection);
            //         lazerrenderer.SetPosition(1, previoushit.point);
            //     }
            //     else
            //     {
            //         lazerrenderer.SetPosition(1, hit.point);
            //     }
            // }
            // previoushit = hit;
            laser.Shoot(lazerdirection);
        }
		else
		{
            StartCoroutine(Lazer());
        }
    }

    IEnumerator Lazer()
    {
        yield return new WaitForSeconds(lazerTimer);
        lazerOn = laser.canShoot = true;
        yield return new WaitForSeconds(lazerTimer);
        lazerOn = laser.canShoot = false;

    }

}
