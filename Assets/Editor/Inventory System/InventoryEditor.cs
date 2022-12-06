using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;

public class InventoryEditor : EditorWindow
{
    private ItemDataList_SO database;
    private List<ItemDetails> itemList = new List<ItemDetails>();
    private VisualTreeAsset itemRowTemplate;
    //获得VisualElement
    private ListView itemListView;
    private ScrollView itemDetailsSection;
    private Sprite defaultIcon;
    private ItemDetails activeItem;
    private VisualElement iconPreview;
    [MenuItem("Editor/InventoryEditor")]
    public static void ShowExample()
    {
        InventoryEditor wnd = GetWindow<InventoryEditor>();
        wnd.titleContent = new GUIContent("InventoryEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        /*VisualElement label = new Label("Hello World! From C#");
        root.Add(label);*/

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Inventory System/InventoryEditor.uxml");
        VisualElement mainTemplate = visualTree.Instantiate();
        root.Add(mainTemplate);
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Inventory System/ItemRowTemplate.uxml");
        //拿到默认Icon图片
        defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/M Studio/Art/Items/Icons/icon_M.png");
        //变量赋值
        itemListView = root.Q<VisualElement>("BackGround").Q<ListView>("ItemList");
        itemDetailsSection = root.Q<ScrollView>("ItemDetails");
        iconPreview = itemDetailsSection.Q<VisualElement>("IconView");
        //获得按键
        root.Q<Button>("AddButton").clicked += OnAddItemClicked;
        root.Q<Button>("DeleteButton").clicked += OnDeleteClicked;
        //加载数据
        LoadDataBase();
        //生成ListView
        GenerateListView();
        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        /*var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Inventory System/InventoryEditor.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        root.Add(labelWithStyle);*/
    }
    #region 按键事件
    private void OnDeleteClicked()
    {
        itemList.Remove(activeItem);
        itemListView.Rebuild();
        itemDetailsSection.visible = false;

    }
    private void OnAddItemClicked()
    {
        ItemDetails newItem = new ItemDetails();
        newItem.itemName = "NEW ITEM";
        newItem.itemID = 1000 + itemList.Count;
        newItem.itemIcon = defaultIcon;
        itemList.Add(newItem);
        itemListView.Rebuild();
    }
    #endregion
    private void LoadDataBase()//加载数据
    {
        var dataArray = AssetDatabase.FindAssets("ItemDataList_SO");
        if (dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            database = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;
        }
        itemList = database.itemDataList;
        //如果不标记则无法记录数据
        EditorUtility.SetDirty(database);
        Debug.Log(itemList[0].itemID);
    }
    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < itemList.Count)
            {
                if (itemList[i].itemIcon != null)
                    e.Q<VisualElement>("List_Icon").style.backgroundImage = itemList[i].itemIcon.texture;
                e.Q<Label>("List_Name").text = itemList[i] == null ? "NO ITEM" : itemList[i].itemName;
            }
        };
        Debug.Log(itemList[0].itemID);
        itemListView.itemsSource = itemList;
        itemListView.makeItem = makeItem;
        itemListView.bindItem = bindItem;
        itemListView.onSelectionChange += OnlistSelectiontChange;
        //右侧信息面板不可见
        itemDetailsSection.visible = false;
    }
    private void OnlistSelectiontChange(IEnumerable<object> selectedItem)
    {
        activeItem = (ItemDetails)selectedItem.First();
        GetItemDetails();
        itemDetailsSection.visible = true;
    }
    private void GetItemDetails()
    {
        itemDetailsSection.MarkDirtyRepaint();
        itemDetailsSection.Q<IntegerField>("itemID").value = activeItem.itemID;
        itemDetailsSection.Q<IntegerField>("itemID").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemID = evt.newValue;
        });
        itemDetailsSection.Q<TextField>("itemName").value = activeItem.itemName;
        itemDetailsSection.Q<TextField>("itemName").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemName = evt.newValue;
            itemListView.Rebuild();
        });
        iconPreview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        /*itemDetailsSection.Q<ObjectField>("itemIcon").value = activeItem.itemIcon;
        itemDetailsSection.Q<ObjectField>("itemIcon").RegisterValueChangedCallback(evt =>
        {
            Sprite newIcon = evt.newValue as Sprite;
            activeItem.itemIcon = newIcon;
            iconPreview.style.backgroundImage = newIcon == null ? defaultIcon.texture : newIcon.texture;
            itemListView.Rebuild();
        });*/
        //其他变量绑定
        /*itemDetailsSection.Q<ObjectField>("itemSprite").value = activeItem.itemOnWorldSprite;
        itemDetailsSection.Q<ObjectField>("itemSprite").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemOnWorldSprite = (Sprite)evt.newValue;
        });*/

        itemDetailsSection.Q<EnumField>("itemType").Init(activeItem.itemType);
        itemDetailsSection.Q<EnumField>("itemType").value = activeItem.itemType;
        itemDetailsSection.Q<EnumField>("itemType").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemType = (ItemType)evt.newValue;
        });

        itemDetailsSection.Q<TextField>("Description").value = activeItem.Description;
        itemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            activeItem.Description = evt.newValue;
        });

        /*itemDetailsSection.Q<IntegerField>("itemUseRadius").value = activeItem.itemUseRadius;
        itemDetailsSection.Q<IntegerField>("itemUseRadius").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemUseRadius = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanPickedup").value = activeItem.canPickedup;
        itemDetailsSection.Q<Toggle>("CanPickedup").RegisterValueChangedCallback(evt =>
        {
            activeItem.canPickedup = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanDropped").value = activeItem.canDropped;
        itemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            activeItem.canDropped = evt.newValue;
        });

        itemDetailsSection.Q<Toggle>("CanCarried").value = activeItem.canCarried;
        itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            activeItem.canCarried = evt.newValue;
        });

        itemDetailsSection.Q<IntegerField>("Price").value = activeItem.itemPrice;
        itemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemPrice = evt.newValue;
        });

        itemDetailsSection.Q<Slider>("SellPercentage").value = activeItem.sellPercentage;
        itemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            activeItem.sellPercentage = evt.newValue;
        });*/
    }
}
