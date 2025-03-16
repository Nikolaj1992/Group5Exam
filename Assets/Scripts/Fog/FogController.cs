using UnityEngine;

public class FogController : MonoBehaviour
{
   
    private Transform player;
    public GameObject lightFog;
    public GameObject denseFog;
    
    private GameObject lightFogInstance;
    private GameObject denseFogInstance;
    
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
        
        // Should make the fog have a slightly realistic drift to it
        Vector3 jitterOffset = new Vector3(
            Mathf.PerlinNoise(Time.time * 0.5f, 0f) * 0.5f - 0.25f,
            0f, 
            Mathf.PerlinNoise(0f, Time.time * 0.5f) * 0.5f - 0.25f
        );
        
        Vector3 targetPosition = player.position + jitterOffset;

        lightFogInstance.transform.position = targetPosition;
        denseFogInstance.transform.position = targetPosition;
        
        // Rotation effect
        lightFogInstance.transform.Rotate(Vector3.up, Time.deltaTime * 5f);
        denseFogInstance.transform.Rotate(Vector3.up, Time.deltaTime * 3f);
    }
    
}
