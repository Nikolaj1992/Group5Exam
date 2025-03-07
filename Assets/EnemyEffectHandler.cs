using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    
    void FixedUpdate()
    {
        
    }
    
    public void ApplyKnockback(Vector3 knockbackDirection, float knockbackForce)
    { 
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            rb.AddForce(Vector3.up * (knockbackForce / 2), ForceMode.Impulse);
    }
}
