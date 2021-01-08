using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mint : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    public Transform startingpoint;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        if(movement.sqrMagnitude > 0)
		{
            animator.SetFloat("lasthorizontal", movement.x);
            animator.SetFloat("lastvertical", movement.y);
        }

        if (transform.childCount == 0) //Volver la velocidad normal
        {
            movementSpeed = 5f;
            //Debug.Log("rapido");
        }
    }

    void FixedUpdate ()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    } 

    public void Die()
	{
        //Instantiate(deatheffect, transform.position, Quaternion.identity);
        transform.position = startingpoint.position;
	}
 
    void HitByRay()
	{
        Die();
	}

    private void OnTriggerStay2D(Collider2D other) //Para disminuir la velocidad
    {
        if (Input.GetButton("Pick up"))
        {
            movementSpeed = 2.5f;
            //Debug.Log("Lento");
        }
    }
}


