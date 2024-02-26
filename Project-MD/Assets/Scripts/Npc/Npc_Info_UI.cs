using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Npc_Info_UI : MonoBehaviour
{
    // UI 세팅.
    string naming;
    int Level;
    string personality;
    float productivity;
    float hunger_Req;
    float req_Exp;
    float nowexp;
    float W_affinity;
    float F_affinity;
    float E_affinity;
    float G_affinity;
    string D1;
    string D2;
    string D3;

    public Slider expSlider;
    public GameObject npc_info;
    [SerializeField]
    private TextMeshProUGUI[] NpcInfo; // npc Info.
    [SerializeField]
    private GameObject selectedGameObject;
    public void get_Values(string _name, int level, string _personality, float _productivity, float _hungry, float _exp, float w_affinity,float f_affinity, float e_affinity, float g_affinity , string _D1, string _D2, string _D3, GameObject _gameObject)
    {
        naming = _name;
        personality = _personality;
        productivity = _productivity;
        hunger_Req = _hungry;
        req_Exp = _exp;
        Level = level;
        W_affinity = w_affinity;
        F_affinity = f_affinity;
        E_affinity = e_affinity;
        G_affinity = g_affinity;
        D1 = _D1;
        D2 = _D2;
        D3 = _D3;
        
        selectedGameObject = _gameObject;
        npc_info.gameObject.SetActive(true);

        // UI 하단 세팅
        NpcInfo[0].text = naming; 
        NpcInfo[1].text = Level.ToString();
        NpcInfo[2].text =  personality;
        NpcInfo[3].text =  productivity.ToString();
        NpcInfo[4].text =  D1;
        NpcInfo[5].text =  D2;
        NpcInfo[6].text =  D3;
       
    }

    //UI 상에서 경험치 입력
    public void gain_Exp(float _nowexp)
    {
        nowexp += _nowexp;
        selectedGameObject.GetComponent<NpcStat>().nowexp += _nowexp;
        if(expSlider != null)
        {
            if(nowexp >= req_Exp)
            {
                nowexp = 0;
            }
            expSlider.value = nowexp / req_Exp;
        }

    }

}
