using UnityEngine;

public class OarHydrodynamics : MonoBehaviour
{
    [System.Serializable]
    public class BladePoint
    {
        public Transform transform;
        [HideInInspector] public Vector3 lastPosition;
    }

    [Header("Blade Points")]
    public BladePoint[] bladePoints;

    [Header("Water Interaction")]
    public float maxSubmergeDepth = 0.4f;
    public float dragCoefficient = 1.5f;
    public float minEffectiveSpeed = 0.05f;

    private ShipRigidbody currentShip;

    private Rigidbody oarRB;

    void Start()
    {
        foreach (var p in bladePoints)
            p.lastPosition = p.transform.position;

        oarRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!currentShip) return;

        foreach (var p in bladePoints)
        {
            ApplyWaterForce(p);
        }
    }

    void ApplyWaterForce(BladePoint bladePoint)
    {
        Vector3 pos = bladePoint.transform.position;

        float waterHeight = WaveManager.instance.getHeight(pos.x, pos.z);
        float depth = waterHeight - pos.y;
        if (depth <= 0f) return;

        Vector3 velocity = oarRB.GetPointVelocity(pos);
        float speed = velocity.magnitude;
        if (speed < 0.05f) return;

        Vector3 dragDir = -velocity.normalized;

        float forceMag = speed * speed * dragCoefficient;
        Vector3 force = dragDir * forceMag;

        oarRB.AddForceAtPosition(force, pos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody &&
            other.attachedRigidbody.TryGetComponent(out ShipRigidbody ship))
        {
            currentShip = ship;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentShip &&
            other.attachedRigidbody == currentShip.rb)
        {
            currentShip = null;
        }
    }
}