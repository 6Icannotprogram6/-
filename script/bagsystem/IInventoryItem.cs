using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IInventoryItem
{
    public string Name { get; }
    public Sprite Image { get; }
    public int Id { get; set; }
    public Transform babat { get; }
    public Transform canvass { get; }
    public GameObject gameob { get; set; }
    public int atk { get; }
    public void OnPickup();
    public void OnDrop();
    public void OnUse();
}
public class InventoryEventArgs : EventArgs
{
    public IInventoryItem Item;
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
}

