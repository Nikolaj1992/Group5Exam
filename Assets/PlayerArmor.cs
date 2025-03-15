using System.Collections.Generic;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    public GameObject armorPrefab;
    private ArmorInfo armorInfo;
    
    // used for the "Bored_Figher" unique
    private Dictionary<GameObject, int> targets = new Dictionary<GameObject, int>();
    
    // TODO: start on the armor, possible rename file or create a new one to handle armor

    private void Awake()
    {
        armorInfo = armorPrefab.transform.GetChild(0).GetComponent<ArmorInfo>();
        Debug.Log(armorInfo.g_unique);
        Debug.Log(armorInfo.g_healthIncrease);
        Debug.Log(armorInfo.g_damageReduction);
    }

    public void DamagePlayer(float damage, GameObject attacker)
    {
        if (armorInfo.g_unique != ArmorInfo.Unique.None)
        {
            switch (armorInfo.g_unique)
            {
                case ArmorInfo.Unique.Rejecting:
                    float reflectedDamage = damage * 0.1f;
                    Debug.Log("Reflecting " + reflectedDamage + " damage to " + attacker.name);
                    break;
                case ArmorInfo.Unique.Bored_Fighter:
                    if (targets.ContainsKey(attacker))
                    {
                        float occurences = targets[attacker];
                        damage = damage * (1 - (occurences/10));
                        Debug.Log("Reducing damage from " + attacker.name + " by " + targets[attacker]*10 + "%");
                        if (occurences < 9) targets[attacker] = targets[attacker] + 1;
                    }
                    else
                    {
                        targets.Add(attacker, 1);
                        Debug.Log("first time seeing " + attacker.name);
                    }
                    break;
            }
            damage *= (1 - (armorInfo.g_damageReduction/100));
            Debug.Log("Q-ARMOR: " + damage);
        }
        else
        {
            damage *= (1 - (armorInfo.g_damageReduction/100));
            Debug.Log("NQ-ARMOR: " + damage);
        }
    }
}
