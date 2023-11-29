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
    GameObject prefab;

    public GameObject build_info;
    [SerializeField]
    private TextMeshProUGUI[] buildInfo; // 건물상태 Info.
    private BuildingDatabaseSO buildDB; // DB 
    private void Start()
    {
       
    }
    public void get_Values(string _name,int _level, int _ID, int _type, string _product, int _productivity, int _max_productivity, Vector3Int _size, GameObject _prefab)
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
    
        build_info.gameObject.SetActive(true);

        // UI 하단 세팅
        buildInfo[0].text = "Name : " + _name;
        buildInfo[1].text = "Level : " + _level;
        buildInfo[2].text = "Product : " + _product;
        buildInfo[3].text = "Productivity : " + _productivity;
        buildInfo[4].text = "Pollution : "; // 미구현
        buildInfo[5].text = "Hungry :" ; // 일하는 정령 npc의 만복도


    }

}
