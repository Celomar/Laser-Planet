using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend = null;
    [SerializeField] private BoxCollider2D boxCollider = null;
    
    private Cristal nextHorizontalCristal = null;
    private Vector2 horizontalEndpoint = Vector2.zero;
    private Cristal nextVerticalCristal = null;
    private Vector2 verticalEndpoint = Vector2.zero;

    private void Awake()
    {
        if(!boxCollider)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }
        FindTargetedObjects();
    }

    public void HitByRay(Vector2 laserDirection)
    {

    }

    public void GetLaserPoints(Vector2 laserDirection, ref List<Vector3> points)
    {
        points.Add(this.firepoint);
        Vector2 horizontalDir = this.GetDirection(true);
        Vector2 verticalDir = this.GetDirection(false);

        // laser going left
        if(laserDirection.x < 0.0f)
        {
            // mirror pointing right
            if(horizontalDir.x > 0.0f)
            {
                if(nextVerticalCristal)
                {
                    nextVerticalCristal.GetLaserPoints(
                        verticalDir, 
                        ref points);
                }
                else
                {
                    points.Add(verticalEndpoint);
                }
            }
        }
        // laser going right
        else if(laserDirection.x > 0.0f)
        {
            // mirror looking left
            if(horizontalDir.x < 0.0f)
            {
                if(nextVerticalCristal)
                {
                    nextVerticalCristal.GetLaserPoints(verticalDir, ref points);
                }
                else
                {
                    points.Add(verticalEndpoint);
                }
            }
        }
        else if(laserDirection.y < 0.0f)
        {
            if(verticalDir.y > 0.0f)
            {
                if(nextHorizontalCristal)
                {
                    nextHorizontalCristal.GetLaserPoints(horizontalDir, ref points);
                }
                else
                {
                    points.Add(horizontalEndpoint);
                }
            }
        }
        else if(verticalDir.y < 0.0f)
        {
            if(nextHorizontalCristal)
            {
                nextHorizontalCristal.GetLaserPoints(horizontalDir, ref points);
            }
            else
            {
                points.Add(horizontalEndpoint);
            }
        }
    }

    private void FindTargetedObjects()
    {
        FindTargetObject(true, out nextHorizontalCristal);
        FindTargetObject(false, out nextVerticalCristal);
    }

    private void FindTargetObject(bool horizontal, out Cristal outCristal)
    {
        Vector2 outputDirection = GetDirection(horizontal);
        RaycastHit2D hit; 
        
        Vector2 raycastOrigin = this.firepoint;
        do{
            hit = Physics2D.Raycast(raycastOrigin, outputDirection);
            raycastOrigin = hit.point + outputDirection * 0.002f;
        } while(hit.transform == this.transform); // make sure the hit object is not us

        if(horizontal) horizontalEndpoint = hit.point;
        else verticalEndpoint = hit.point;

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
            // note that this means the original sprite should be 
            // looking to the right by default
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

    public Vector2 firepoint
    {
        get
        { 
            // firepoint is at the center of the collider
            return  new Vector2(transform.position.x, transform.position.y) + 
                    (boxCollider.offset * transform.localScale); 
        }
    }
}