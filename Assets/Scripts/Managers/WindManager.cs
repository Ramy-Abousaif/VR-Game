using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance { get; private set; }

    public Vector3 direction = new Vector3(1f, 0f, 0f);
}