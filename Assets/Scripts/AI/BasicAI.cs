using UnityEngine;

[RequireComponent(typeof(Ship))]
public class BasicAI : MonoBehaviour
{
    [SerializeField] Transform target;
    Ship ship;
    private void Awake()
    {
        ship = GetComponent<Ship>();
    }
    private void OnEnable()
    {
        AcquireTarget();
    }
    void AcquireTarget()
    {
        foreach (var t in ShipManager.Instance.Ships)
        {
            if (t != transform)
            {
                target = t;
                break;
            }
        }
    }
    private void Update()
    {
        ship.Shoot();
        HandleMovement();
    }
    void HandleMovement()
    {
        var dir = GameManager.Instance.Stars[0].position - transform.position;
        var ToStar = dir.normalized;
        var rightDot = Vector3.Dot(ToStar, transform.right);
        if (rightDot >= 0)
        {
            rightDot = .5f;
        }
        else
        {
            rightDot = -.5f;
        }
        ship.Torque = rightDot;
        ship.Impulse = 1;
        //var forwardDot = Vector3.Dot(ToStar, transform.forward);
        //if (forwardDot >= 0)
        //{
        //    forwardDot = 1;
        //}
        //else
        //{
        //    forwardDot = -1;
        //}
        //ship.Impulse = forwardDot;
    }
}
