using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend = null;
    [SerializeField] private BoxCollider2D boxCollider = null;
    
    private Cristal nextHorizontalCristal = null;
    private Cristal nextVerticalCristal = null;

    private void Awake()
    {
        if(!boxCollider)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }
        FindTargetedObjects();
    }

    private void FindTargetedObjects()
    {
        FindTargetObject(true, out nextHorizontalCristal);
        FindTargetObject(false, out nextVerticalCristal);

        Debug.Log(transform.name + " hits:\n\thor: " + nextHorizontalCristal + "\n\tvert:" + nextVerticalCristal);
    }

    private void FindTargetObject(bool horizontal, out Cristal outCristal)
    {
        Vector2 outputDirection = GetDirection(horizontal);
        Vector2 firepoint = new Vector2(transform.position.x, transform.position.y) + 
                            (boxCollider.offset * transform.localScale);
        RaycastHit2D hit; 
        
        do{
            hit = Physics2D.Raycast(firepoint, outputDirection);
            firepoint = hit.point + outputDirection * 0.002f;
        } while(hit.transform == this.transform);

        // if we hit an object, and that object is not us
        if(hit.collider != null)
        {
            // try to get a cristal component from the object that was hit
            // outCristal will be null if the hit object does not have a cristal component
            if(hit.transform.TryGetComponent<Cristal>(out outCristal))
                return;
        }
        outCristal = null;
    }
    
    public Vector2 GetDirection(bool horizonal)
    {
        string spriteName = rend.sprite.name;
        
        if(horizonal)
        {
            // local scale x will be 1 when looking to the right
            // and -1 when looking to the left
            // note that this means the sprite should be looking
            // to the right by default
            return new Vector2(transform.localScale.x, 0.0f);
        }

        // b: bottom
        // t: top
        if(spriteName.EndsWith("b"))
        {
            return new Vector2(0.0f, -1.0f);
        }
        else if(spriteName.EndsWith("t"))
        {
            return new Vector2(0.0f, 1.0f);
        }

        return Vector2.zero;
    }
}