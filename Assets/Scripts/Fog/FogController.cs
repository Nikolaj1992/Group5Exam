using UnityEngine;

public class FogController : MonoBehaviour
{
   
    private Transform player;
    public GameObject lightFog;
    public GameObject denseFog;
    
    private GameObject lightFogInstance;
    private GameObject denseFogInstance;

    public float lightFogRadius = 10f; // Radius of Light Fog around our wizard
    public float denseFogRadius = 20f; // Radius of Dense Fog around our wizard
    
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
        
        lightFogInstance.transform.position = Vector3.Lerp(lightFogInstance.transform.position, player.position, Time.deltaTime * 5f);
        denseFogInstance.transform.position = Vector3.Lerp(denseFogInstance.transform.position, player.position, Time.deltaTime * 5f);
        
        // lightFogInstance.transform.localScale = Vector3.one * lightFogRadius;
        // denseFogInstance.transform.localScale = Vector3.one * denseFogRadius;
        AdjustFogSize(lightFogInstance, lightFogRadius);
        AdjustFogSize(denseFogInstance, denseFogRadius);
    }
    
    private void AdjustFogSize(GameObject fogObject, float radius)
    {
        ParticleSystem fogParticleSystem = fogObject.GetComponentInChildren<ParticleSystem>();
        if (fogParticleSystem != null)
        {
            var shape = fogParticleSystem.shape;
            shape.radius = radius;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (player == null) return;

        // Radius of light fog/mist
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(player.position, lightFogRadius);

        // Radius of dense fog
        Gizmos.color = new Color(0,1,0,0.3f);
        Gizmos.DrawWireSphere(player.position, denseFogRadius);
    }
    
}
