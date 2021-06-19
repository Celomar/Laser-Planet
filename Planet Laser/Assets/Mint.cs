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

    private bool requestedInteraction = false;

    void Awake()
    {
        InteractionCaller interactionCaller = GetComponentInChildren<InteractionCaller>();
        interactionCaller.wantsToInteract += WantsToInteract;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // puede ser -1 0 1
        movement.y = Input.GetAxisRaw("Vertical");

        if (transform.childCount <= 1)
        {
            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);

            if (movement.sqrMagnitude > 0)
            {
                animator.SetFloat("lasthorizontal", movement.x);
                animator.SetFloat("lastvertical", movement.y);
            }

        }

        animator.SetFloat("speed", movement.sqrMagnitude);

        if (transform.childCount <= 1) //Volver la velocidad normal
        {
            movementSpeed = 5f;
            //Debug.Log("rapido");
        }
        else
        {
            movementSpeed = 1.5f;
        }

    }

    void FixedUpdate ()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime); //el tiempo entre la ultima vez que se llamo la funcion y ahora
        requestedInteraction = Input.GetKeyDown(KeyCode.X);
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

    private bool WantsToInteract()
    {
        return requestedInteraction;
    }
}


