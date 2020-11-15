using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileInfo info;

    public void SetupProjectile(ProjectileInfo info)
    {
        this.info = info;
    }

    void FixedUpdate()
    {
        Vector3 translation = new Vector3(
            info.direction.x * info.speed * Time.deltaTime,
            info.direction.y * info.speed * Time.deltaTime,
            0.0f
        );
        transform.Translate(translation);
    }

    public struct ProjectileInfo
    {
        public Vector2 direction;
        public float speed;
    }
}
