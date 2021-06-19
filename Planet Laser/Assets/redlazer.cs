using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redlazer : MonoBehaviour
{
    public Vector2 lazerdirection;
    public Animator redanimator;
    public Laser laser = null;
    public float lazerTimer = 1.5f;

    private bool _keepShooting = false;
    private Coroutine shootingCoroutine = null;

    void Start()
    {
        redanimator.SetFloat("X", lazerdirection.x);
        redanimator.SetFloat("Y", lazerdirection.y);
        laser.Shoot(lazerdirection);
        keepShooting = true;
    }

    void Update()
    {
        redanimator.SetBool("Open",laser.canShoot);
    }

    IEnumerator Lazer()
    {
        while(_keepShooting)
        {
            laser.canShoot = true;
            yield return new WaitForSeconds(lazerTimer);
            laser.canShoot = false;
            yield return new WaitForSeconds(lazerTimer);
        }
    }

    public bool keepShooting
    {
        get{ return _keepShooting; }
        set
        {
            _keepShooting = value;
            laser.canShoot = value;
            if(shootingCoroutine != null) StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;

            if(value)
            {
                shootingCoroutine = StartCoroutine(Lazer());
            }
        }
    }
}
