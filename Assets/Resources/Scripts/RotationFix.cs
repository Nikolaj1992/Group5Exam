using UnityEngine;

public class RotationFix : MonoBehaviour
{
    [SerializeField] private Transform attackBone;  // Choose shoulder or upper arm bone
    [SerializeField] private float rotationOffset = -45f;   // Counteract Idle tilt

    private Quaternion originalRotation;

    private void Start()
    {
        if (attackBone != null)
        {
            originalRotation = attackBone.localRotation;
        }
    }

    public void OnAttackStart()
    {
        if (attackBone != null)
        {
            attackBone.localRotation *= Quaternion.Euler(0, rotationOffset, 0);
        }
    }

    public void OnAttackEnd()
    {
        if (attackBone != null)
        {
            attackBone.localRotation = originalRotation;
        }
    }
    
}