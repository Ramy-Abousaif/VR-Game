using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    public bool active = true;

    private int floaters;
    public Rigidbody rb;

    private void Start()
    {
        if(rb == null)
        {
            rb = GetComponentInParent<Rigidbody>();
        }

        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);

        floaters = transform.parent.childCount;
    }

    private void FixedUpdate()
    {
        if (!active || rb == null)
            return;

        float waterHeight = WaveManager.instance.getHeight(transform.position.x, transform.position.z);

        if (transform.position.y < waterHeight)
        {
            float depth = waterHeight - transform.position.y;

            float displacementMultiplier = Mathf.Clamp01(depth / depthBeforeSubmerged);

            // TOTAL gravity force on boat
            float gravityForce = rb.mass * Mathf.Abs(Physics.gravity.y);

            float buoyancyStrength = 1.6f; // tweak between 1.1–1.6

            float buoyancyPerFloater =
                (gravityForce * buoyancyStrength) / transform.parent.childCount;

            // Final buoyancy force
            float buoyancyForce = buoyancyPerFloater * displacementMultiplier;

            // Damping (vertical only)
            float velocityY = rb.GetPointVelocity(transform.position).y;
            float dampingForce = velocityY * 2f;

            float totalForce = buoyancyForce - dampingForce;

            rb.AddForceAtPosition(
                Vector3.up * totalForce,
                transform.position,
                ForceMode.Force
            );
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
