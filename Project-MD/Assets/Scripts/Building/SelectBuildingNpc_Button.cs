using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectBuildingNpc_Button : MonoBehaviour
{
    [SerializeField]
    private GameObject Info;
    public void SetButtonData(GameObject npc)
    {        
        string[] currentNpcStat = npc.GetComponent<NpcStat>().GetSelectBuildingNpcInfo();
        Info.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentNpcStat[0];
        Info.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentNpcStat[1];
        Info.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = currentNpcStat[2];
        Info.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = currentNpcStat[3];
    }
}
