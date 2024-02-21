using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC ���� ����.
public class NpcStat : MonoBehaviour
{
    public NpcDatabaseSO npc_database;
    [HideInInspector]
    public string names;
    private string state;
    private float affinity;
    private int iD;
    private int level;
    private float hungry;
    private float exp;

    private NpcAI npcAI;
    private GameObject prefab;
    private Npc_Info_UI npcInfo;
    [SerializeField]
    private GameObject npcManager;
    private Npc_datamanager npcdata;
    // Start is called before the first frame update
    void Start()
    {
        npcInfo = GameObject.FindGameObjectWithTag("NpcManager").GetComponent<Npc_Info_UI>();
        npcdata = npcManager.GetComponent<Npc_datamanager>();
        npcdata.Setaschild(this.gameObject);
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���� ������ ȣ�� �� ����.
    public void Get_Infovalue(int _number)
    {
       
    }

    // npc UI ����ȭ ������ �ѱ�.
    private void OnMouseDown()
    {
        Debug.Log("npc UI�� ���Ƚ��ϴ�!");
        npcInfo.get_Values(npc_database.npcData[iD].name, npc_database.npcData[iD].ID,
        npc_database.npcData[iD].hungry, npc_database.npcData[iD].exp,
        npc_database.npcData[iD].state, npc_database.npcData[iD].level, npc_database.npcData[iD].Affinity_w, npc_database.npcData[iD].prefab);
    }
}
