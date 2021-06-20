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
        this.isShooting = true;
    }

    public void OnInteracted()
    {
        this.isShooting = !this.isShooting;
    }

    public bool isShooting
    {
        get{ return this.laser.canShoot; }
        set
        { 
            this.yellowanimator.SetBool("Awake", value);
            this.laser.canShoot = value; 
        }
    }

}
