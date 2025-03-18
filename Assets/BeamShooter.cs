using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class BeamShooter : MonoBehaviour, IAttack
{
    public float spreadAngle = 15f; // Max spread angle in degrees
    public float range = 25f;      // Max raycast distance
    
    private List<Vector3> raycastOrigins = new List<Vector3>();
    private List<Vector3> raycastDirections = new List<Vector3>();
    
    public GameObject beamPrefab;
    public GameObject impactPrefab;
    
    //TODO: move to general attack script
    private List<GameObject> targets = new List<GameObject>();
    
    public void ExecuteAttack(Transform muzzle, int amount)
    {
        // Debug.Log("Shooting " + amount + " beam(s)");
        targets.Clear();
        raycastOrigins.Clear();
        raycastDirections.Clear();
        Vector3 knockbackVector = Vector3.zero;
        
        // Use muzzlePoint position if assigned, otherwise use the script's transform position
        Vector3 origin = muzzle ? muzzle.position : transform.position;

        for (int i = 0; i < amount; i++)
        {
            Vector3 direction;
            if (amount == 1)
            { 
                direction = muzzle.forward;
                knockbackVector = direction;
            }
            else
            {
                direction = GetRandomDirectionInCone(muzzle.forward, spreadAngle);
            }
            
            raycastOrigins.Add(origin);
            raycastDirections.Add(direction);
            
            if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
            {
                // Debug.DrawRay(origin, direction * hit.distance, Color.red, 1.0f);
                
                // impact vfx effect
                GameObject impact = Instantiate(impactPrefab, hit.point, Quaternion.identity) as GameObject;
                Destroy(impact, 1f);
                
                // beam vfx effect
                beamPrefab.GetComponentInChildren<VisualEffect>().SetFloat("YLength", hit.distance);
                GameObject beam = Instantiate(beamPrefab, origin, Quaternion.LookRotation(direction)) as GameObject;
                Destroy(beam, 0.5f);
                beamPrefab.GetComponentInChildren<VisualEffect>().SetFloat("YLength", 25); // 25 is the default
                
                // sphere to visualize the aoe of the impact, radius is set to 1 and hard coded
                Collider[] hitColliders = Physics.OverlapSphere(hit.point, 1);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Enemy") && !targets.Contains(hitCollider.gameObject))
                    {
                        Debug.Log("L-HIT: " + hitCollider.name);
                        targets.Add(hitCollider.gameObject);
                    }
                    
                    // enable the code below to show the hitbox of each beam impact and have the target logged
                    // GameObject sphereP = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    // GameObject sphere = Instantiate(sphereP, hit.point, Quaternion.identity);
                    // Destroy(sphereP);
                    // Destroy(sphere, 1f);
                    // sphere.GetComponent<SphereCollider>().enabled = false;
                    //
                    // sphere.transform.localScale = Vector3.one * 2; // * 1 before the 2 is gone, 1 is the radius
                    //
                    // Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    //
                    // mat.SetFloat("_Surface", 1); // 1 = Transparent
                    // mat.SetFloat("_Blend", 1); // Alpha Blending
                    // mat.SetFloat("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    // mat.SetFloat("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    // mat.SetFloat("_ZWrite", 0); // Disable depth writing for transparency
                    // mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT"); // Enable transparency in URP
                    // mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    //
                    // mat.color = new Color(1f, 0f, 0f, 0.3f); // Red with 30% opacity
                    //
                    // Renderer sphereRenderer = sphere.GetComponent<Renderer>();
                    // sphereRenderer.material = mat;
                    // sphereRenderer.shadowCastingMode = ShadowCastingMode.Off;
                    // sphereRenderer.receiveShadows = false;
                }
            }
            else
            {
                // Debug.DrawRay(origin, direction * range, Color.yellow, 1.0f);
                GameObject beam = Instantiate(beamPrefab, origin, Quaternion.LookRotation(direction)) as GameObject;
                Destroy(beam, 0.5f);
            }
        }

        if (amount > 1)
        {
            foreach (Vector3 position in raycastDirections)
            {
                knockbackVector += position;
            }
            knockbackVector = knockbackVector / raycastDirections.Count;
        }
        knockbackVector = knockbackVector.normalized;
        //TODO: make a general attack script to handle this next part
        if (knockbackVector == Vector3.zero) return;
        PlayerAttack playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        if (playerAttack == null) return;
        playerAttack.ApplyEffectsToTargets(knockbackVector, 4, targets);
        foreach (GameObject target in targets)
        {
            playerAttack.HandleUniqueAndDamageTarget(true, target);
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
    // private void OnDrawGizmos()
    // {
    //     if (raycastOrigins.Count == 0) return;
    //     
    //     Gizmos.color = Color.cyan;
    //     
    //     for (int i = 0; i < raycastOrigins.Count; i++)
    //     {
    //         Gizmos.DrawLine(raycastOrigins[i], raycastOrigins[i] + raycastDirections[i] * range);
    //     }
    // }
}
