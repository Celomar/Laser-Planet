using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend = null;
    [SerializeField] private BoxCollider2D boxCollider = null;
    [SerializeField] private Sprite lookingDownSprite = null;
    [SerializeField] private Sprite lookingUpSprite = null;
    
    // serialize field will make it writeable from the inspector
    // we need it for our crystal editor
    [SerializeField] [HideInInspector]
    private Vector2 _reflectionDirections = new Vector2(1.0f, 0.0f);
    
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
        reflectionDirections = this._reflectionDirections;
    }

    public void HitByRay(Vector2 laserDirection)
    {

    }

    /// <param name="laserDirection">Normalized direction of the laser</param>
    public void GetLaserPoints(Vector2 laserDirection, ref List<Vector3> points)
    {
        points.Add(this.firepoint);
        Vector2 horizontalDir = this.GetDirection(true);
        Vector2 verticalDir = this.GetDirection(false);

        Vector2 redirectedDirection = Vector2.zero;
        Cristal nextCristal = null;
        Vector2 nextPoint = Vector2.zero;

        // if horizontal direction and laser direction are exactly opposite
        if(Vector2.Dot(horizontalDir, laserDirection) == -1.0f)
        {
            nextCristal = nextVerticalCristal;
            redirectedDirection = verticalDir;
            nextPoint = verticalEndpoint;
        }
        else if(Vector2.Dot(verticalDir, laserDirection) == -1.0f)
        {
            nextCristal = nextHorizontalCristal;
            redirectedDirection = horizontalDir;
            nextPoint = horizontalEndpoint;
        }

        if(nextCristal)
        {
            nextCristal.GetLaserPoints(redirectedDirection, ref points);
        }
        // if sqrd mag > 0, it means it's not vector 0 anymore
        // which means at least one of the conditions above passed
        else if(nextPoint.sqrMagnitude > 0.0f)
        {
            points.Add(nextPoint);
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
        return horizonal?
            new Vector2(horizonalDirection, 0.0f) :
            new Vector2(0.0f, verticalDirection);
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

    public Vector2 reflectionDirections
    {
        get{ return _reflectionDirections; }
        set
        {
            horizonalDirection = value.x;
            verticalDirection = value.y;
            FindTargetedObjects();
        }
    }

    private float horizonalDirection
    {
        get{ return _reflectionDirections.x; }
        set
        { 
            _reflectionDirections.x = Mathf.Sign(value); 
            Vector3 scale = transform.localScale;
            scale.x = _reflectionDirections.x;
            transform.localScale = scale;
        }
    }

    private float verticalDirection
    {
        get{ return _reflectionDirections.y; }
        set
        { 
            _reflectionDirections.y = Mathf.Sign(value); 
            rend.sprite = _reflectionDirections.y > 0.0f?
                lookingUpSprite : lookingDownSprite;
        }
    }
}