using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VGF.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataList_SO itemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO playerBag_SO;
        /// <summary>
        /// 找到物品信息
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>物品信息</returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDataList.Find(i => i.itemID == ID);
        }
        /// <summary>
        /// 把物品放入背包（入口函数）
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <param name="num">物品数量</param>
        /// <returns>是否能放入背包</returns>
        public bool AddItem(int ID,int num)
        {
            int index = GetItemIndexInBag(ID);
            if(!CheckBagCapacity()&&index==-1) return false;
            AddItemAtIndex(ID,index,num);
            EventHandler.CallUpdateInventoryUI(InventoryLocation.player, playerBag_SO.itemList);
            return true;
        }
        public bool SearchItem(int ID,int num)
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                if (playerBag_SO.itemList[i].itemID == ID && playerBag_SO.itemList[i].amount>=num)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 得到物品在背包的索引值
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>物品在背包的索引值</returns>
        private int GetItemIndexInBag(int ID)
        {
            for(int i=0;i<playerBag_SO.itemList.Count;i++)
            {
                if (playerBag_SO.itemList[i].itemID==ID)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 检查背包是否有空位
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>是否有空位</returns>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < playerBag_SO.itemList.Count; i++)
            {
                if (playerBag_SO.itemList[i].itemID == 0)
                    return true;
            }
            return false;
        }
        private void AddItemAtIndex(int ID,int index,int amount)
        {
            if(index==-1)
            {
                var item=new InventoryItem { itemID = ID,amount = amount };
                for(int i=0;i<playerBag_SO.itemList.Count;i++)
                {
                    if(playerBag_SO.itemList[i].itemID==0)
                    {
                        playerBag_SO.itemList[i] = item;
                        break;
                    }
                }
            }
            else
            {
                int currentAmount = playerBag_SO.itemList[index].amount+amount;
                var item=new InventoryItem { itemID = ID, amount = currentAmount };
                playerBag_SO.itemList[index] = item;    
            }
        }
        void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.player, playerBag_SO.itemList);
        }
        void Update()
        {

        }
    }

}
