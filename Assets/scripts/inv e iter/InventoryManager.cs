using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventoryObjects
{
    public string itemName;
    public int quantity;

}

public class InventoryManager : MonoBehaviour
{
    public HashSet<InventoryObjects> inventory = new HashSet<InventoryObjects>();

  
    public void AddItem(string itemName)
    {
        InventoryObjects existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);

        if (existingItem != null)
        {
            existingItem.quantity++;
        }
        else
        {
            InventoryObjects newItem = new InventoryObjects { itemName = itemName, quantity = 1 };
            inventory.Add(newItem);
        }
    }

    public bool HasItem(string itemName)
    {
        InventoryObjects existingItem = inventory.FirstOrDefault(item => item.itemName == itemName);
        return existingItem != null && existingItem.quantity > 0;
    }

   
}
