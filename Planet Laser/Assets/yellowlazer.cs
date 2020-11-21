using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowlazer : MonoBehaviour
{
    public Transform firepoint;
    public LineRenderer lazerrenderer;
    public GameObject Mint;
    public Vector2 lazerdirection;
    public Animator yellowanimator;
    public bool lazerOn = true;

    private RaycastHit2D hit;
    private RaycastHit2D previoushit;

    void Start()
    {
        yellowanimator.SetFloat("X", lazerdirection.x);
        yellowanimator.SetFloat("Y", lazerdirection.y);
    }

    void Update()
    {
        yellowanimator.SetBool("Awake", lazerOn);
        if (lazerOn)
        {
            hit = Physics2D.Raycast(firepoint.position, lazerdirection);
            lazerrenderer.enabled = true;
            lazerrenderer.SetPosition(0, firepoint.position);

            if (hit)
            {
                if (hit.transform.tag == "hittable")
                {
                    hit.transform.SendMessage("HitByRay", lazerdirection);
                    lazerrenderer.SetPosition(1, previoushit.point);
                }
                else
                {
                    lazerrenderer.SetPosition(1, hit.point);
                }
            }
            previoushit = hit;
        }
		else
		{
			lazerrenderer.enabled = false;
			lazerOn = false;
		}

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1.5f);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject == Mint && Input.GetKeyDown(KeyCode.X))
            {
                lazerOn = !lazerOn;
            }
        }
    }

}
