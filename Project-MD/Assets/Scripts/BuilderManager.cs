using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private GameObject Builder_UI;
    [SerializeField]
    private GameObject GridLine; // �׸��� ǥ��
    
    private int selectedObjectIndex = -1; // ���õ� �ǹ� �ε���.
    private GameObject mouseIndicator; // ���� ���õ� �ǹ� ����.

    // Builder ���� UI��.    
    private GameObject Center_UI;
    private GameObject Right_UI;
    private Button Modify;
    private Button Fix;

    BuilderMode builderMode;
    enum BuilderMode
    {
        No, // ������ �ƴ�.
        Default, // �⺻ ���� ���.
        Modify, // ��ġ ���� ���.
    }
    
    private void Start()
    {
        // ���� UI �Ҵ�.
        Center_UI = Builder_UI.transform.Find("Center_UI").gameObject;
        Right_UI = Builder_UI.transform.Find("Right_UI").gameObject;
        Modify = Right_UI.transform.Find("Modify").GetComponent<Button>();
        Fix = Right_UI.transform.Find("Fix").GetComponent<Button>();
        builderMode = BuilderMode.No;

        // �ʱ�ȭ.
        StopMove();
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
                GridLine.SetActive(false);
                break;
            case "Modify":
                builderMode = BuilderMode.Modify;
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
    private void interactable_Fix(bool toggle)
    {
        Fix.interactable = toggle;
    }
    private void interactable_Cancel(bool toggle)
    {
        Modify.interactable = toggle;
    }
    private void interactable_Modify(bool toggle)
    {
        Modify.interactable = toggle;
    }    
    private void interactable_Quit(bool toggle)
    {
        Fix.interactable = toggle;
    }    
    public void click_FixButton()
    {
        // �浹 ���� �ǹ��� �ִٸ�?
        if (checkCollide() == true)
            return;        
        StopMove();
    }
    public void click_CancelButton()
    {
        // ��ġ�� ĵ���� �� ����ϴ� �Լ�.
        if (mouseIndicator != null)
            Destroy(mouseIndicator);
        StopMove();
    }
    public void click_QuitButton()
    {
        // �浹 ���� �ǹ��� �ִٸ�?
        if (checkCollide() == true)                    
            return;        

        // Modify -> Default.
        change_BuilderMode("Default");
        StopMove();
    }    
    public void StartPlacement(int ID)
    {
        // �ǹ��� ó�� ������ �� ����ϴ� �Լ�.
        // �Ű� ���� ID�� ���� ���� ������ Data.ID�� �ִٸ� �ҷ�����.
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);
        // ����Ÿ�� ���ٸ� ����.
        if (selectedObjectIndex < 0)
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
        cellIndicator.SetActive(true);
        cellIndicator.transform.localPosition = grid.CellToWorld(gridPos);
        Vector3Int prefab_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = mouseIndicator.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(prefab_size, child_Pos);

        // �������� ����.
        GameManager.instance.builderManager.change_BuilderMode("Modify");

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

        // �� �ε������� ��Ƽ�� �� ������ �Ҵ�.
        cellIndicator.SetActive(true);        
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
        cellIndicator_child.localScale = new Vector3(size.x, 0.001f, size.z);
        cellIndicator_child.localPosition = new Vector3(child_Pos.x, 0f, child_Pos.z);
    }
}
