using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Projectile projectilePrefab = null;

    public Projectile SpawnProjectile(Vector2 firepoint, float rotation)
    {
        Projectile proj = Instantiate<Projectile>(
            projectilePrefab,
            new Vector3(firepoint.x, firepoint.y, 0.0f),
            Quaternion.AngleAxis(rotation, Vector3.forward)
        );
        return proj;
    }

    public Projectile Shoot(Vector2 firepoint, Projectile.Info projectileInfo)
    {
        Projectile proj = SpawnProjectile(
            firepoint, 
            Mathf.Atan2(projectileInfo.direction.y, projectileInfo.direction.x)
        );

        proj.SetupProjectile(projectileInfo);

        return proj;
    }

    public Projectile[] ShootRadial(Projectile.Info projectileInfo, int count, float radius, float angleOffset)
    {
        Projectile[] projectiles = new Projectile[count];

        float angleIncrement = 360.0f / (float)count;
        for(int i = 0; i < count; i++)
        {
            Projectile.Info currentInfo = projectileInfo;
            float angle = angleIncrement * (float)i + angleOffset;
            currentInfo.direction = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
            Vector2 firepoint = currentInfo.direction * radius;

            projectiles[i] = this.Shoot(firepoint, currentInfo);
        }

        return projectiles;
    }
}
