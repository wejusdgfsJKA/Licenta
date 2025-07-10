using UnityEngine;

[RequireComponent(typeof(Ship))]
public class PlayerInput : MonoBehaviour
{
    Ship ship;
    private void Awake()
    {
        ship = GetComponent<Ship>();
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        ship.Impulse = Input.GetAxis("Vertical");
        ship.Torque = -Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space))
        {
            ship.Shoot();
        }
    }
}
