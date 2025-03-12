using UnityEngine;
using System.Collections.Generic;
using UnityEngine.VFX;

public class BeamShooter : MonoBehaviour
{
    public GameObject muzzlePoint; // The object from which the pellets originate
    public float spreadAngle = 15f; // Max spread angle in degrees
    public float range = 25f;      // Max raycast distance
    
    private List<Vector3> raycastOrigins = new List<Vector3>();
    private List<Vector3> raycastDirections = new List<Vector3>();
    
    [SerializeField] private GameObject beamPrefab;
    [SerializeField] private GameObject impactPrefab;
    
    public void FireShots(int shotAmount)
    {
        raycastOrigins.Clear();
        raycastDirections.Clear();
        
        // Use muzzlePoint position if assigned, otherwise use the script's transform position
        Vector3 origin = muzzlePoint ? muzzlePoint.transform.position : transform.position;
        
        for (int i = 0; i < shotAmount; i++)
        {
            Vector3 direction = GetRandomDirectionInCone(transform.forward, spreadAngle);
            
            raycastOrigins.Add(origin);
            raycastDirections.Add(direction);
            
            if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
            {
                Debug.DrawRay(origin, direction * hit.distance, Color.red, 1.0f);
                GameObject impact = Instantiate(impactPrefab, hit.point, Quaternion.identity) as GameObject;
                Destroy(impact, 1f);
                beamPrefab.GetComponentInChildren<VisualEffect>().SetFloat("YLength", hit.distance);
                GameObject beam = Instantiate(beamPrefab, origin, Quaternion.LookRotation(direction)) as GameObject;
                Destroy(beam, 0.5f);
                beamPrefab.GetComponentInChildren<VisualEffect>().SetFloat("YLength", 25); // 25 is the default
            }
            else
            {
                Debug.DrawRay(origin, direction * range, Color.yellow, 1.0f);
                GameObject beam = Instantiate(beamPrefab, origin, Quaternion.LookRotation(direction)) as GameObject;
                Destroy(beam, 0.5f);
            }
        }
    }
    
    private Vector3 GetRandomDirectionInCone(Vector3 forward, float angle)
    {
        float halfAngle = angle / 2f;
        
        // Random angles in spherical coordinates
        float randomYaw = Random.Range(-halfAngle, halfAngle); // Horizontal spread
        float randomPitch = Random.Range(-halfAngle, halfAngle); // Vertical spread
        
        // Create a rotation from the forward direction
        Quaternion rotation = Quaternion.Euler(randomPitch, randomYaw, 0);
        
        // Apply rotation to forward vector
        return rotation * forward;
    }
    
    // Draw rays in Scene view for debugging
    private void OnDrawGizmos()
    {
        if (raycastOrigins.Count == 0) return;
        
        Gizmos.color = Color.cyan;
        
        for (int i = 0; i < raycastOrigins.Count; i++)
        {
            Gizmos.DrawLine(raycastOrigins[i], raycastOrigins[i] + raycastDirections[i] * range);
        }
    }
}
