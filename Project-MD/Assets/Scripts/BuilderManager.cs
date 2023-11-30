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
    private GameObject cellIndicator; // 건물 아래 생성되는 셀.
    [SerializeField]
    private Material[] cell_Color; // 셀 컬러.
    [SerializeField]
    private GameObject instance_Parent; // 생성된 빌딩 부모.               
    [SerializeField]
    public BuildingDatabaseSO database; // 빌딩 DB        
    [SerializeField]
    public ResourceManager resourceManager; // 빌딩 DB        
    [SerializeField]
    private GameObject Builder_UI;
    [SerializeField]
    private GameObject GridLine; // 그리드 표시
    [SerializeField]
    private GameObject BuildingInfo_UI; // 건물 상태창 UI.
    [SerializeField]
    private TextMeshProUGUI[] BuildingInfo; // 건물 상태창 인포.
    [SerializeField]
    private GameObject interacting_Build; // 버튼 v -와 동기화시킬 건물

    private int selectedObjectIndex = -1; // 선택된 건물 인덱스.
    private GameObject mouseIndicator; // 현재 선택된 건물 저장.

    // Builder 하위 UI들.    
    private GameObject Center_UI;
    private GameObject Right_UI;
    private Button Relocate;
    private Button Fix;
    private Button Cancel;
    private Button Quit;
    private GameObject Inquire;
    
    BuilderMode builderMode;
    enum BuilderMode
    {
        No, // 건축모드 아님.
        Default, // 기본 건축 모드.
        Fix, // 새로운 건물 설치 모드.
        Relocate, // 배치 수정 모드.
    }


    // 임시.
    public TextMeshProUGUI cur_Mode;

    private void Start()
    {
        // 하위 UI 할당.
        Center_UI = Builder_UI.transform.Find("Center_UI").gameObject;
        Right_UI = Builder_UI.transform.Find("Right_UI").gameObject;
        Inquire = BuildingInfo_UI.transform.Find("Inquire").gameObject;
        Relocate = Right_UI.transform.Find("Relocate").GetComponent<Button>();
        Fix = Right_UI.transform.Find("Fix").GetComponent<Button>();
        Cancel = Right_UI.transform.Find("Cancel").GetComponent<Button>();
        Quit = Right_UI.transform.Find("Quit").GetComponent<Button>();
        resourceManager = this.GetComponent<ResourceManager>();
        // 초기화.
        StopMove();
        builderMode = BuilderMode.No;
    }
            
    public void change_BuilderMode(string mode)
    {
        cur_Mode.text = mode;
        // 현재 건축 모드를 변경하는 함수.
        switch(mode)
        {
            case "No":
                builderMode = BuilderMode.No;
                GridLine.SetActive(false);
                break;
            case "Default":
                builderMode = BuilderMode.Default;
                GridLine.SetActive(false);
                interactable_Fix(false);
                interactable_Cancel(false);
                interactable_Relocate(true);
                interactable_Quit(true);
                break;
            case "Fix":
                builderMode = BuilderMode.Fix;
                GridLine.SetActive(true);
                interactable_Fix(true);
                interactable_Cancel(true);
                interactable_Relocate(false);
                interactable_Quit(false);
                break;
            case "Relocate":
                builderMode = BuilderMode.Relocate;
                GridLine.SetActive(true);
                interactable_Fix(false);
                interactable_Cancel(true);
                interactable_Relocate(true);
                interactable_Quit(true);
                break;
        }
    }
    private void interactable_Fix(bool toggle)
    {
        Fix.interactable = toggle;
    }
    private void interactable_Cancel(bool toggle)
    {
        Cancel.interactable = toggle;
    }
    private void interactable_Relocate(bool toggle)
    {
        Relocate.interactable = toggle;
    }
    private void interactable_Quit(bool toggle)
    {
        Quit.interactable = toggle;
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
    public void click_FixButton()
    {
        // 충돌 중인 건물이 있다면?
        if (checkCollide() == true)
            return;

        // 재화 검사 및 UI 띄워야 하는 함수.

        //

        change_BuilderMode("Default");
        StopMove();
    }
    public void click_CancelButton()
    {
        // 배치를 캔슬할 때 사용하는 함수.
        if (mouseIndicator != null)
            Destroy(mouseIndicator);

        // 재화 돌려받는 함수.

        //

        // Fix -> Default. Modify에서는 변경 X.
        if (builderMode == BuilderMode.Fix)
            change_BuilderMode("Default");
        StopMove();
    }
    public void click_QuitButton()
    {
        // 충돌 중인 건물이 있다면?
        if (checkCollide() == true)                    
            return;        

        // Modify -> Default.
        if (builderMode == BuilderMode.Relocate)
            change_BuilderMode("Default");
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
        // 데이타가 없다면 중지.
        if (selectedObjectIndex < 0)
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

        // 건물 생성 후 buildstate = true;
        //database.buildingsData[selectedObjectIndex].prefab.GetComponent<BuildingState>().built = true;
       
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
        cellIndicator_child.localScale = new Vector3(size.x, 0.001f, size.z);
        cellIndicator_child.localPosition = new Vector3(child_Pos.x, 0f, child_Pos.z);
    }

    public void set_BuildingInfo(int ID, bool state, int pollution)
    {
        selectedObjectIndex = database.buildingsData.FindIndex(data => data.ID == ID);
        // 데이타가 없다면 중지.
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        BuildingInfo_UI.SetActive(true);
        BuildingInfo[0].text = "Name : " + database  .buildingsData[selectedObjectIndex].name;
        BuildingInfo[1].text = "State : " + state;
        BuildingInfo[2].text = "Product : " + database.buildingsData[selectedObjectIndex].product;
        BuildingInfo[3].text = "Productivity : " + database.buildingsData[selectedObjectIndex].productivity;        
        BuildingInfo[4].text = "Pollution : " + pollution;

        // 슬라이더 값으로 조절할거니깐 나중에.
        //BuildingInfo[5].text = "Hungry : ";
        BuildingInfo[5].text = "Max_Product : "; // 최대생산량
    }

}
