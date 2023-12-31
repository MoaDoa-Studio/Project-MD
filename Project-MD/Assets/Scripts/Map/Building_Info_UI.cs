using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Building_Info_UI : MonoBehaviour
{
    // UI 세팅.
    string naming;
    int iD;
    int level;
    int type;
    string product;
    int productivity;
    int max_productivity;
    bool set;
    Vector3Int size;
    GameObject prefab; // 건물 프리팹.
    GameObject original; // 상호작용 건물.

    [HideInInspector]
    public GameObject select_UI;
    //[HideInInspector]
    public GameObject build_info;
    public GameObject done;
    [SerializeField]
    private TextMeshProUGUI[] buildInfo; // 건물상태 Info.
    private BuildingDatabaseSO buildDB; // DB 
    private BuildingState buildState;
    private ResourceManager resourceManager;

    private void Start()
    {
        buildState = this.GetComponent<BuildingState>();
        resourceManager = GetComponent<ResourceManager>();
       
    }

    private void Update()
    {   // 건물 레별별 동기화 x
        if (set == true)
        {
            buildInfo[6].GetComponentInChildren<Slider>().value = original.GetComponent<BuildingState>().totalproductivity / (float)max_productivity;            
        }
        if (buildInfo[6].GetComponentInChildren<Slider>().value == 1)
            done.SetActive(true);      
        
    }
    public void get_Values(string _name,int _level, int _ID, int _type, string _product, int _productivity, int _max_productivity, Vector3Int _size, GameObject _prefab, GameObject _original)
    {
        this.GetComponent<Npc_Select_UI>().select_Buildsync(original);  //=> 말그대로 prefab만 전달해서 문제가 생김
        
        naming = _name;
        level = _level;
        iD = _ID;
        type = _type;
        product = _product;
        productivity = _productivity;
        max_productivity = _max_productivity;
        size = _size;
        prefab = _prefab;
        original = _original;

        //build_info.gameObject.SetActive(true);

        // UI 하단 세팅
        buildInfo[0].text = "Name : " + _name;
        buildInfo[1].text = "Level : " + _level;
        buildInfo[2].text = "Product : " + _product;
        buildInfo[3].text = "Productivity : " + _productivity;
        buildInfo[4].text = "Pollution : "; // 미구현
        buildInfo[5].text = "Hungry :" ; // 일하는 정령 npc의 만복도
        buildInfo[6].text = "Total Resource :" ; // 일하는 정령 npc의 만복도

        set = true;
    }

    // Npc_select_UI와 buildstat 동기화.
    public void set_NpcselectUI()
    {
        select_UI.SetActive(true);
    }

    public void inquire()
    {
        int resourceID = getResourceID(product);
        resourceManager.first_Source[resourceID] += max_productivity;
        original.GetComponent<BuildingState>().totalproductivity = 0;
        done.SetActive(false);
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
