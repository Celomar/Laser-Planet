using UnityEngine;

public class Cristal : MonoBehaviour
{
    public Transform firepoint = null;

    [SerializeField] private SpriteRenderer rend = null;
    
    private Cristal nextHorizontalCristal = null;
    private Cristal nextVerticalCristal = null;

    private void Awake()
    {
        FindTargetedObjects();
    }

    private void FindTargetedObjects()
    {
        FindTargetObject(true, out nextHorizontalCristal);
        FindTargetObject(false, out nextVerticalCristal);

        Debug.Log("next horizontal cristal: " + nextHorizontalCristal);
        Debug.Log("next vertical cristal: " + nextVerticalCristal);
    }

    private void FindTargetObject(bool horizontal, out Cristal outCristal)
    {
        Vector2 outputDirection = GetDirection(horizontal);
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, outputDirection);

        // if we hit an object, and that object is not us
        if(hit.collider != null && hit.transform != this.transform)
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
        
        // b: bottom
        // t: top
        // l: left
        // r: right
        if(spriteName.EndsWith("bl"))
        {
            if(horizonal) return new Vector2(-1.0f, 0.0f);
            else return new Vector2(0.0f, -1.0f);
        }
        else if(spriteName.EndsWith("tl"))
        {
            if(horizonal) return new Vector2(-1.0f, 0.0f);
            else return new Vector2(0.0f, 1.0f);
        }
        else if(spriteName.EndsWith("br"))
        {
            if(horizonal) return new Vector2(1.0f, 0.0f);
            else return new Vector2(0.0f, -1.0f);
        }
        else if(spriteName.EndsWith("tr"))
        {
            if(horizonal) return new Vector2(1.0f, 0.0f);
            else return new Vector2(0.0f, 1.0f);
        }
        return Vector2.zero;
    }
}