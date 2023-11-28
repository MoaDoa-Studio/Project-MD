using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField]
    private GameObject NpcInfo_UI; // �ǹ� ����â UI.
    [SerializeField]
    private TextMeshProUGUI[] NpcInfo; // �ǹ� ����â ����.
    [SerializeField]
    public NpcDatabaseSO database; // ���� DB 
    private int selectednpcIndex = -1; // ���õ� npc -1 �ʱ�ȭ


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
        // ����Ÿ�� ���ٸ� ����.
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

        // �����̴� ������ �����ҰŴϱ� ���߿�.
        //BuildingInfo[5].text = "Hungry : ";
        //BuildingInfo[6].text = "EXP : ";
    }
}
