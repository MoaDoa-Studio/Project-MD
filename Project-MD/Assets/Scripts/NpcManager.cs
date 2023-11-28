using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField]
    private GameObject NpcInfo_UI; // 건물 상태창 UI.
    [SerializeField]
    private TextMeshProUGUI[] NpcInfo; // 건물 상태창 인포.
    [SerializeField]
    public NpcDatabaseSO database; // 빌딩 DB 
    private int selectednpcIndex = -1; // 선택된 npc -1 초기화


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set_NpcInfo(int ID, int Affinity)
    {
        selectednpcIndex = database.npcData.FindIndex(data => data.ID == ID);
        // 데이타가 없다면 중지.
        if (selectednpcIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        NpcInfo_UI.SetActive(true);
        NpcInfo[0].text = "Name : " + database.npcData[selectednpcIndex].name;
        NpcInfo[4].text = "Affinity : " + Affinity;
        NpcInfo[1].text = "Level : " + database.npcData[selectednpcIndex].level;
        NpcInfo[2].text = "Exp : " + database.npcData[selectednpcIndex].exp;
        NpcInfo[3].text = "Hungry : " + database.npcData[selectednpcIndex].hungry;
        NpcInfo[5].text = "State : " + database.npcData[selectednpcIndex].state;

        // 슬라이더 값으로 조절할거니깐 나중에.
        //BuildingInfo[5].text = "Hungry : ";
        //BuildingInfo[6].text = "EXP : ";
    }
}
