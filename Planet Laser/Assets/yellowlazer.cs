using UnityEngine;

public class yellowlazer : MonoBehaviour
{
    public Vector2 lazerdirection;
    public Animator yellowanimator;
    public Laser laser = null;

    void Start()
    {
        yellowanimator.SetFloat("X", lazerdirection.x);
        yellowanimator.SetFloat("Y", lazerdirection.y);
        laser.Shoot(lazerdirection);
    }

    public void OnInteracted()
    {
        Debug.Log("interacted");
        this.laser.canShoot = !this.laser.canShoot;
    }

    public bool isShooting
    {
        get{ return this.laser.canShoot; }
        set{ this.laser.canShoot = value; }
    }

}
