using UnityEngine;

public class RudderController : MonoBehaviour
{
    public ShipRigidbody ship;
    public Transform rudder;
    public float turnStrength = 5f;

    void FixedUpdate()
    {
        float turnInput = GetSteeringInput();

        Vector3 torque = Vector3.up * turnInput * turnStrength;
        ship.rb.AddTorque(torque);
    }

    // Placeholder for actual input retrieval logic
    float GetSteeringInput()
    {
        return 1.0f;
    }
}