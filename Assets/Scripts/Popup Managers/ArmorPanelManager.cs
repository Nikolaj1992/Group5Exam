using UnityEngine;
using UnityEngine.UI;

public class ArmorPanelManager : MonoBehaviour
{
    
    public GameObject armorItemPrefab;
    public Transform armorPanel;
    public Item[] armorItems;
    public LoadoutMenu loadoutMenu;

    private void OnEnable()
    {
        PopulateWeaponPanel();
    }

    public void PopulateWeaponPanel()
    {
        foreach (Transform child in armorPanel)
        {
            Destroy(child.gameObject);
        }

        // Instantiate new buttons for each weapon item
        foreach (var armor in armorItems)
        {
            GameObject newItem = Instantiate(armorItemPrefab, armorPanel);
            ItemDisplay itemDisplay = newItem.GetComponent<ItemDisplay>();
            
            itemDisplay.SetItem(armor);

            newItem.GetComponent<Button>().onClick.AddListener(() => loadoutMenu.OnArmorSelected(armor, armor.itemIcon));
        }
    }
    
}
