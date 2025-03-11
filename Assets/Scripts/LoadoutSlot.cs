using UnityEngine;
using UnityEngine.UI;

public class LoadoutSlot : MonoBehaviour
{

    public Image iconImage;
    public delegate void ItemSelectHandler(Item item, Sprite sprite);
    public event ItemSelectHandler OnItemSelect;
    
    public void SelectItem(Item item)
    {
        if (OnItemSelect != null)
        {
            OnItemSelect.Invoke(item, item.itemIcon);
        }
    }
    
    public void SetIcon(Sprite icon)
    {
        iconImage.sprite = icon;
    }

}