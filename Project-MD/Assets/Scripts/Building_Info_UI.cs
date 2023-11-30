using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Building_Info_UI : MonoBehaviour
{
    // UI ����.
    string naming;
    int iD;
    int level;
    int type;
    string product;
    int productivity;
    int max_productivity;
    Vector3Int size;
    GameObject prefab; // �ǹ� ������.
    GameObject original; // ��ȣ�ۿ� �ǹ�.

    [HideInInspector]
    public GameObject select_UI;
    [HideInInspector]
    public GameObject build_info;

    [SerializeField]
    private TextMeshProUGUI[] buildInfo; // �ǹ����� Info.
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

        // UI �ϴ� ����
        buildInfo[0].text = "Name : " + _name;
        buildInfo[1].text = "Level : " + _level;
        buildInfo[2].text = "Product : " + _product;
        buildInfo[3].text = "Productivity : " + _productivity;
        buildInfo[4].text = "Pollution : "; // �̱���
        buildInfo[5].text = "Hungry :" ; // ���ϴ� ���� npc�� ������


    }

    // Npc_select_UI�� buildstat ����ȭ.
    public void set_NpcselectUI()
    {
        select_UI.SetActive(true);
        // � ���� stat���� �����������
        this.GetComponent<Npc_Select_UI>().select_Buildsync(original);  //=> ���״�� prefab�� �����ؼ� ������ ����
        Debug.Log($"selectUI�� {prefab}�� �߰���");
       
    }
}
