using UnityEngine;

public class ArmStretch : MonoBehaviour
{
    [System.Serializable]
    public class ArmData
    {
        public Transform upperArm;
        public Transform lowerArm;
        public Transform hand;
        public Transform target;

        [HideInInspector] public float armLength;
        [HideInInspector] public Vector3 upperStartScale;
        [HideInInspector] public Vector3 lowerStartScale;
    }

    public ArmData leftArm;
    public ArmData rightArm;

    [Header("Stretch Settings")]
    public float maxStretch = 1.1f; // max 10% stretch
    public float stretchStartThreshold = 0.95f; // start stretching at 95% extension

    void Start()
    {
        InitArm(leftArm);
        InitArm(rightArm);
    }

    void InitArm(ArmData arm)
    {
        arm.armLength =
            Vector3.Distance(arm.upperArm.position, arm.lowerArm.position) +
            Vector3.Distance(arm.lowerArm.position, arm.hand.position);

        arm.upperStartScale = arm.upperArm.localScale;
        arm.lowerStartScale = arm.lowerArm.localScale;
    }

    void LateUpdate()
    {
        ApplyOverride(leftArm);
        ApplyOverride(rightArm);
    }

    void ApplyOverride(ArmData arm)
    {
        // 1. Hand follows controller exactly
        arm.hand.position = arm.target.position;
        arm.hand.rotation = arm.target.rotation;

        // 2. Stretch based purely on distance
        float distance = Vector3.Distance(arm.upperArm.position, arm.hand.position);
        float stretchRatio = distance / arm.armLength;

        float appliedStretch = 1f;

        if (stretchRatio > stretchStartThreshold)
        {
            float t = Mathf.InverseLerp(stretchStartThreshold, 1f, stretchRatio);
            appliedStretch = Mathf.Lerp(1f, maxStretch, t);
        }

        // Apply stretch instantly (distance-driven)
        Vector3 upperScale = arm.upperStartScale;
        Vector3 lowerScale = arm.lowerStartScale;

        // Depends on forwards axis of the arm model, in this case Y
        upperScale.y *= appliedStretch;
        lowerScale.y *= appliedStretch;

        arm.upperArm.localScale = upperScale;
        arm.lowerArm.localScale = lowerScale;
    }
}
