using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float shotCooldown = 0.5f;
    float lastTimeShot = -1;
    [field: SerializeField] public float Torque { get; set; }
    public float Impulse { get; set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot()
    {
        if (Time.time - lastTimeShot >= shotCooldown)
        {
            //fire
            lastTimeShot = Time.time;
        }
    }
    public void FixedUpdate()
    {
        HandleBoundaries();
        rb.AddForce(transform.up * Impulse);
        transform.Rotate(Vector3.forward, -Torque);
    }
    void HandleBoundaries()
    {
        //handle horizontal
        if (transform.position.x > GlobalConfig.HorizontalBoundary)
        {
            transform.position = new Vector3(-GlobalConfig.HorizontalBoundary, transform.position.y, 0);
        }
        else if (transform.position.x < -GlobalConfig.HorizontalBoundary)
        {
            transform.position = new Vector3(GlobalConfig.HorizontalBoundary, transform.position.y, 0);
        }
        //handle vertical
        if (transform.position.y > GlobalConfig.VerticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, -GlobalConfig.VerticalBoundary, 0);
        }
        else if (transform.position.y < -GlobalConfig.VerticalBoundary)
        {
            transform.position = new Vector3(transform.position.x, GlobalConfig.VerticalBoundary, 0);
        }
    }
}
