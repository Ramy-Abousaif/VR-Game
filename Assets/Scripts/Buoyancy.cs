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

        floaters = transform.parent.childCount;

        if (rb.useGravity)
            rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (rb == null)
            return;

        // Manual gravity subdivided based on the amount of floaters.
        rb.AddForceAtPosition(Physics.gravity * floaters, transform.position, ForceMode.Acceleration);

        if (!active)
            return;

        float wH = WaveManager.instance.getHeight(transform.position.x, transform.position.z);

        // If the floater is below water
        if (transform.position.y < wH)
        {
            float displacementMultiplier = Mathf.Clamp01((wH - transform.position.y) / depthBeforeSubmerged) * displacementAmount;

            // Buoyant Force
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementAmount * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);

            // Drag Force
            rb.AddForce(displacementMultiplier * -rb.linearVelocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);

            // Torque Force
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
