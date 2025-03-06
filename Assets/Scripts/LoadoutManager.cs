using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    
    public static LoadoutManager Instance;

    public string selectedWeapon = "None";
    public string selectedArmor = "None";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWeapon(string weapon)
    {
        selectedWeapon = weapon;
        Debug.Log("Weapon Selected: " + selectedWeapon);
    }

    public void SetArmor(string armor)
    {
        selectedArmor = armor;
        Debug.Log("Armor Selected: " + selectedArmor);
    }
    
}