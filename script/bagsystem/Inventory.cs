using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public const int SLOTS = 24;
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public void AddItem(IInventoryItem item)
    {
        if (Instruct.ItemList.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }
    public void RemoveItem(IInventoryItem item)
    {
        if (Instruct.ItemList.Contains(item.Name))
        {
            if (ItemRemoved != null)
            {
                this.ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
    internal void DropItem(IInventoryItem item)
    {


        if (Instruct.ItemList.Contains(item.Name))
        {
          

            item.OnDrop();

        }
    }
    internal void UseItem(IInventoryItem item)
    {
        if (Instruct.ItemList.Contains(item.Name))
        {
           

            item.OnUse();
           
        }
    }




}
