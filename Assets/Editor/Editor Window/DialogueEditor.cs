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
    private List<DialoguePiece> detailsList = new List<DialoguePiece>();
    private Dialogue_SO database;
    private ListView detailsView;
    private ListView sectionView;
    private DialoguePiece activeDetails;
    private Dialogue_SO activeSections;
    private List<Dialogue_SO> dialogueDatabase = new List<Dialogue_SO>();
    private List<string> nameArray = new List<string>();
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
        detailsView.selectionType=SelectionType.None;//列表不能选择
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
    private void LoadDataBase(Dialogue_SO so = null)//��������
    {
        if (so)
        {
            nameArray.Add("[" + AssetDatabase.GetAssetPath(so) + "]");
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
                //database = (Dialogue_SO)AssetDatabase.LoadAssetAtPath(path, typeof(Dialogue_SO));
            }


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
            e.Q<Label>("Name").text = nameArray[i];
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
                    //检测是否换行，如果换行就自动开启新的对话
                    // if (evt.newValue.Length > 0)
                    //     if (evt.newValue[evt.newValue.Length - 1] == '\n')
                    //     { //System.Environment.NewLine
                    //         e.Q<TextField>("Content").value = evt.previousValue;
                    //         if (i == detailsList.Count - 1)
                    //         {
                    //             OnAddPiecesClicked();
                    //         }
                    //         Debug.Log(i);
                           
                            //detailsView[1].Q<TextField>("Content").Focus();
                            //detailsView[i].Add(new Label("selected I am "+ (i+1).ToString()));
                            //detailsView.Q<Scroller>().value += e.resolvedStyle.height;

                        // }
                        // else
                        // {
                            detailsList[i].Content = evt.newValue;
                        // }
                });
                //e.Add(new Label(i.ToString()));

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
    //https://forum.unity.com/threads/is-it-possible-to-open-scriptableobjects-in-custom-editor-cindows-with-double-click.992796/
    [UnityEditor.Callbacks.OnOpenAsset]
    static bool OnOpenAsset(int instanceID, int line)
    {
        var tmp = EditorUtility.InstanceIDToObject(instanceID) as Dialogue_SO;
        if (tmp)
        {
            DialogueEditor wnd = GetWindow<DialogueEditor>();
            wnd.titleContent = new GUIContent("DialogueEditor");

            wnd.LoadDataBase(tmp);

        }
        return false;
    }

}