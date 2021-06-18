using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer = null;
    public Transform firepoint = null;

    public void Shoot(Vector2 direction)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(firepoint.position);

        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, direction);
        if(hit.collider)
        {
            if(hit.transform.tag == "hittable")
            {
                hit.transform.SendMessage("HitByRay", direction);

                Cristal cristal = null;
                if(hit.transform.TryGetComponent<Cristal>(out cristal))
                {
                    cristal.GetLaserPoints(direction, ref points);
                }
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
}
