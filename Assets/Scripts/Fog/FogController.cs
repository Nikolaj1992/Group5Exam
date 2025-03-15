using UnityEngine;

public class FogController : MonoBehaviour
{
   
    private Transform player;
    public GameObject lightFog;
    public GameObject denseFog;
    
    private GameObject lightFogInstance;
    private GameObject denseFogInstance;

    // public float lightFogRadius = 10f; // Radius of Light Fog around our wizard
    // public float denseFogRadius = 20f; // Radius of Dense Fog around our wizard
    
    private void Start()
    {
        GameObject foundPlayer = GameObject.FindWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }
        else
        {
            Debug.LogError("FogManager: No player instance with the tag 'Player' was found in the scene!");
            return;
        }
        
        // Instantiate
        if (lightFog != null)
        {
            lightFogInstance = Instantiate(lightFog, player.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("FogManager: LightFog prefab is not assigned!");
        }

        if (denseFog != null)
        {
            denseFogInstance = Instantiate(denseFog, player.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("FogManager: DenseFog prefab is not assigned!");
        }
    }
    
    void Update()
    {
        if (player == null || lightFogInstance == null || denseFogInstance == null) return;
        
        lightFogInstance.transform.position = player.position;
        denseFogInstance.transform.position = player.position;
        
        // Offset positions outward from the player, to move fog "outward" and not ON the player
        // Vector3 playerPos = player.position;
        //
        // Vector3 lightFogOffset = GetCirclePosition(playerPos, (lightFogRadius + denseFogRadius) / 2f);
        // Vector3 denseFogOffset = GetCirclePosition(playerPos, (denseFogRadius + 40f) / 2f);
        
        // lightFogInstance.transform.position = Vector3.Lerp(lightFogInstance.transform.position, lightFogOffset, Time.deltaTime * 3f);
        // denseFogInstance.transform.position = Vector3.Lerp(denseFogInstance.transform.position, denseFogOffset, Time.deltaTime * 3f);
        
        // Dynamic shape adjustments... hopefully this works as intended
        // AdjustFogSize(lightFogInstance, lightFogRadius);
        // AdjustFogSize(denseFogInstance, denseFogRadius);
    }
    
    // private Vector3 GetCirclePosition(Vector3 center, float radius)
    // {
    //     float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
    //     float x = Mathf.Cos(angle) * radius;
    //     float z = Mathf.Sin(angle) * radius;
    //     return new Vector3(center.x + x, center.y, center.z + z);
    // }
    //
    // private void AdjustFogSize(GameObject fogObject, float radius)
    // {
    //     ParticleSystem fogParticleSystem = fogObject.GetComponentInChildren<ParticleSystem>();
    //     if (fogParticleSystem != null)
    //     {
    //         var shape = fogParticleSystem.shape;
    //         shape.radius = radius;
    //     }
    // }
    //
    // private void OnDrawGizmos()
    // {
    //     if (player == null) return;
    //
    //     // Radius of light fog/mist
    //     Gizmos.color = new Color(1, 0, 0, 0.3f);
    //     Gizmos.DrawWireSphere(player.position, lightFogRadius);
    //
    //     // Radius of dense fog
    //     Gizmos.color = new Color(0,1,0,0.3f);
    //     Gizmos.DrawWireSphere(player.position, denseFogRadius);
    // }
    
}
