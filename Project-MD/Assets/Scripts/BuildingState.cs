using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    private int ID;
    [SerializeField]
    private float making_cooltime = 5f;
    [SerializeField]
    private float reset_cooltime = 5f;
    private bool state = false;
    private string product;
    private int productivity;
    private float max_productivity;
    private Building building;
    private GameObject gameManager;
    private ResourceManager resourceManager;

    public BuildingDatabaseSO buildingDatabase; // DB

    public enum BuildingStat
    {
        Idle,
        Run,
        Stop
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
        max_productivity = buildingDatabase.buildingsData[ID].max_productivity; // 최대 생산량
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    public void UpdateState()
    {
        switch(buildingstat)
        {
            case BuildingStat.Idle:
                Running_build();
                break;
            case BuildingStat.Run:
                
                break;
            case BuildingStat.Stop:
             
                break;
        }
    }

    void Running_build()
    {
        if(making_cooltime <= 0)
        {
            // 생산을 합시다!! //value 값에 생산할 product, productivity 넣고 
            resourceManager.add_First_ResourceValue(product, productivity);
            Debug.Log($"생산량은 {productivity}, 이며 생산하는 물건은 {product}입니다");
            making_cooltime = reset_cooltime; // 쿨타임 초기화
        }
        making_cooltime -= Time.deltaTime;
    }
}
