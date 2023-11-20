using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlacementSystem : MonoBehaviour
{    
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private GameObject cellIndicator; // �� Ȯ�ο�.    
    [SerializeField]
    private Material[] cell_Color; // �� �÷� ����.
    [SerializeField]
    private GameObject instance_Parent; // ������ ���� �θ�.        

    private GameObject indicator; // ������ ������Ʈ,
    private GameObject mouseIndicator; // ���콺�� �ɸ� �ӽ� ������Ʈ.    

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    public BuildingDatabaseSO database;
    private int selectedObjectIndex = -1;
    
    private void Start()
    {
        StopPlacement();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedObjectIndex < 0)
            return;

        // ���콺 -> �׸��� ��ǥ ��ȯ.        
        Vector3Int gridPos = mouseToGrid();

        // ���� �� �� �̵�.
        moveItem(gridPos);
    }

    #region �ǹ� ����
    public void StartPlacement(int ID)
    {        
        // �Ű� ���� ID�� ���� ���� ������ Data.ID�� �ִٸ� �ҷ�����.
        selectedObjectIndex = database.buildingsData.FindIndex(data=> data.ID == ID);
        
        // ����Ÿ�� ���ٸ� ����.
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        
        // �ε������� �Ҵ�.
        GameObject prefab = database.buildingsData[selectedObjectIndex].prefab;
        indicator = prefab;        
        mouseIndicator = Instantiate(prefab);

        // �� �ε������� ��Ƽ�� �� ������ �Ҵ�.
        cellIndicator.SetActive(true);
        Vector3Int prefab_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = prefab.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(prefab_size, child_Pos);

        // inputManager Ŭ�� �׼� �Ҵ�.
        GameManager.instance.inputManager.OnClicked += PlaceStructure;        
    }
    private void PlaceStructure()
    {
        // UI�� ��ġ�� ���� �ΰ��ӿ��� �������� �ʵ���.
        if (GameManager.instance.inputManager.IsPointerOverUI())        
            return;
        
        // ���콺 -> �׸��� ��ǥ ��ȯ.        
        Vector3Int gridPos = mouseToGrid();

        // ������ ������Ʈ ����.                
        GameObject clone = Instantiate(indicator, grid.CellToWorld(gridPos), Quaternion.identity, instance_Parent.transform);
        clone.GetComponent<Building>().isDisplayed = true;

        // inputManager Ŭ�� �׼� ȸ��.
        GameManager.instance.inputManager.OnClicked -= PlaceStructure;

        // �ʱ�ȭ
        StopPlacement();        
    }
    private void StopPlacement()
    {
        // �ε������� �� ������ �ʱ�ȭ �Լ�.
        selectedObjectIndex = -1;
        Destroy(mouseIndicator);
        cellIndicator.SetActive(false);
        indicator = null;
    }
    #endregion
    #region �ǹ� �̵�
    public void StartMove(int ID, GameObject building)
    {
        Debug.Log("����");
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
        Vector3Int prefab_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = building.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(prefab_size, child_Pos);

        // inputManager Ŭ�� �׼� �Ҵ�.
        GameManager.instance.inputManager.OnClicked += PlaceMove;
    }
    private void PlaceMove()
    {
        // UI�� ��ġ�� ���� �ΰ��ӿ��� �������� �ʵ���.
        if (GameManager.instance.inputManager.IsPointerOverUI())
            return;

        // ���콺 -> �׸��� ��ǥ ��ȯ.        
        Vector3Int gridPos = mouseToGrid();        

        // inputManager Ŭ�� �׼� ȸ��.
        GameManager.instance.inputManager.OnClicked -= PlaceMove;

        // �ʱ�ȭ        
        StopMove();
    }
    private void StopMove()
    {
        // �ε������� �� ������ �ʱ�ȭ �Լ�.
        selectedObjectIndex = -1;
        mouseIndicator.GetComponent<Building>().isDisplayed = true;
        mouseIndicator = null;
        cellIndicator.SetActive(false);        
    }
    #endregion

    void moveItem(Vector3Int gridPos)
    {
        // �ǹ� ���콺 ���� �ٴϰ� �ϴ� �Լ�.        
        mouseIndicator.transform.position = grid.CellToWorld(gridPos);
        cellIndicator.transform.position = grid.CellToWorld(gridPos);

        // �ٸ� �ǹ����� �浹 üũ.
        if (checkCollide())
            cellIndicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = cell_Color[0];
        else
            cellIndicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = cell_Color[1];
    }

    bool checkCollide()
    {        
        // �ǹ����� �浹 ���� �Լ�.
        if (mouseIndicator.GetComponent<Building>().isCollide == true)        
            return true;        
        return false;
    }

    private Vector3Int mouseToGrid()
    {
        // ���콺 -> �׸��� ��ǥ ��ȯ �Լ�.
        Vector3 mousePos = GameManager.instance.inputManager.get_MousePosition();
        mousePos.y = 0;
        Vector3Int gridPos = grid.WorldToCell(mousePos);
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