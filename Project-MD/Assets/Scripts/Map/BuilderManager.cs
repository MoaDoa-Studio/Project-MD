using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuilderManager : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private GameObject cellIndicator; // �ǹ� �Ʒ� �����Ǵ� ��.
    [SerializeField]
    private Material[] cell_Color; // �� �÷�.
    [SerializeField]
    private GameObject instance_Parent; // ������ ���� �θ�.               
    [SerializeField]
    public BuildingDatabaseSO database; // ���� DB        
    [SerializeField]
    public ResourceManager resourceManager; // ���� DB        
    [SerializeField]
    private GameObject Builder_UI;
    [SerializeField]
    private GameObject GridLine; // �׸��� ǥ��
    [SerializeField]
    private GameObject BuildingInfo_UI; // �ǹ� ����â UI.
    [SerializeField]
    private TextMeshProUGUI[] BuildingInfo; // �ǹ� ����â ����.    
    [SerializeField]
    private Image BuildingInfo_Image; // �ǹ� ����â �ǹ� �̹���.    

    private int selectedObjectIndex = -1; // ���õ� �ǹ� �ε���.
    public GameObject mouseIndicator; // ���� ���õ� �ǹ� ����.

    // Builder ���� UI��.    
    private GameObject Center_UI;
    private GameObject Right_UI;
    private Button Relocate;
    private Button Fix;
    private Button Cancel;
    private Button Quit;    
    
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
        Right_UI = Builder_UI.transform.Find("Right_UI").gameObject;        
        Relocate = Right_UI.transform.Find("Relocate").GetComponent<Button>();
        Fix = Right_UI.transform.Find("Fix").GetComponent<Button>();
        Cancel = Right_UI.transform.Find("Cancel").GetComponent<Button>();
        Quit = Right_UI.transform.Find("Quit").GetComponent<Button>();
        resourceManager = this.GetComponent<ResourceManager>();

        // �ʱ�ȭ.
        StopMove();
        builderMode = BuilderMode.No;
    }

    private void Update()
    {
        change_CellColor();
    }
    public void change_BuilderMode(string mode)
    {        
        // ���� ���� ��带 �����ϴ� �Լ�.
        switch(mode)
        {
            case "No":
                builderMode = BuilderMode.No;
                GridLine.SetActive(false);
                break;
            case "Default":
                builderMode = BuilderMode.Default;
                GridLine.SetActive(true);
                Fix.interactable = false;
                Cancel.interactable = false;
                Relocate.interactable = true;
                Quit.interactable = true;                
                break;
            case "Fix":
                builderMode = BuilderMode.Fix;
                GridLine.SetActive(true);                
                Fix.interactable = true;
                Cancel.interactable = true;
                Relocate.interactable = false;
                Quit.interactable = false;
                break;
            case "Relocate":
                builderMode = BuilderMode.Relocate;
                GridLine.SetActive(true);                
                Fix.interactable = false;
                Cancel.interactable = true;
                Relocate.interactable = true;
                Quit.interactable = true;
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

    public void ViewBuildingInfo(int ID, int level, int pollution)
    {
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);
        // ����Ÿ�� ���ٸ� ����.
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        
        BuildingInfo[0].text = database.buildingsData[selectedObjectIndex].name; // �̸�
        BuildingInfo[1].text = level.ToString(); // ����.
        BuildingInfo[2].text = database.buildingsData[selectedObjectIndex].type; // Ÿ��.
        BuildingInfo[3].text = database.buildingsData[selectedObjectIndex].product.ToString();
        BuildingInfo[4].text = database.buildingsData[selectedObjectIndex].production_Speed.ToString();        
        BuildingInfo[5].text = pollution.ToString();
        BuildingInfo_Image.sprite = database.buildingsData[selectedObjectIndex].sprite;
        BuildingInfo_UI.SetActive(true);
    }
}
