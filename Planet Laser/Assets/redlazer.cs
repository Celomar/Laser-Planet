using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redlazer : MonoBehaviour
{
    public float lazerTimer = 1.5f;
    public Transform firepoint;
    public LineRenderer lazerrenderer;

    private RaycastHit2D hit;
    private bool lazerOn = false;

    void Start()
    {
        //InvokeRepeating("Lazer", 2, lazerTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (lazerOn)
		{
            hit = Physics2D.Raycast(firepoint.position, -Vector2.up);
            Debug.Log(hit.transform.name);
            Mint hitmint = hit.transform.GetComponent<Mint>();
            if (hitmint != null)
            {
                hitmint.Die();
            }
		}
		else
		{
            StartCoroutine(Lazer());
        }
    }

    IEnumerator Lazer()
    {
        yield return new WaitForSeconds(lazerTimer);

        lazerOn = true;
        lazerrenderer.enabled = true;
        lazerrenderer.SetPosition(0, firepoint.position);
        lazerrenderer.SetPosition(1, hit.point);

        yield return new WaitForSeconds(lazerTimer);

        lazerrenderer.enabled = false;
        lazerOn = false;

    }

}
