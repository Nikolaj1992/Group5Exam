using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapScript : MonoBehaviour
{
    private List<GameObject> mainObjectives = new List<GameObject>();
    private List<GameObject> sideObjectives = new List<GameObject>();

    public int sideObjectiveAmount = 2;
    private GameObject activeMainObjective;
    private List<GameObject> activeSideObjectives = new List<GameObject>();

    private List<Object> sideObjectivePrefabs = new List<Object>();
    private List<Object> mainObjectivePrefabs = new List<Object>();
    private List<Object> activePrefabs = new List<Object>();
    
    void Start()
    {
        for (int i = 0; i < gameObject.transform.Find("Main Objective Areas").childCount; i++)
        {
            mainObjectives.Add(gameObject.transform.Find("Main Objective Areas").GetChild(i).gameObject);
        }
        for (int i = 0; i < gameObject.transform.Find("Side Objective Areas").childCount; i++)
        {
            sideObjectives.Add(gameObject.transform.Find("Side Objective Areas").GetChild(i).gameObject);
        }
        
        sideObjectivePrefabs.AddRange(Resources.LoadAll("Prefabs/SideObjectives"));
        mainObjectivePrefabs.AddRange(Resources.LoadAll("Prefabs/MainObjectives"));
        
        LoadObjectives();
        PlaceRandomMainObjective();
    }

    void LoadObjectives()
    {
        List<int> usedRandomInts = new List<int>();
        List<int> usedRandomPrefabInts = new List<int>();
        
        for (int i = 0; i < sideObjectiveAmount; i++)
        {
            int randomInt;
            int randomPrefabInt;
            do
            {
                randomInt = Random.Range(0, sideObjectives.Count);
            } while (usedRandomInts.Contains(randomInt));
            do
            {
                randomPrefabInt = Random.Range(0, sideObjectivePrefabs.Count);
            } while (usedRandomPrefabInts.Contains(randomPrefabInt));

            usedRandomInts.Add(randomInt);
            usedRandomPrefabInts.Add(randomPrefabInt);
            activeSideObjectives.Add(sideObjectives[randomInt] as GameObject);
            activePrefabs.Add(sideObjectivePrefabs[randomPrefabInt] as GameObject);
        }
        activeMainObjective = mainObjectives[Random.Range(0, mainObjectives.Count)] as GameObject;
        
        if (activeMainObjective != null && activeSideObjectives.Count == sideObjectiveAmount && activePrefabs.Count == sideObjectiveAmount)
        {
            activeMainObjective.GetComponent<Renderer>().material.color = Color.green;
            for (int i = 0; i < activeSideObjectives.Count; i++)
            {
                activeSideObjectives[i].GetComponent<Renderer>().material.color = Color.red;
                Instantiate(activePrefabs[i], activeSideObjectives[i].transform.position, activeSideObjectives[i].transform.rotation);
            }
        }
    }

    void PlaceRandomMainObjective()
    {
        if (mainObjectives.Count < 2)
        {
            Debug.LogWarning("Not enough main objective areas to choose from!");
            return;
        }

        if (mainObjectivePrefabs.Count == 0)
        {
            Debug.LogError("No MainObjective prefabs found in Resources/Prefabs/MainObjectives!");
            return;
        }

        // Picks a random MO location
        int selectedMOIndex = Random.Range(0, mainObjectives.Count);
        activeMainObjective = mainObjectives[selectedMOIndex];

        // Pick a random MainObjective prefab, in case we ever make more than one way to win the game!
        GameObject selectedPrefab = mainObjectivePrefabs[Random.Range(0, mainObjectivePrefabs.Count)] as GameObject;
        if (selectedPrefab == null)
        {
            Debug.LogError("Failed to cast selected MainObjective prefab to GameObject.");
            return;
        }

        // Instantiate
        Vector3 spawnPosition = activeMainObjective.transform.position;
        GameObject spawnedObjective = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // Ensure the object faces the center of the terrain! Should always be obvious that it's a win area!
        Vector3 terrainCenter = new Vector3(0, spawnPosition.y, 0); // Assuming center at (0,0)
        Vector3 directionToCenter = terrainCenter - spawnPosition;
        directionToCenter.y = 0;

        if (directionToCenter != Vector3.zero)
        {
            spawnedObjective.transform.rotation = Quaternion.LookRotation(directionToCenter);
        }
        
        activeMainObjective.GetComponent<Renderer>().material.color = Color.green;
    }

}
