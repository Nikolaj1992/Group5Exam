using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeverPuzzleScript : MonoBehaviour
{
    private Dictionary<string, GameObject> levers = new Dictionary<string, GameObject>();
    private List<bool> correctCombination = new List<bool>();
    private List<bool> randomLeverStartPosition = new List<bool>();
    
    void Awake()
    {
        for (int i = 0; i < gameObject.transform.Find("Levers").childCount; i++)
        {
            GameObject lever = gameObject.transform.Find("Levers").transform.GetChild(i).gameObject;
            levers.Add(lever.name, lever);
            Debug.Log("added: " + lever.name);
        }
        Debug.Log("lever amount: " + levers.Count);

        for (int i = 0; i < levers.Count; i++)
        {
            correctCombination.Add(Random.Range(0, 2) == 0);
        }
        Debug.Log("C: " + string.Join(", ", correctCombination.Select(x => x.ToString()).ToArray()));
        
        do
        {
            randomLeverStartPosition.Clear();
            for (int i = 0; i < levers.Count; i++)
            {
                randomLeverStartPosition.Add(Random.Range(0, 2) == 0);
            }
        } while (randomLeverStartPosition.SequenceEqual(correctCombination));
        Debug.Log("S: " + string.Join(", ", randomLeverStartPosition.Select(x => x.ToString()).ToArray()));

        // Randomize lever position at the start
        for (int i = 0; i < levers.Count(); i++)
        {
            if (randomLeverStartPosition.ElementAt(i))
            { 
                Animator animator = levers.ElementAt(i).Value.GetComponentInChildren<Animator>(); 
                animator.SetTrigger("LeverPull");
                animator.SetBool("LeverDown", randomLeverStartPosition.ElementAt(i)); 
            }
        }
        
    }
    
    void Update()
    {
        
    }

    void deactivateLevers()
    {
        
    }
}
