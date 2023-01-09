using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VGF.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        [Header("组件获取")]
        [SerializeField] private Image slotImage;
        [SerializeField] private Text amountText;
        public SlotType slotType;
        public int slotIndex;
        private void Start()
        {
            UpdateEmptySlot();
        }

        
        void Update()
        {

        }
        /// <summary>
        /// 让slot更新为空
        /// </summary>
        public void UpdateEmptySlot()
        {
            slotImage.enabled = false;
            amountText.text=string.Empty;
        }
        public void UpdateSlot(ItemDetails item,int amount)
        {
            slotImage.sprite = item.itemIcon;
            amountText.text = amount.ToString();
            slotImage.enabled = true;
        }
    }
}

