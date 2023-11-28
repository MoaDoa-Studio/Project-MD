using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingState : MonoBehaviour
{
    public GameObject state_icon;
    public Sprite spriteimg;
    public Image totalPdBar;
    private int ID;
    [SerializeField]
    private float making_cooltime = 5f;
    [SerializeField]
    private float reset_cooltime = 5f;
    private bool state = false;
    private string product;
    private int productivity;
    private int totalproductivity;
    private int max_productivitydefault; // 임시 default 값
    private Building building;
    private GameObject gameManager;
    private ResourceManager resourceManager;

    public BuildingDatabaseSO buildingDatabase; // DB

    public enum BuildingStat
    {
        Idle,
        Run,
        Stopwork
    }

    public BuildingStat buildingstat = BuildingStat.Run; // 기본상태로 초기화

    // Start is called before the first frame update
    void Start()
    {
        building = this.GetComponent<Building>();
        resourceManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ResourceManager>(); 
        ID = building.ID; // 프리팹의 ID값 DB로 빌딩스텟 관리 
        product = buildingDatabase.buildingsData[ID].product; // 생산하는 원소자원
        productivity = buildingDatabase.buildingsData[ID].productivity; // 건물 생산 속도
        max_productivitydefault = buildingDatabase.buildingsData[ID].max_productivity; // 최대 생산량
        
    }

    // Update is called once per frame
    void Update()
    {
        //슬라이더는 항상 보여줌
        totalPdBar.fillAmount = totalproductivity / max_productivitydefault;

        UpdateState();
    }

    public void UpdateState()
    {
        switch(buildingstat)
        {
            case BuildingStat.Idle:
                state_icon.SetActive(false);
                Running_build();
                break;
            case BuildingStat.Run:
                
                break;
            case BuildingStat.Stopwork:
                stoped_build();
                break;
        }
    }

    void Running_build()
    {
        // 가동중일때 나타나는 sprite image
        // state_icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Icons/cycling");
        
        // 총 생산량을 초과했을때 상태전환
        if(totalproductivity >= max_productivitydefault)
        {
            state_icon.SetActive(true);
            buildingstat = BuildingStat.Stopwork; // 그만 일하세욧!

        }

        // 생산량 쿨타임 적용
        if(making_cooltime <= 0)
        {
            // 생산을 합시다!! 
            int resourceID = getResourceID(product);
           
            totalproductivity += productivity;
            Debug.Log($"전체 :{max_productivitydefault} // 에서 현재 {totalproductivity}만큼 쌓여있습니다");
            // 최대 생산량 초과
            if(totalproductivity >= max_productivitydefault)
            {
                totalproductivity = max_productivitydefault;
                Debug.Log($"전체 보유랑을 {max_productivitydefault}를 초과하여 {totalproductivity}만큼 저장되었습니다");
            }
            making_cooltime = reset_cooltime; // 쿨타임 초기화
        }
        making_cooltime -= Time.deltaTime;
    }

    // 공장 생산량 초과로 npc가 대기중인 상탠
    void stoped_build()
    {   
        // 생산이 완료된 sprite image ?? 개발 미구현
        
        // 아이콘을 눌렀을 때 자원을 더함 => BuilderManager실행
        
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
}
