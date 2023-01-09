using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VGF.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("��Ʒ����")]
        public ItemDataList_SO itemDataList_SO;
        [Header("��������")]
        public InventoryBag_SO playerBag_SO;
        /// <summary>
        /// �ҵ���Ʒ��Ϣ
        /// </summary>
        /// <param name="ID">��ƷID</param>
        /// <returns>��Ʒ��Ϣ</returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDataList.Find(i => i.itemID == ID);
        }
        /// <summary>
        /// ����Ʒ���뱳������ں�����
        /// </summary>
        /// <param name="ID">��ƷID</param>
        /// <param name="num">��Ʒ����</param>
        /// <returns>�Ƿ��ܷ��뱳��</returns>
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
        /// �õ���Ʒ�ڱ���������ֵ
        /// </summary>
        /// <param name="ID">��ƷID</param>
        /// <returns>��Ʒ�ڱ���������ֵ</returns>
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
        /// ��鱳���Ƿ��п�λ
        /// </summary>
        /// <param name="ID">��ƷID</param>
        /// <returns>�Ƿ��п�λ</returns>
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
