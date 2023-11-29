using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC ���� ����.
public class NpcStat : MonoBehaviour
{
    public NpcDatabaseSO npc_database;
    private NpcAI npcAI;

    private string names;
    private string state;
    private float affinity;
    private int iD;
    private int level;
    private float hungry;
    private float exp;
    private GameObject prefab;
    private Npc_Info_UI npcInfo;
    // Start is called before the first frame update
    void Start()
    {
        npcInfo = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Npc_Info_UI>();

        // DB �ʱ�ȭ
        int npcID = GetComponent<NpcAI>().ID;
        names = npc_database.npcData[npcID].name;
        state = npc_database.npcData[npcID].state;
        affinity = npc_database.npcData[npcID].Affinity;
        iD = npc_database.npcData[npcID].ID;
        level = npc_database.npcData[npcID].level;
        hungry = npc_database.npcData[npcID].hungry;
        exp = npc_database.npcData[npcID].exp;
        prefab = npc_database.npcData[npcID].prefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // npc UI ����ȭ ������ �ѱ�.
    private void OnMouseDown()
    {
        Debug.Log("npc UI�� ���Ƚ��ϴ�!");
        npcInfo.get_Values(npc_database.npcData[iD].name, npc_database.npcData[iD].ID,
        npc_database.npcData[iD].hungry, npc_database.npcData[iD].exp,
        npc_database.npcData[iD].state, npc_database.npcData[iD].level, npc_database.npcData[iD].Affinity, npc_database.npcData[iD].prefab);
    }
}
