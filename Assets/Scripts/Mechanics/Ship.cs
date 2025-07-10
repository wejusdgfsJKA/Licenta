using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float shotCooldown = 0.5f;
    float lastTimeShot = -1;
    float torque;
    public float Torque
    {
        get
        {
            return torque;
        }
        set
        {
            torque = Mathf.Clamp(value, -1, 1);
        }
    }
    float impulse;
    public float Impulse
    {
        get
        {
            return impulse;
        }
        set
        {
            impulse = Mathf.Clamp(value, -1, 1);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = 2;
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }
    private void OnEnable()
    {
        GameManager.Instance.RegisterShip(transform);
    }
    public void Shoot()
    {
        if (Time.time - lastTimeShot >= shotCooldown)
        {
            //fire
            GameManager.Instance.GetBullet(transform);
            lastTimeShot = Time.time;
        }
    }
    public void FixedUpdate()
    {
        HandleBoundaries();
        HandleStars();
        rb.AddForce(transform.up * impulse * GlobalConfig.ImpulseCoefficient, ForceMode2D.Force);
        rb.AddTorque(torque * GlobalConfig.TorqueCoefficient);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //we can only bump into stars, so destroy this ship
        gameObject.SetActive(false);
    }
    void HandleStars()
    {
        for (int i = 0; i < GameManager.Instance.Stars.Count; i++)
        {
            Vector3 dir = GameManager.Instance.Stars[i].position - transform.position;
            rb.AddForce(new Vector2(dir.x, dir.y) * GlobalConfig.StarGravity);
        }
    }
    private void OnDisable()
    {
        GameManager.Instance.ShipDestroyed(transform);
    }
}
