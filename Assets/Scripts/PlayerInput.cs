using UnityEngine;

[RequireComponent(typeof(Ship))]
public class PlayerInput : MonoBehaviour
{
    Ship ship;
    private void Awake()
    {
        ship = GetComponent<Ship>();
    }
    void Update()
    {
        ship.Impulse = Input.GetAxis("Vertical");
        ship.Torque = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space))
        {
            ship.Shoot();
        }
    }
}
