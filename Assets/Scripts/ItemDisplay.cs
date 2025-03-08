using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{

    public Item itemData;
    public Image iconImage;

    private LoadoutMenu loadoutMenu;

    private void Start()
    {
        loadoutMenu = FindAnyObjectByType<LoadoutMenu>();
        
        iconImage = transform.Find("Icon").GetComponent<Image>();
        
        GetComponent<Button>().onClick.AddListener(OnItemSelected);
    }

    public void SetItem(Item newItem)
    {
        itemData = newItem;
        iconImage.sprite = itemData.itemIcon;

        iconImage.SetNativeSize();
    }

    private void OnItemSelected()
    {
        if (itemData == null || loadoutMenu == null) return;

        if (itemData.isWeapon)
            loadoutMenu.OnWeaponSelected(itemData, itemData.itemIcon);
        else
            loadoutMenu.OnArmorSelected(itemData, itemData.itemIcon);
    }

}
