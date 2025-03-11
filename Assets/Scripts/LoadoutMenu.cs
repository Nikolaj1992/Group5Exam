using UnityEngine;

public class LoadoutMenu : MonoBehaviour
{
    
    public LoadoutSlot weaponSlot;
    public LoadoutSlot armorSlot;

    private Item selectedWeapon;
    private Item selectedArmor;

    private GameManager gameManager;
    
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        weaponSlot.OnItemSelect += OnWeaponSelected;
        armorSlot.OnItemSelect += OnArmorSelected;
    }
    
    public void OnWeaponSelected(Item item, Sprite sprite)
    {
        selectedWeapon = item;
        weaponSlot.SetIcon(item.itemIcon);
        
        LoadoutManager.Instance.SetWeapon(item.itemName, item.itemIcon);
    }

    public void OnArmorSelected(Item item, Sprite sprite)
    {
        selectedArmor = item;
        armorSlot.SetIcon(item.itemIcon);
        
        LoadoutManager.Instance.SetArmor(item.itemName, item.itemIcon);
    }

    public void StartGame()
    {
        gameManager.StartNewRound();
        
        PlayerPrefs.SetString("SelectedWeapon", selectedWeapon.itemName);
        PlayerPrefs.SetString("SelectedArmor", selectedArmor.itemName);
    }
    
}
