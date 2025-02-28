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

    private List<Object> prefabs = new List<Object>();
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
        
        prefabs.AddRange(Resources.LoadAll("Prefabs/SideObjectives"));
        
        LoadObjectives();
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
                randomPrefabInt = Random.Range(0, prefabs.Count);
            } while (usedRandomPrefabInts.Contains(randomPrefabInt));

            usedRandomInts.Add(randomInt);
            usedRandomPrefabInts.Add(randomPrefabInt);
            activeSideObjectives.Add(sideObjectives[randomInt] as GameObject);
            activePrefabs.Add(prefabs[randomPrefabInt] as GameObject);
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
}
