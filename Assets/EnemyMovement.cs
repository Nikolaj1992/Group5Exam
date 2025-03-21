using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float roamRadius = 10f; // How far the enemy can roam
    public float detectionRange = 5f; // Detection range for player
    public float roamDelay = 3f; // Time before selecting a new roam point
    public float maintainDistance = 2f; // Distance the enemy should maintain from the player
    public LayerMask playerLayer; // Player layer for detection
    
    private NavMeshAgent agent;
    private Transform playerTransform;
    private bool playerDetected = false;
    private Vector3 startPosition;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        StartCoroutine(Roam());
    }
    
    void Update()
    {
        DetectPlayer();
    }
    
    private IEnumerator Roam()
    {
        while (true)
        {
            if (!playerDetected) // Only roam if player is not detected
            {
                Vector3 randomPoint = GetRandomNavMeshPoint();
                agent.SetDestination(randomPoint);
            }
            yield return new WaitForSeconds(roamDelay);
        }
    }
    
    private void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange, playerLayer);
        
        if (hits.Length > 0)
        {
            playerTransform = hits[0].transform;
            playerDetected = true;
            
            // Ensures the enemy always faces the player, even when backing up
            Quaternion rotationToPlayer = Quaternion.LookRotation(playerTransform.position - transform.position);
            transform.rotation = Quaternion.Euler(0, rotationToPlayer.eulerAngles.y, 0);
            
            // Makes the agent maintain its distance
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Vector3 targetPosition = playerTransform.position - directionToPlayer * maintainDistance;
            agent.SetDestination(targetPosition);
        }
        else
        {
            playerDetected = false;
        }
    }
    
    private Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += startPosition;
        
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, roamRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        
        return transform.position; // Stay in place if no valid position is found
    }
    
    private void OnDrawGizmosSelected()
    {
        if (startPosition != Vector3.zero)
        { 
            Gizmos.color = Color.green; 
            Gizmos.DrawWireSphere(startPosition, roamRadius);
        }
        else
        {
            Gizmos.color = Color.green; 
            Gizmos.DrawWireSphere(transform.position, roamRadius);
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}