using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeverPuzzleScript : MonoBehaviour
{
    private List<GameObject> levers = new List<GameObject>();
    private List<bool> correctCombination = new List<bool>();
    private List<bool> leverStates = new List<bool>();
    
    void Awake()
    {
        for (int i = 0; i < gameObject.transform.Find("Levers").childCount; i++)
        {
            GameObject lever = gameObject.transform.Find("Levers").transform.GetChild(i).gameObject;
            levers.Add(lever);
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
            leverStates.Clear();
            for (int i = 0; i < levers.Count; i++)
            {
                leverStates.Add(Random.Range(0, 2) == 0);
            }
        } while (leverStates.SequenceEqual(correctCombination));
        Debug.Log("S: " + string.Join(", ", leverStates.Select(x => x.ToString()).ToArray()));

        // Randomize lever states at the start
        for (int i = 0; i < levers.Count(); i++)
        {
            if (leverStates[i])
            { 
                Animator animator = levers[i].GetComponentInChildren<Animator>(); 
                animator.SetTrigger("LeverPull");
                animator.SetBool("LeverDown", leverStates[i]); 
            }
        }
        
    }
    
    void Update()
    {
        
    }

    public void LeverPulled()
    {
        for (int i = 0; i < levers.Count; i++)
        {
            leverStates[i] = levers[i].GetComponentInChildren<Animator>().GetBool("LeverDown");
        }

        if (leverStates.SequenceEqual(correctCombination))
        {
            DeactivateLevers();
            Debug.Log("WON");
        }
    }

    void DeactivateLevers()
    {
        for (int i = 0; i < levers.Count; i++)
        {
            levers[i].layer = LayerMask.NameToLayer("Default");
        }
    }
}
