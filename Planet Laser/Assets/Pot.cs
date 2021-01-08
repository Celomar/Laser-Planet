using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null && Input.GetButtonUp("Pick up"))
        {
            this.transform.SetParent(null);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        GameObject pot;
        Transform astronauta;
        Vector3 localPosition= new Vector3(1.008f,1.008f,0);

        pot = this.gameObject;
        astronauta = other.transform; //Referencia al game object

        Vector3 potPosition = pot.transform.position;
        Vector3 astPosition=other.transform.position;
        Vector3 direction = potPosition - astPosition;

        localPosition = Vector3.Scale(localPosition,direction);

        

        if (Input.GetButton("Pick up"))
        {
            if (transform.parent == null)
            {
                pot.transform.SetParent(astronauta, true);
                pot.transform.localPosition = localPosition;
            }
        }
        
    }
}
