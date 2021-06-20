using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer = null;
    public Transform firepoint = null;

    private event System.Action<Laser,bool> notifyStateChangeToSubscribers = null;
    
    public LaserTrigger laserTrigger = null;
    private Vector2 currentDirection = Vector2.zero;

    public void Shoot(Vector2 direction)
    {
        if(direction == Vector2.zero)
            direction = currentDirection;
        currentDirection = direction;

        notifyStateChangeToSubscribers?.Invoke(this,false);
        notifyStateChangeToSubscribers = null;

        List<Vector3> points = new List<Vector3>();
        points.Add(firepoint.position);

        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, direction);
        
        if(hit.collider)
        {
            Cristal cristal = null;
            if(hit.collider.gameObject.tag == "hittable")
            {
                hit.collider.gameObject.SendMessage("HitByRay", direction);
            }
            else if(hit.transform.TryGetComponent<Cristal>(out cristal))
            {
                cristal.GetLaserPoints(direction, ref points, this);
            }
            else
            {
                points.Add(hit.point);
            }
        }
        else
        {
            points.Add(hit.point);
        }

        laserTrigger.CalculateTransform(firepoint.position, hit.point);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public bool canShoot
    {
        get{ return this.lineRenderer.enabled; }
        set
        { 
            this.lineRenderer.enabled = value; 
            notifyStateChangeToSubscribers?.Invoke(this,value);
        }
    }

    public void AddSubscriber(System.Action<Laser,bool> action)
    {
        notifyStateChangeToSubscribers += action;
    }

    public void OnLaserTrigger(Collider2D other, LaserTrigger trigger)
    {
        if(canShoot && other.tag == "hittable")
        {
            Vector2 laserDirection = (trigger.transform.position - firepoint.position).normalized;
            other.gameObject.SendMessage("HitByRay", laserDirection);
        }
    }
}
