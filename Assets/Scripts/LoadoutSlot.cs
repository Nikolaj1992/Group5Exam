using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadoutSlot : MonoBehaviour
{

    public Image itemImage;
    public event Action<string, Sprite> OnItemSelect;

    public void SelectItem(string itemName, Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
        OnItemSelect?.Invoke(itemName, itemSprite);
    }
    
}