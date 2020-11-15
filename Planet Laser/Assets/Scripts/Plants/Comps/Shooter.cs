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

    public Projectile Shoot(Vector2 firepoint, float speed, Vector2 direction)
    {
        Projectile proj = SpawnProjectile(
            firepoint, 
            Mathf.Atan2(direction.y, direction.x)
        );

        Projectile.ProjectileInfo info = new Projectile.ProjectileInfo();
        info.speed = speed;
        info.direction = direction;

        proj.SetupProjectile(info);

        return proj;
    }

    public Projectile[] ShootRadial(float speed, int count, float radius, float angleOffset)
    {
        Projectile[] projectiles = new Projectile[count];

        float angleIncrement = 360.0f / (float)count;
        for(int i = 0; i < count; i++)
        {
            float angle = angleIncrement * (float)i + angleOffset;
            Vector2 direction = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );
            Vector2 firepoint = direction * radius;

            projectiles[i] = this.Shoot(firepoint, speed, direction);
        }

        return projectiles;
    }
}
