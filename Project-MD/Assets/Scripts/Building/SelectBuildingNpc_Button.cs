using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectBuildingNpc_Button : MonoBehaviour
{
    [SerializeField]
    private GameObject Info;

    private NpcStat currentNpcStat;
    public void SetButtonData(GameObject npc)
    {
        currentNpcStat = npc.GetComponent<NpcStat>();        
        Info.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentNpcStat.GetName();
        Info.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentNpcStat.GetLevel().ToString();
        Info.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = currentNpcStat.GetPersonality();
        Info.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = currentNpcStat.GetJinId().ToString();
    }
    
    public void PlaceNpc()
    {
        int FindPID = GameManager.instance.builderManager.currentBuildingPID;
        Factory currentFactory = (Factory)GameDataManager.instance.GetBuildingData(FindPID); // ���� �ٿ� ĳ����.
        
        if (currentFactory == null)
        {
            Debug.LogError("ã�� �� ���� ����");
            return;
        }

        currentFactory.currentNpcID = currentNpcStat.GetPrimaryID();
        Debug.Log($"selectbutton level : {currentFactory.currentNpcID}");
    }
}
