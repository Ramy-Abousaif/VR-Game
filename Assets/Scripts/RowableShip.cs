using UnityEngine;

public class RowableShip : MonoBehaviour
{
    public ShipRigidbody ship;
    public float rowingForceMultiplier = 1f;

    private void Awake()
    {
        if (!ship) ship = GetComponent<ShipRigidbody>();
    }
}