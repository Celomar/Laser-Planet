using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer = null;
    public Transform firepoint = null;

    private event System.Action<bool> notifyStateChangeToSubscribers = null;

    public void Shoot(Vector2 direction)
    {
        notifyStateChangeToSubscribers?.Invoke(false);
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

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public bool canShoot
    {
        get{ return this.lineRenderer.enabled; }
        set
        { 
            this.lineRenderer.enabled = value; 
            notifyStateChangeToSubscribers?.Invoke(value);
        }
    }

    public void AddSubscriber(System.Action<bool> action)
    {
        notifyStateChangeToSubscribers += action;
    }
}
