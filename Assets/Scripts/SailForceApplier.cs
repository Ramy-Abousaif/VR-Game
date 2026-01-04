using UnityEngine;

public class SailForceApplier : MonoBehaviour
{
    public SailableShip ship;
    public Transform sailPlane;
    public float sailPower = 20f;

    void FixedUpdate()
    {
        Vector3 wind = WindManager.instance.direction;

        float alignment = Vector3.Dot(sailPlane.forward, wind.normalized);
        if (alignment <= 0f) return;

        Vector3 force = wind.normalized * sailPower * alignment;
        ship.ship.rb.AddForce(force);
    }
}