using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipRigidbody : MonoBehaviour
{
    public Rigidbody rb { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void ApplyForceAtPoint(Vector3 force, Vector3 point)
    {
        rb.AddForceAtPosition(force, point, ForceMode.Force);
    }
}
