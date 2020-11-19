using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class GreenPlant : MonoBehaviour
{
    private Shooter shooter = null;
    private float nextBurst = 0.0f;
    public float burstRate = 0.3f;

    [Header("Projectile")]
    public int projCount = 8;
    public float projAngleOffset = 0.0f;
    public float projSpawnDist = 0.7f;
    public Projectile.Info projectileInfo;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
        nextBurst = Time.time + burstRate;
        projectileInfo.onHit.RemoveListener(OnProjectileHit);
        projectileInfo.onHit.AddListener(OnProjectileHit);
    }

    void Update()
    {
        // shoot something every x seconds
    	if( Time.time >= nextBurst )
        {
            Burst();
        }
    }

    private void Burst()
    {
        shooter.ShootRadial(projectileInfo, projCount, projSpawnDist, projAngleOffset);
        nextBurst = Time.time + burstRate;
    }

    private void OnProjectileHit(Projectile projectile, Collider2D other)
    {
        Debug.Log(projectile.name + " is hitting " + other.gameObject.name);
    }
}
