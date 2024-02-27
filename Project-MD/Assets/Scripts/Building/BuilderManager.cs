using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class BuilderManager : MonoBehaviour
{    
    [SerializeField]
    private GameObject instance_Parent; // ������ ���� �θ�.               
    [SerializeField]
    public BuildingDatabaseSO database; // ���� DB            
    [SerializeField]
    private GameObject Builder_UI;    
    [SerializeField]
    private Image BuildingInfo_Image; // �ǹ� ����â �ǹ� �̹���.    

    private int selectedObjectIndex = -1; // ���õ� �ǹ� �ε���.
    public GameObject mouseIndicator; // ���� ���õ� �ǹ� ����.

    // Builder ���� UI��.    
    private GameObject Center_UI;

    BuilderMode builderMode;
    enum BuilderMode
    {
        No, // ������ �ƴ�.
        Default, // �⺻ ���� ���.
        Fix, // ���ο� �ǹ� ��ġ ���.
        Relocate, // ��ġ ���� ���.
    }
    private void Start()
    {
        // ���� UI �Ҵ�.
        Center_UI = Builder_UI.transform.Find("Center_UI").gameObject;

        // �ʱ�ȭ.
        StopMove();
        builderMode = BuilderMode.No;
    }
    private void Update()
    {
        change_CellColor();
    }
}

partial class BuilderManager
{
    [SerializeField]
    private GameObject cellIndicator; // �ǹ� �Ʒ� �����Ǵ� ��.
    [SerializeField]
    private Material[] cell_Color; // �� �÷�.
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private GameObject GridLine; // �׸��� ǥ��

    // �ǹ� ũ������ ����.
    public void change_BuilderMode(string mode)
    {
        // ���� ���� ��带 �����ϴ� �Լ�.
        switch (mode)
        {
            case "No":
                builderMode = BuilderMode.No;
                GridLine.SetActive(false);
                break;
            case "Default":
                builderMode = BuilderMode.Default;
                GridLine.SetActive(true);
                break;
            case "Fix":
                builderMode = BuilderMode.Fix;
                GridLine.SetActive(true);
                break;
            case "Relocate":
                builderMode = BuilderMode.Relocate;
                GridLine.SetActive(true);
                break;
        }
    }
    public int get_BuilderMode()
    {
        return (int)builderMode;
    }
    public void click_BuildingTap(int index)
    {
        // �ǹ� �׸� �� Ŭ�� �� �����ϴ� �Լ�.
        for (int i = 0; i < Center_UI.transform.childCount; i++)
        {
            // ���ϴ� ���� ���� ���.
            if (i == index)
                Center_UI.transform.GetChild(i).gameObject.SetActive(true);
            else
                Center_UI.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void Click_BuilderExit()
    {
        // Default ���°� �ƴϸ� ���� �Ұ�.
        if (builderMode != BuilderMode.Default)
            return;

        // ��ġ�� ĵ���� �� ����ϴ� �Լ�.
        if (mouseIndicator != null)
            Destroy(mouseIndicator);

        // �ʱ�ȭ �� ������ ����.
        mouseIndicator = null;
        cellIndicator.SetActive(false);
        change_BuilderMode("No");
        StopMove();
    }
    public void StartPlacement(int ID)
    {
        // ���� ����� ���� ��ġ �Ұ�.
        if (builderMode == BuilderMode.Relocate)
            return;

        // �ǹ��� ó�� ������ �� ����ϴ� �Լ�.
        // �Ű� ���� ID�� ���� ���� ������ Data.ID�� �ִٸ� �ҷ�����.
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0) // �����Ͱ� ���ٸ� ����.
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // ���� -> �׸��� ��ǥ ��ȯ.        
        Vector3Int gridPos = centerToGrid();

        // �ҷ��� ������ �ǹ� ����.
        if (mouseIndicator != null)
            Destroy(mouseIndicator);
        mouseIndicator = Instantiate(database.buildingsData[selectedObjectIndex].prefab, grid.CellToWorld(gridPos), Quaternion.identity, instance_Parent.transform);

        // �� �ε������� ��Ƽ�� �� ������ �Ҵ�.        
        cellIndicator.transform.localPosition = grid.CellToWorld(gridPos);
        Vector3Int prefab_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = mouseIndicator.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(prefab_size, child_Pos);
        cellIndicator.SetActive(true);

        // ��ġ ���� ����.
        GameManager.instance.builderManager.change_BuilderMode("Fix");

        // �ݶ��̴� üũ.
        change_CellColor();
    }
    public void StartMove(int ID, GameObject building)
    {
        // ���� �ǹ� ��ġ�� ������ �� ����ϴ� �Լ�.
        // �Ű� ���� ID�� ���� ���� ������ Data.ID�� �ִٸ� �ҷ�����.
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);

        // ����Ÿ�� ���ٸ� ����.
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // �ε������� �Ҵ�.        
        mouseIndicator = building;

        // �� �ε������� ������ �Ҵ�.                
        Vector3Int building_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = building.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(building_size, child_Pos);
    }
    public void StopMove()
    {
        // �ε������� �� ������ �ʱ�ȭ �Լ�.
        selectedObjectIndex = -1;
        mouseIndicator = null;
        cellIndicator.SetActive(false);
    }
    public void moveItem()
    {
        // ���콺 -> �׸��� ��ǥ ��ȯ.
        Vector3Int gridPos = mouseToGrid();

        // �ǹ� ���콺 ���� �ٴϰ� �ϴ� �Լ�.        
        mouseIndicator.transform.position = grid.CellToWorld(gridPos);
        cellIndicator.transform.position = grid.CellToWorld(gridPos);

        // �ٸ� �ǹ����� �浹 üũ.
        change_CellColor();

        // �� �ε������� ��Ƽ��
        cellIndicator.SetActive(true);
    }
    public bool checkCollide()
    {
        // �ǹ����� �浹 ���� �Լ�.
        if (mouseIndicator != null && mouseIndicator.GetComponent<Building>().isCollide == true)
            return true;
        return false;
    }
    private void change_CellColor()
    {
        if (checkCollide())
            cellIndicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = cell_Color[0];
        else
            cellIndicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = cell_Color[1];
    }
    private Vector3Int mouseToGrid()
    {
        // ���콺 -> �׸��� ��ǥ ��ȯ �Լ�.
        Vector3 mousePos = GameManager.instance.inputManager.get_MousePosition();
        mousePos.y = 0;
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        return gridPos;
    }
    private Vector3Int centerToGrid()
    {
        // ���콺 -> �׸��� ��ǥ ��ȯ �Լ�.
        Vector3 centerPos = GameManager.instance.inputManager.get_CenterPosition();
        centerPos.y = 0;
        Vector3Int gridPos = grid.WorldToCell(centerPos);
        return gridPos;
    }
    private void set_CellIndicator(Vector3Int size, Vector3 child_Pos)
    {
        // ������Ʈ �Ʒ� �򸮴� �� ���� �Լ�.
        Transform cellIndicator_child = cellIndicator.transform.GetChild(0).transform;
        cellIndicator_child.localScale = new Vector3(size.x, 0.00001f, size.z);
        cellIndicator_child.localPosition = new Vector3(child_Pos.x, 0f, child_Pos.z);
    }
}

partial class BuilderManager
{
    // UI �ҷ�����
    [SerializeField]
    private GameObject BuildingInfo_UI; // �ǹ� ����â UI      
    [SerializeField]
    private GameObject SelectBuildingNpc_UI; // �ǹ��� ��ġ�� ĳ���� ���� UI
    [SerializeField]
    private GameObject SelectBuildingNpc_Button; // �ǹ��� ��ġ�� ĳ���� ���� ��ư.
    public void ViewBuildingInfo(int ID, int level, int pollution, int currentNpcAI_ID)
    {
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);
        // ����Ÿ�� ���ٸ� ����.
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        GameObject TextInfo = BuildingInfo_UI.transform.Find("Info").gameObject;
        TextInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = database.buildingsData[selectedObjectIndex].name; // �̸�
        TextInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = level.ToString(); // ����.
        TextInfo.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = database.buildingsData[selectedObjectIndex].type; // Ÿ��.
        TextInfo.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = database.buildingsData[selectedObjectIndex].product.ToString(); // ���귮
        TextInfo.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = database.buildingsData[selectedObjectIndex].production_Speed.ToString(); // ����ӵ�.
        TextInfo.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = pollution.ToString(); // ������.
        BuildingInfo_Image.sprite = database.buildingsData[selectedObjectIndex].sprite; // �ǹ� �̹���.

        BuildingInfo_UI.SetActive(true);
    }

    public void ViewSelectBuildingNpcUI()
    {
        List<GameObject> Npc_List = GameManager.instance.npc_Datamanager.get_totalNpcValues();

        GameObject SelectBuildingNpc_Content = SelectBuildingNpc_UI.transform.Find("Scroll View/Viewport/Content").gameObject;
        
        foreach (Transform child in SelectBuildingNpc_Content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject npc in Npc_List)
        {
            GameObject clone = Instantiate(SelectBuildingNpc_Button, SelectBuildingNpc_Content.transform);
            clone.transform.GetComponent<SelectBuildingNpc_Button>().SetButtonData(npc);
        }

        SelectBuildingNpc_UI.transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
        SelectBuildingNpc_UI.SetActive(true);
    }
}