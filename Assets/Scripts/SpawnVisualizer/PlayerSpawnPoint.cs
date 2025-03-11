using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        // Just to let us see the actual spawn point
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1f);
    }
    
}
