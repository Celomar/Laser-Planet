﻿using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LaserTrigger : MonoBehaviour
{
    public void CalculateTransform(Vector2 start, Vector2 end)
    {
        Vector2 laserPosition = (start + end) / 2.0f;
        transform.position = new Vector3(laserPosition.x, laserPosition.y, transform.position.z);
        
        Vector2 differenceVector = end - start;
        Vector2 outputDirection = differenceVector.normalized;
        Vector2 baseScale = new Vector2( 
            1.0f - Mathf.Abs(outputDirection.x), 
            1.0f - Mathf.Abs(outputDirection.y));
        Vector2 scale = differenceVector + baseScale;
        transform.localScale = new Vector3(scale.x, scale.y, 1.0f);
    }
}
