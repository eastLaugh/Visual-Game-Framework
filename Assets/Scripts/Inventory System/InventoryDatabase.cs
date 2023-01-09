using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ItemDetails  //记录物品详情
{
    public int itemID;
    public string itemName;
    public string Description;
    public ItemType itemType;
    public Sprite itemIcon;
    public Sprite itemWorldSprite;
    public int physicAttack;
    public int magicAttack;
    public int physicDefence;
    public int magicDefence;
    public int hPRecover;
    public int mpRecover;
    public int sellPrice;
    public int BuyPrice;
}
[System.Serializable]
public class InventoryItem  //
{
    public int itemID;
    public int amount;
}
