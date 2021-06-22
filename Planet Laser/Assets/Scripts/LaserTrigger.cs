using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class LaserTrigger : MonoBehaviour
{
    [System.Serializable]
    public class OnTrigger : UnityEvent<Collider2D,LaserTrigger> {}
    [SerializeField] private OnTrigger onTrigger = null;

    private HashSet<Collider2D> collidersWithinTrigger = new HashSet<Collider2D>();
    private HashSet<Laser> activeLasers = new HashSet<Laser>();

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

    void OnTriggerStay2D(Collider2D other)
    {
        collidersWithinTrigger.Add(other);
        onTrigger?.Invoke(other, this);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        collidersWithinTrigger.Remove(other);
    }

    public void CheckTrigger()
    {
        if(onTrigger == null) return;
        foreach(Collider2D collider in this.collidersWithinTrigger)
        {
            onTrigger.Invoke(collider,this);
        }
    }

    public void AddActiveLaser(Laser laser)
    {
        activeLasers.Add(laser);
    }

    public void RemoveActiveLaser(Laser laser)
    {
        activeLasers.Remove(laser);
    }

    public HashSet<Laser> lasers
    {
        get{ return activeLasers; }
    }
}
