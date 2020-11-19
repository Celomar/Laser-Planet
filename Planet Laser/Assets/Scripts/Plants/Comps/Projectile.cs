using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public const float DIST_ERROR_MARGIN = 0.1f;

    public Info info;

    private Vector3 finalPosition = Vector3.zero;

    public void SetupProjectile(Info info)
    {
        this.info = info;
        // need to normalize para que los calculos
        // donde se usa la dirección salgan as expected
        this.info.direction.Normalize();    
        CalculateFinalPosition();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, info.speed * Time.deltaTime);
        if(isAtFinalPosition) ReachedFinalPosition();
    }

    private void CalculateFinalPosition()
    {
        Vector3 dir = new Vector3(this.info.direction.x, this.info.direction.y, 0.0f);
        finalPosition = transform.position + dir * this.info.maxDistance;
    }

    private bool isAtFinalPosition
    {
        get
        {
            float distSqrFromFinalPosition = (finalPosition - transform.position).sqrMagnitude;
            const float margin = DIST_ERROR_MARGIN * DIST_ERROR_MARGIN;
            return distSqrFromFinalPosition < margin;
        }
    }

    protected virtual void ReachedFinalPosition()
    {
        End();
    }

    protected virtual void End()
    {
        Destroy(this.gameObject);
    }

    [System.Serializable]
    public struct Info
    {
        [HideInInspector]
        public Vector2 direction;
        public float speed;
        public float maxDistance;
    }
}
