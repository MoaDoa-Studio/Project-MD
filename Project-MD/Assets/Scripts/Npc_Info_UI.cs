using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Npc_Info_UI : MonoBehaviour
{
    // UI 세팅.
    string naming;
    int ID;
    float hungry;
    float exp;
    string state;
    int level;
    float affinity;
    GameObject prefab;

    public GameObject npc_info;
    [SerializeField]
    private TextMeshProUGUI[] NpcInfo; // npc Info.
  
    public void get_Values(string _name, int _ID, float _hungry, float _exp, string _state, int _level, float _Affiniity, GameObject _prefab)
    {
        naming = _name;
        ID = _ID;
        hungry = _hungry;
        exp = _exp;
        state = _state;
        level = _level;
        affinity = _Affiniity;
        prefab = _prefab;

        npc_info.gameObject.SetActive(true);

        // UI 하단 세팅
        NpcInfo[0].text = "Name : " +_name;
        NpcInfo[1].text = "Level : " +_level;
        NpcInfo[2].text = "Exp : " + _exp;
        NpcInfo[3].text = "Hungry : "  +_hungry;
        NpcInfo[4].text = "Affinity : " +_Affiniity;
        NpcInfo[5].text = "state : " +_state;
    }

}
