using UnityEngine;
using UnityEngine.UI;

public class WeaponPanelManager : MonoBehaviour
{
    
    public GameObject weaponItemPrefab;
    public Transform weaponPanel;
    public Item[] weaponItems;
    public LoadoutMenu loadoutMenu;

    private void OnEnable()
    {
        PopulateWeaponPanel();
    }

    public void PopulateWeaponPanel()
    {
        foreach (Transform child in weaponPanel)
        {
            Destroy(child.gameObject);
        }

        // Instantiate new buttons for each weapon item
        foreach (var weapon in weaponItems)
        {
            GameObject newItem = Instantiate(weaponItemPrefab, weaponPanel);
            ItemDisplay itemDisplay = newItem.GetComponent<ItemDisplay>();
            
            itemDisplay.SetItem(weapon);

            newItem.GetComponent<Button>().onClick.AddListener(() => loadoutMenu.OnWeaponSelected(weapon, weapon.itemIcon));
        }
    }
    
}