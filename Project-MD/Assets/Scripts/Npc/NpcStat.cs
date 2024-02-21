using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC 스탯 관리.
public class NpcStat : MonoBehaviour
{
    public NpcDatabaseSO npc_database;
    [HideInInspector]
    public string names;
    [SerializeField]
    private int jin_Id;
    [SerializeField]
    private float W_affinity;
    [SerializeField]
    private float F_affinity;
    [SerializeField]
    private float E_affinity;
    [SerializeField]
    private float G_affinity;
    [SerializeField]
    private int level = 1; // 정령 1레벨 초기화.
    
    private string state;
    private float hunger;
    private float levelingexp;

    private NpcAI npcAI;
    private GameObject prefab;
    private Npc_Info_UI npcInfo;
    private Npc_datamanager npcdata;
    private Npc_dataLoader npcLoader;

    // Start is called before the first frame update
    void Start()
    {
        npcLoader = GameObject.FindGameObjectWithTag("NpcManager").GetComponent<Npc_dataLoader>();
        npcdata = GameObject.FindGameObjectWithTag("NpcManager").GetComponent<Npc_datamanager>();
       
        npcdata.Setaschild(this.gameObject);

        // 특성값을 만들어내는 함수.
        attribute_Affinity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 정령 생성시 데이터 호출 및 저장.
    public void Get_Infovalue(int _number)
    {
        name = npcLoader.npcDatas[_number].Ef1; //진화 1폼이름으로 설정
        jin_Id = npcLoader.npcDatas[_number].Jin_id;
        hunger = npcLoader.levelDatas[level].hunger;
        levelingexp = npcLoader.levelDatas[level].ReqLev;
    }

    private void attribute_Affinity()
    {

    }
    // npc UI 동기화 정보값 넘김.
    private void OnMouseDown()
    {
        
    }
}
