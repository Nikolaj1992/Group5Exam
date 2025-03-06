using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadoutSlot : MonoBehaviour
{
    
    public Text selectedItemText; // UI text to display selected item

    public event Action<string> OnItemSelect;

    public void SelectItem(string itemName)
    {
        selectedItemText.text = itemName;
        OnItemSelect?.Invoke(itemName);
    }
    
}