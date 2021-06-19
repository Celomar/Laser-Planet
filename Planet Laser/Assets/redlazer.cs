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
        _keepShooting = true;
        shootingCoroutine = StartCoroutine(Lazer());
    }

    void Update()
    {
        redanimator.SetBool("Open",laser.canShoot);
    }

    IEnumerator Lazer()
    {
        while(_keepShooting)
        {
            yield return new WaitForSeconds(lazerTimer);
            laser.canShoot = true;
            yield return new WaitForSeconds(lazerTimer);
            laser.canShoot = false;
        }
    }

    public bool keepShooting
    {
        get{ return _keepShooting; }
        set
        {
            _keepShooting = value;
            laser.canShoot = value;

            if(value)
            {
                shootingCoroutine = StartCoroutine(Lazer());
            }
            else
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }
}
