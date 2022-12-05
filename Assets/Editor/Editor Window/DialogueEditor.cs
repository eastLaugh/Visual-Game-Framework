using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;

public class DialogueEditor : EditorWindow
{
    private VisualTreeAsset sectionRowTemplate;
    private VisualTreeAsset detailsRowTemplate;
    private List<DialoguePiece>  detailsList=new List<DialoguePiece>();
    private Dialogue_SO database;
    private ListView detailsView;
    private ListView sectionView;
    private DialoguePiece activeDetails;
    private Dialogue_SO activeSections;
    private List<Dialogue_SO> dialogueDatabase=new List<Dialogue_SO>();
    private List<string> nameArray=new List<string>();
    [MenuItem("Editor/DialogueEditor")]
    public static void ShowExample()
    {
        DialogueEditor wnd = GetWindow<DialogueEditor>();
        wnd.titleContent = new GUIContent("DialogueEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        
     

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Editor Window/DialogueEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        //�����б��ز�
        sectionRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Editor Window/SectionRowTemplate.uxml");
        detailsRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Editor Window/DetailsRowTemplate.uxml");
        //������ֵ
        detailsView = root.Q<VisualElement>("SectionDetails").Q<ListView>("DetailsView");//
        sectionView = root.Q<VisualElement>("SectionList").Q<ListView>("SectionView");
        //���¼�
        root.Q<Button>("AddPieces").clicked += OnAddPiecesClicked;
        root.Q<Button>("DeletePieces").clicked += OnDeletePiecesClicked;
        LoadDataBase();
        //GenerateDetailsView();
    }
    private void OnAddPiecesClicked()
    {
        DialoguePiece newPieces = new DialoguePiece();
        newPieces.Scaler = 100f;
        newPieces.Content = "input content";
        newPieces.Append = false;
        //newPieces.characterName = "input name";
        detailsList.Add(newPieces);
        detailsView.Rebuild();
    }
    private void OnDeletePiecesClicked()
    {
        detailsList.Remove(activeDetails);
        detailsView.Rebuild();
    }
    private void LoadDataBase()//��������
    {
         var dataArray = AssetDatabase.FindAssets("DSO", new[] {"Assets"});
         Debug.Log(dataArray.Length);
         int len=dataArray.Length;
         for(int i=0; i<len; i++)
         {
           var path = AssetDatabase.GUIDToAssetPath(dataArray[i]);
           Debug.Log(path+"  ");
           string[] pieces = path.Split("/");
           nameArray.Add(" "+pieces[pieces.Length-1]);
            dialogueDatabase.Add((Dialogue_SO)AssetDatabase.LoadAssetAtPath(path, typeof(Dialogue_SO)));
            //database = (Dialogue_SO)AssetDatabase.LoadAssetAtPath(path, typeof(Dialogue_SO));
         }
         GenerateSectionsView();
         
        //database = AssetDatabase.LoadAssetAtPath<Dialogue_SO>("Assets/testDialogue.asset");
        //detailsList = database.dialoguePieces;
        //�����������޷���¼����
        //EditorUtility.SetDirty(dialogueDatabase);
        //Debug.Log(itemList[0].itemID);
    }
    private void GenerateSectionsView()
    {
        sectionView.MarkDirtyRepaint();
        Func<VisualElement> makeItem = () => sectionRowTemplate.Instantiate();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            e.Q<Label>("Name").text=nameArray[i];
        };
        sectionView.makeItem = makeItem;
        sectionView.bindItem = bindItem;
        sectionView.onSelectionChange += OnSectionSelectiontChange;
        sectionView.itemsSource = dialogueDatabase;
    }
    private void GenerateDetailsView(Dialogue_SO list) //�������ݱ༭��Ϣ
    {
        detailsList=list.dialoguePieces;
        detailsView.MarkDirtyRepaint();
        Func<VisualElement> makeItem = () => detailsRowTemplate.Instantiate();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            //detailsView.MarkDirtyRepaint();
            if (i < detailsList.Count)
            {
                #region ������
                e.Q<TextField>("Name").value = "NaN";//detailsList[i].characterName;
                /*e.Q<TextField>("Name").RegisterCallback<ChangeEvent<string>>(evt =>
                {
                    detailsList[i].characterName = evt.newValue;
                });*/
                e.Q<TextField>("Content").value = detailsList[i].Content;
                e.Q<TextField>("Content").RegisterCallback<ChangeEvent<string>>(evt =>
                {
                    detailsList[i].Content = evt.newValue;
                });
                e.Q<Slider>("Scaler").value = detailsList[i].Scaler;
                e.Q<Slider>("Scaler").RegisterCallback<ChangeEvent<float>>(evt =>
                {
                    detailsList[i].Scaler = evt.newValue;
                });
                e.Q<Toggle>("Append").value = detailsList[i].Append;
                e.Q<Toggle>("Append").RegisterCallback<ChangeEvent<bool>>(evt =>
                {
                    detailsList[i].Append = evt.newValue;
                });
                #endregion
            }
        };
        detailsView.itemsSource = detailsList;
        detailsView.makeItem = makeItem;
        detailsView.bindItem = bindItem;
        detailsView.onSelectionChange += OnDetailsSelectiontChange;

    }
    private void OnDetailsSelectiontChange(IEnumerable<object> selectedItem)
    {
        activeDetails = (DialoguePiece)selectedItem.First();
    }
    private void OnSectionSelectiontChange(IEnumerable<object> selectedItem)
    {
        activeSections = (Dialogue_SO)selectedItem.First();
        GenerateDetailsView(activeSections);
    }
}