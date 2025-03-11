using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    
    public static LoadoutManager Instance;

    public string selectedWeapon = "None";
    public string selectedArmor = "None";

    public Dictionary<string, Sprite> collectedItems = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // if (transform.parent != null)
            // {
            //     transform.SetParent(null);  // Remove any parent
            // }
            
            DontDestroyOnLoad(gameObject);
            Debug.Log("LoadoutManager Created!");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWeapon(string weaponName, Sprite weaponSprite)
    {
        selectedWeapon = weaponName;
        collectedItems[weaponName] = weaponSprite;
        Debug.Log("Weapon equipped: " + selectedWeapon);
    }

    public void SetArmor(string armorName, Sprite armorSprite)
    {
        selectedArmor = armorName;
        collectedItems[armorName] = armorSprite;
        Debug.Log("Armor equipped: " + selectedArmor);
    }

    public void AddItem(string itemName, Sprite itemSprite)
    {
        if (!collectedItems.ContainsKey(itemName))
        {
            collectedItems.Add(itemName, itemSprite);
            Debug.Log("Collected item: " + itemName);
        }
    }
    
}