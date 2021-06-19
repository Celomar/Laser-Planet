using UnityEngine;

public class bluelazer : MonoBehaviour
{
    public Vector2 lazerdirection;
    public Animator blueanimator;

    public Laser laser = null;

    void Start()
    {
        blueanimator.SetFloat("X", lazerdirection.x);
        blueanimator.SetFloat("Y", lazerdirection.y);
        laser.Shoot(lazerdirection);
    }

    // void Update()
    // {
    // }
}

