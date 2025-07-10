using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform Parent { get; set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }
    private void OnEnable()
    {
        rb.velocity = transform.up * GlobalConfig.BulletSpeed;
    }
    private void Update()
    {
        HandleBoundaries();
    }
    void HandleBoundaries()
    {
        if (transform.position.x >= Mathf.Abs(GlobalConfig.HorizontalBoundary) ||
            transform.position.y >= Mathf.Abs(GlobalConfig.VerticalBoundary))
        {
            transform.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Star")
        {
            transform.gameObject.SetActive(false);
            return;
        }
        if (other.transform.root != Parent)
        {
            other.gameObject.SetActive(false);
            transform.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        Parent = null;
        BulletManager.Instance.AddBulletToPool(this);
    }
}
