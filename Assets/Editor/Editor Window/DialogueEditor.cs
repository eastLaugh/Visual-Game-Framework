using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;
using Unity.EditorCoroutines.Editor;

public class DialogueEditor : EditorWindow
{
    private VisualTreeAsset sectionRowTemplate;
    private VisualTreeAsset detailsRowTemplate;
    private VisualElement editArea;
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
        editArea = root.Q<VisualElement>("EditArea");
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
        //newPieces.Append = false;
        //newPieces.characterName = "input name";
        detailsList.Add(newPieces);
        GenerateIndex();
        GenerateDetailsView(activeSections);
       
    }
    private void OnDeletePiecesClicked()
    {
        detailsList.Remove(activeDetails);
        GenerateIndex();
        GenerateDetailsView(activeSections);
        
    }
    private void LoadDataBase(Dialogue_SO so=null)//��������
    {
        if (so)
        {
            nameArray.Add("Single File - " + AssetDatabase.GetAssetPath(so));
            dialogueDatabase.Add(so);

        }
        else
        {
            dialogueDatabase.Clear();

            var dataArray = AssetDatabase.FindAssets("t:Dialogue_SO", new[] { "Assets" });
            Debug.Log(dataArray.Length);
            int len = dataArray.Length;
            for (int i = 0; i < len; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(dataArray[i]);
                Debug.Log(path + "  ");
                string[] pieces = path.Split("/");
                nameArray.Add(" " + pieces[pieces.Length - 1]);
                dialogueDatabase.Add((Dialogue_SO)AssetDatabase.LoadAssetAtPath(path, typeof(Dialogue_SO)));
                EditorUtility.SetDirty(dialogueDatabase[i]);

            }
            /*database = (Dialogue_SO)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(dataArray[0]), typeof(Dialogue_SO));
            EditorUtility.SetDirty(database);
            detailsView.MarkDirtyRepaint();TATATATATATATATATAT
            Debug.Log(AssetDatabase.GUIDToAssetPath(dataArray[0]));
            detailsView.Q<TextField>("sadness").value = database.dialoguePieces[1].Content;
            detailsView.Q<TextField>("sadness").RegisterValueChangedCallback(evt =>
            {
                database.dialoguePieces[1].Content = evt.newValue;
            }
            );
            for (int i = 0; i < database.dialoguePieces.Count-2; i++)
            {

                VisualElement e = (VisualElement)detailsRowTemplate.Instantiate();
                //e.MarkDirtyRepaint();
                e.Q<TextField>("Content").name = "Content#" + i.ToString();
                detailsView.Add(e);
                

            }
            for (int i = 0; i < database.dialoguePieces.Count - 2; i++)
            {
                EditorUtility.SetDirty(database);
                database.dialoguePieces[i].Content = "qunide";
                detailsView.MarkDirtyRepaint();
                detailsView.Q<TextField>("Content#" + i.ToString()).value = database.dialoguePieces[i].Content;
                detailsView.Q<TextField>("Content#" + i.ToString()).RegisterValueChangedCallback(evt =>
                {
                    EditorUtility.SetDirty(database);
                    detailsView.MarkDirtyRepaint();
                    database.dialoguePieces[i].Content = "qunide";
                    Debug.Log("changed");
                });
            }*/
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
        detailsList = list.dialoguePieces;
        detailsView.MarkDirtyRepaint();
        Func<VisualElement> makeItem = () => detailsRowTemplate.Instantiate();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            detailsView.MarkDirtyRepaint();
            if (i < detailsList.Count)
            {
                #region ������
                //e.Q<TextField>("Name").value = "NaN";//detailsList[i].characterName;
                /*e.Q<TextField>("Name").RegisterCallback<ChangeEvent<string>>(evt =>
                {
                    detailsList[i].characterName = evt.newValue;
                });*/
                e.Q<Label>("Display").text = detailsList[i].Content==null? "Input Text" : detailsList[i].Content;
           
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
        activeDetails=(DialoguePiece)selectedItem.First();
        Debug.Log("the index is:" + activeDetails.index.ToString());
        EditText();
    }
    private void EditText()
    {
        editArea.MarkDirtyRepaint();
        editArea.Q<TextField>("Edit").value=activeDetails.Content;
        editArea.Q<TextField>("Edit").RegisterValueChangedCallback(evt =>
        {
            activeDetails.Content = evt.newValue;
            detailsView.Rebuild();
        });

    }
    private void OnSectionSelectiontChange(IEnumerable<object> selectedItem)
    {
        activeSections = (Dialogue_SO)selectedItem.First();
        EditorUtility.SetDirty(activeSections);
        detailsView.MarkDirtyRepaint();
        GenerateDetailsView(activeSections);
    }
//https://forum.unity.com/threads/is-it-possible-to-open-scriptableobjects-in-custom-editor-cindows-with-double-click.992796/
    [UnityEditor.Callbacks.OnOpenAsset]
    static bool OnOpenAsset(int instanceID, int line){
        var tmp = EditorUtility.InstanceIDToObject(instanceID) as Dialogue_SO;
        if(tmp){
            DialogueEditor wnd = GetWindow<DialogueEditor>();
            wnd.titleContent = new GUIContent("DialogueEditor - " + AssetDatabase.GetAssetPath(tmp));

            wnd.LoadDataBase(tmp);
            
        }
        return false;
    }
    private void GenerateIndex()
    {
        for(int i=0;i<detailsList.Count;i++)
        {
            detailsList[i].index = i;
        }
    }
    
}