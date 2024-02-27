using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class BuilderManager : MonoBehaviour
{    
    [SerializeField]
    private GameObject instance_Parent; // 생성된 빌딩 부모.               
    [SerializeField]
    public BuildingDatabaseSO database; // 빌딩 DB            
    [SerializeField]
    private GameObject Builder_UI;    
    [SerializeField]
    private Image BuildingInfo_Image; // 건물 상태창 건물 이미지.    

    private int selectedObjectIndex = -1; // 선택된 건물 인덱스.
    public GameObject mouseIndicator; // 현재 선택된 건물 저장.

    // Builder 하위 UI들.    
    private GameObject Center_UI;

    BuilderMode builderMode;
    enum BuilderMode
    {
        No, // 건축모드 아님.
        Default, // 기본 건축 모드.
        Fix, // 새로운 건물 설치 모드.
        Relocate, // 배치 수정 모드.
    }
    private void Start()
    {
        // 하위 UI 할당.
        Center_UI = Builder_UI.transform.Find("Center_UI").gameObject;

        // 초기화.
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
    private GameObject cellIndicator; // 건물 아래 생성되는 셀.
    [SerializeField]
    private Material[] cell_Color; // 셀 컬러.
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private GameObject GridLine; // 그리드 표시

    // 건물 크래프팅 관련.
    public void change_BuilderMode(string mode)
    {
        // 현재 건축 모드를 변경하는 함수.
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
        // 건물 테마 탭 클릭 시 변경하는 함수.
        for (int i = 0; i < Center_UI.transform.childCount; i++)
        {
            // 원하는 빌딩 탭인 경우.
            if (i == index)
                Center_UI.transform.GetChild(i).gameObject.SetActive(true);
            else
                Center_UI.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void Click_BuilderExit()
    {
        // Default 상태가 아니면 퇴장 불가.
        if (builderMode != BuilderMode.Default)
            return;

        // 배치를 캔슬할 때 사용하는 함수.
        if (mouseIndicator != null)
            Destroy(mouseIndicator);

        // 초기화 후 건축모드 퇴장.
        mouseIndicator = null;
        cellIndicator.SetActive(false);
        change_BuilderMode("No");
        StopMove();
    }
    public void StartPlacement(int ID)
    {
        // 수정 모드일 때는 설치 불가.
        if (builderMode == BuilderMode.Relocate)
            return;

        // 건물을 처음 생성할 때 사용하는 함수.
        // 매개 변수 ID와 같은 값을 가지는 Data.ID가 있다면 불러오기.
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0) // 데이터가 없다면 중지.
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        // 센터 -> 그리드 좌표 변환.        
        Vector3Int gridPos = centerToGrid();

        // 불러온 데이터 건물 생성.
        if (mouseIndicator != null)
            Destroy(mouseIndicator);
        mouseIndicator = Instantiate(database.buildingsData[selectedObjectIndex].prefab, grid.CellToWorld(gridPos), Quaternion.identity, instance_Parent.transform);

        // 셀 인디케이터 액티브 및 사이즈 할당.        
        cellIndicator.transform.localPosition = grid.CellToWorld(gridPos);
        Vector3Int prefab_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = mouseIndicator.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(prefab_size, child_Pos);
        cellIndicator.SetActive(true);

        // 설치 모드로 변경.
        GameManager.instance.builderManager.change_BuilderMode("Fix");

        // 콜라이더 체크.
        change_CellColor();
    }
    public void StartMove(int ID, GameObject building)
    {
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

        // 셀 인디케이터 사이즈 할당.                
        Vector3Int building_size = database.buildingsData[selectedObjectIndex].size;
        Vector3 child_Pos = building.transform.GetChild(0).transform.localPosition;
        set_CellIndicator(building_size, child_Pos);
    }
    public void StopMove()
    {
        // 인디케이터 및 데이터 초기화 함수.
        selectedObjectIndex = -1;
        mouseIndicator = null;
        cellIndicator.SetActive(false);
    }
    public void moveItem()
    {
        // 마우스 -> 그리드 좌표 변환.
        Vector3Int gridPos = mouseToGrid();

        // 건물 마우스 따라 다니게 하는 함수.        
        mouseIndicator.transform.position = grid.CellToWorld(gridPos);
        cellIndicator.transform.position = grid.CellToWorld(gridPos);

        // 다른 건물과의 충돌 체크.
        change_CellColor();

        // 셀 인디케이터 액티브
        cellIndicator.SetActive(true);
    }
    public bool checkCollide()
    {
        // 건물끼리 충돌 감지 함수.
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
        // 마우스 -> 그리드 좌표 변환 함수.
        Vector3 mousePos = GameManager.instance.inputManager.get_MousePosition();
        mousePos.y = 0;
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        return gridPos;
    }
    private Vector3Int centerToGrid()
    {
        // 마우스 -> 그리드 좌표 변환 함수.
        Vector3 centerPos = GameManager.instance.inputManager.get_CenterPosition();
        centerPos.y = 0;
        Vector3Int gridPos = grid.WorldToCell(centerPos);
        return gridPos;
    }
    private void set_CellIndicator(Vector3Int size, Vector3 child_Pos)
    {
        // 오브젝트 아래 깔리는 셀 세팅 함수.
        Transform cellIndicator_child = cellIndicator.transform.GetChild(0).transform;
        cellIndicator_child.localScale = new Vector3(size.x, 0.00001f, size.z);
        cellIndicator_child.localPosition = new Vector3(child_Pos.x, 0f, child_Pos.z);
    }
}

partial class BuilderManager
{
    // UI 불러오기
    [SerializeField]
    private GameObject BuildingInfo_UI; // 건물 상태창 UI      
    [SerializeField]
    private GameObject SelectBuildingNpc_UI; // 건물에 배치할 캐릭터 선택 UI
    [SerializeField]
    private GameObject SelectBuildingNpc_Button; // 건물에 배치할 캐릭터 선택 버튼.
    public void ViewBuildingInfo(int ID, int level, int pollution, int currentNpcAI_ID)
    {
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);
        // 데이타가 없다면 중지.
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        GameObject TextInfo = BuildingInfo_UI.transform.Find("Info").gameObject;
        TextInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = database.buildingsData[selectedObjectIndex].name; // 이름
        TextInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = level.ToString(); // 레벨.
        TextInfo.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = database.buildingsData[selectedObjectIndex].type; // 타입.
        TextInfo.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = database.buildingsData[selectedObjectIndex].product.ToString(); // 생산량
        TextInfo.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = database.buildingsData[selectedObjectIndex].production_Speed.ToString(); // 생산속도.
        TextInfo.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = pollution.ToString(); // 오염도.
        BuildingInfo_Image.sprite = database.buildingsData[selectedObjectIndex].sprite; // 건물 이미지.

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