using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    Vector3Int size;
    GameObject prefab; // 건물 프리팹.
    GameObject original; // 상호작용 건물.

    [HideInInspector]
    public GameObject select_UI;
    [HideInInspector]
    public GameObject build_info;

    [SerializeField]
    private TextMeshProUGUI[] buildInfo; // 건물상태 Info.
    private BuildingDatabaseSO buildDB; // DB 
    private BuildingState buildState; 

    private void Start()
    {
        buildState = this.GetComponent<BuildingState>();  
    }
    public void get_Values(string _name,int _level, int _ID, int _type, string _product, int _productivity, int _max_productivity, Vector3Int _size, GameObject _prefab, GameObject _original)
    {
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

        build_info.gameObject.SetActive(true);

        // UI 하단 세팅
        buildInfo[0].text = "Name : " + _name;
        buildInfo[1].text = "Level : " + _level;
        buildInfo[2].text = "Product : " + _product;
        buildInfo[3].text = "Productivity : " + _productivity;
        buildInfo[4].text = "Pollution : "; // 미구현
        buildInfo[5].text = "Hungry :" ; // 일하는 정령 npc의 만복도


    }

    // Npc_select_UI와 buildstat 동기화.
    public void set_NpcselectUI()
    {
        select_UI.SetActive(true);
        // 어떤 빌딩 stat인지 전달해줘야함
        this.GetComponent<Npc_Select_UI>().select_Buildsync(original);  //=> 말그대로 prefab만 전달해서 문제가 생김
        Debug.Log($"selectUI로 {prefab}이 추가됨");
       
    }
}
