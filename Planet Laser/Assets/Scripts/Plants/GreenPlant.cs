using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class GreenPlant : MonoBehaviour
{
    private Shooter shooter = null;
    public Transform firepoint = null;

    [Header("Projectile")]
    public int projCount = 8;
    public float projAngleOffset = 0.0f;
    public float projSpawnDist = 0.7f;
    public Projectile.Info projectileInfo;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
        projectileInfo.onHit.RemoveListener(OnProjectileHit);
        projectileInfo.onHit.AddListener(OnProjectileHit);
    }

    private void Burst()
    {
        shooter.ShootRadial(
            projectileInfo, 
            projCount, 
            projSpawnDist, 
            projAngleOffset, 
            firepoint.position
        );
    }

    private void OnProjectileHit(Projectile projectile, Collider2D other)
    {
        if(other.tag != "hittable" && other.name.ToLower() != "mint") return;
        other.gameObject.SendMessage("HitByRay");
    }
}
