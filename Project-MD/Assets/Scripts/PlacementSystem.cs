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
    private GameObject cellIndicator; // 셀 확인용.    
    [SerializeField]
    private Material[] cell_Color; // 셀 컬러 변경.
    [SerializeField]
    private GameObject instance_Parent; // 생성된 빌딩 부모.        

    private GameObject indicator; // 생성할 오브젝트,
    private GameObject mouseIndicator; // 마우스에 걸릴 임시 오브젝트.    

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

        // 마우스 -> 그리드 좌표 변환.        
        Vector3Int gridPos = mouseToGrid();

        // 물건 및 셀 이동.
        moveItem(gridPos);
    }

    #region 건물 생성
    public void StartPlacement(int ID)
    {        
        // 매개 변수 ID와 같은 값을 가지는 Data.ID가 있다면 불러오기.
        selectedObjectIndex = database.buildingsData.FindIndex(data=> data.ID == ID);
        
        // 데이타가 없다면 중지.
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        
        // 인디케이터 할당.
        GameObject prefab = database.buildingsData[selectedObjectIndex].prefab;
        indicator = prefab;        
        mouseIndicator = Instantiate(prefab);

        // 셀 인디케이터 액티브 및 사이즈 할당.
        cellIndicator.SetActive(true);
        Vector3Int prefab_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = prefab.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(prefab_size, child_Pos);

        // inputManager 클릭 액션 할당.
        GameManager.instance.inputManager.OnClicked += PlaceStructure;        
    }
    private void PlaceStructure()
    {
        // UI를 터치할 때는 인게임에서 반응하지 않도록.
        if (GameManager.instance.inputManager.IsPointerOverUI())        
            return;
        
        // 마우스 -> 그리드 좌표 변환.        
        Vector3Int gridPos = mouseToGrid();

        // 선택한 오브젝트 생성.                
        GameObject clone = Instantiate(indicator, grid.CellToWorld(gridPos), Quaternion.identity, instance_Parent.transform);
        clone.GetComponent<Building>().isDisplayed = true;

        // inputManager 클릭 액션 회수.
        GameManager.instance.inputManager.OnClicked -= PlaceStructure;

        // 초기화
        StopPlacement();        
    }
    private void StopPlacement()
    {
        // 인디케이터 및 데이터 초기화 함수.
        selectedObjectIndex = -1;
        Destroy(mouseIndicator);
        cellIndicator.SetActive(false);
        indicator = null;
    }
    #endregion
    #region 건물 이동
    public void StartMove(int ID, GameObject building)
    {
        Debug.Log("무브");
        // 기존 건물 위치를 변경할 때 사용하는 함수.
        // 매개 변수 ID와 같은 값을 가지는 Data.ID가 있다면 불러오기.
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);

        // 데이타가 없다면 중지.
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // 인디케이터 할당.        
        mouseIndicator = building;

        // 셀 인디케이터 액티브 및 사이즈 할당.
        cellIndicator.SetActive(true);
        Vector3Int prefab_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = building.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(prefab_size, child_Pos);

        // inputManager 클릭 액션 할당.
        GameManager.instance.inputManager.OnClicked += PlaceMove;
    }
    private void PlaceMove()
    {
        // UI를 터치할 때는 인게임에서 반응하지 않도록.
        if (GameManager.instance.inputManager.IsPointerOverUI())
            return;

        // 마우스 -> 그리드 좌표 변환.        
        Vector3Int gridPos = mouseToGrid();        

        // inputManager 클릭 액션 회수.
        GameManager.instance.inputManager.OnClicked -= PlaceMove;

        // 초기화        
        StopMove();
    }
    private void StopMove()
    {
        // 인디케이터 및 데이터 초기화 함수.
        selectedObjectIndex = -1;
        mouseIndicator.GetComponent<Building>().isDisplayed = true;
        mouseIndicator = null;
        cellIndicator.SetActive(false);        
    }
    #endregion

    void moveItem(Vector3Int gridPos)
    {
        // 건물 마우스 따라 다니게 하는 함수.        
        mouseIndicator.transform.position = grid.CellToWorld(gridPos);
        cellIndicator.transform.position = grid.CellToWorld(gridPos);

        // 다른 건물과의 충돌 체크.
        if (checkCollide())
            cellIndicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = cell_Color[0];
        else
            cellIndicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = cell_Color[1];
    }

    bool checkCollide()
    {        
        // 건물끼리 충돌 감지 함수.
        if (mouseIndicator.GetComponent<Building>().isCollide == true)        
            return true;        
        return false;
    }

    private Vector3Int mouseToGrid()
    {
        // 마우스 -> 그리드 좌표 변환 함수.
        Vector3 mousePos = GameManager.instance.inputManager.get_MousePosition();
        mousePos.y = 0;
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        return gridPos;
    }

    private void set_CellIndicator(Vector3Int size, Vector3 child_Pos)
    {        
        // 오브젝트 아래 깔리는 셀 세팅 함수.
        Transform cellIndicator_child = cellIndicator.transform.GetChild(0).transform;
        cellIndicator_child.localScale = new Vector3(size.x, 0.001f, size.z);
        cellIndicator_child.localPosition = new Vector3(child_Pos.x, 0f, child_Pos.z);
    }
}