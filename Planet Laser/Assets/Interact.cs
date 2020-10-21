using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Transform firepoint;

      void OnTriggerStay2D(Collider2D other)
    {
      
        if (other.tag == "Mint" && Input.GetKeyDown(KeyCode.X))
        {

            RaycastHit2D hit = Physics2D.Raycast(firepoint.position, -Vector2.up);

            Debug.Log(hit.transform.position);
        }
    }
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
