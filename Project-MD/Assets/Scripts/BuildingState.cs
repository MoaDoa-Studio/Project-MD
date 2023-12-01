using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingState : MonoBehaviour
{
    [SerializeField]
    private float making_cooltime = 5f;
    [SerializeField]
    private float reset_cooltime = 5f;
    [SerializeField]
    public GameObject chosen_Npc = null;   // 공장에서 일하는 npc
    public BuildingDatabaseSO buildingDatabase; // 빌딩 DB
    public bool built = false; // 건물 최초 건설값
    private int ID;
    private int productivity;
    private int bonus_productivity; // npc 추가 효과
    public int totalproductivity;
    private int max_productivitydefault; // 임시 최대 생산량.
    private string product;
    private Building building;
    private Building_Info_UI buildInfo;
    private GameObject Building_Info; // 건물 상태창 UI Info.
    private GameObject gameManager;
    private ResourceManager resourceManager;
    private BuilderManager builderManager;
    private Npc_Select_UI npcSelect;
    public delegate void OnNpcChangedHandler(GameObject newNpc, GameObject oldNpc);
    public event OnNpcChangedHandler onNpcChanged; // delegate 정의
    // Builder 하위 UI들.
    private Image totalPdBar;
    public GameObject state_icon; // 공장 가동, 생산완료 아이콘.
    public GameObject select_UI;
    
    public enum BuildingStat
    {
        RunwithNpc,
        Run,
        Stopwork
    }

    private BuildingStat buildingstat = BuildingStat.Run; // 기본상태로 초기화

    void Start()
    {
        building = this.GetComponent<Building>();
        npcSelect = this.GetComponent<Npc_Select_UI>();
        buildInfo = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Building_Info_UI>();
        resourceManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ResourceManager>();
        builderManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BuilderManager>();   
        ID = building.ID; // 프리팹의 ID값 DB로 빌딩스텟 관리 
        product = buildingDatabase.buildingsData[ID].product; // 생산하는 원소자원
        productivity = buildingDatabase.buildingsData[ID].productivity; // 건물 생산 속도
        max_productivitydefault = buildingDatabase.buildingsData[ID].max_productivity; // 최대 생산량

    }

    void Update()
    {
       // Swicth문 벗어남.

        UpdateState();
    }

    public void UpdateState()
    {
        switch(buildingstat)
        {
            case BuildingStat.RunwithNpc:
                Debug.Log("Run && Npc 상태");
                // 총 생산량 초과
                finishBuild();
                run_Build();

                break;
            case BuildingStat.Run:
                Debug.Log("Run 상태 실행됨");
                Debug.Log("현재 가공에서 일하고 있는 정령의 이름은 : " + chosen_Npc);           
                finishBuild(); // 생산 종료
                if(chosen_Npc != null)
                   buildingstat = BuildingStat.RunwithNpc;  
               else
                   run_Build();
 
                break;
            case BuildingStat.Stopwork:
                Debug.Log("StopWork가 실행됨");
                
                break;
        }
    }

    void run_Build()
    {
        //슬라이더 항상.
        //totalPdBar.fillAmount = (float)totalproductivity / (float)max_productivitydefault;
        
        // 생산량 쿨타임 적용.
        if(making_cooltime <= 0)
        {
            // 생산을 합시다!! 
            int resourceID = getResourceID(product);
           
            totalproductivity += productivity;
            Debug.Log($"전체 :{max_productivitydefault} // 에서 현재 {totalproductivity}만큼 쌓여있습니다");
            // 최대 생산량 초과.
            if(totalproductivity >= max_productivitydefault)
            {
                totalproductivity = max_productivitydefault;
                Debug.Log($"전체 보유랑을 {max_productivitydefault}를 초과하여 {totalproductivity}만큼 저장되었습니다");

            }
            making_cooltime = reset_cooltime; // 쿨타임 초기화
        }
        making_cooltime -= Time.deltaTime;
    }

    // 공장 생산량 초과로 npc 대기중.
    void finishBuild()
    {
        if (totalproductivity >= max_productivitydefault)
        {
            buildingstat = BuildingStat.Stopwork; // 그만 일하세욧!

        }

    }  // select UI에서 호출.
    public void get_buildNpcInfo(GameObject _npcObject)
    {
        GameObject oldNpc = chosen_Npc; // 이전 NPC 저장
        chosen_Npc = _npcObject;    // 새로운 NPC로 업데이트
        Debug.Log("Chosen_npc는 : " +  chosen_Npc);

        // 이벤트 호출 (Npc 변경시)

        if (onNpcChanged != null)
        {
            onNpcChanged.Invoke(chosen_Npc, oldNpc);
            
        }
        else
            onNpcChanged = OnNpcUpdated;
    }

    private void OnNpcUpdated(GameObject newNpc, GameObject oldNpc)
    {

    }

    private int getResourceID(string str)
    {
        // 리소스에 임의로 배정된 ID 리턴 함수.
        switch (str)
        {
            case "Fire":
                return 0;
            case "Water":
                return 1;
            case "Spark":
                return 2;
            case "Ground":
                return 3;
        }
        return -1;
    }

    // Build_Info UI 세팅
    private void OnMouseDown()
    {
        Debug.Log("build UI가 눌렸습니다!");
        buildInfo.get_Values(buildingDatabase.buildingsData[ID].name, buildingDatabase.buildingsData[ID].level,
        buildingDatabase.buildingsData[ID].ID, buildingDatabase.buildingsData[ID].type,
        buildingDatabase.buildingsData[ID].product, buildingDatabase.buildingsData[ID].
        productivity, buildingDatabase.buildingsData[ID].max_productivity,
        buildingDatabase.buildingsData[ID].size, buildingDatabase.buildingsData[ID].prefab, this.gameObject);
    }
}
