using UnityEngine;

public class SailableShip : MonoBehaviour
{
    public ShipRigidbody ship;
    public float maxSpeed = 12f;

    private void Awake()
    {
        if (!ship) ship = GetComponent<ShipRigidbody>();
    }
}