using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 결과를 저장할 클래스
public class GameObjectInfoResult
{
    public int objectCount;
    public List<int> objectInfoList;
}

public class Npc_Data : ScriptableObject
{  
    [field: SerializeField]
    public string name { get; set; } //이름
    [field: SerializeField]
    public int Jin_id { get;  set; } // 고유 객체 번호
    [field: SerializeField]
    public string personality { get; set; } // 성격
    [field: SerializeField]
    public float hunger { get; set; } //만복도
    [field: SerializeField]
    public float exp { get; set; } //경험치
    [field: SerializeField]
    public string state { get; set; } //상태 (쉬는중, 일하는중..)
    [field: SerializeField]
    public int JinLev { get; set; } //정령 레벨

    [field: SerializeField]
    public int ReqLev { get; set; } //정령 필요경험치
    [field: SerializeField]
    public float F_affinity { get; set; } // 특성 불 친화력
    [field: SerializeField]
    public float W_affinity { get; set; } // 특성 물 친화력
    [field: SerializeField]
    public float E_affinity { get; set; } // 특성 전기 친화력
    [field: SerializeField]
    public float G_affinity { get; set; } // 특성 땅 친화력
    [field: SerializeField]
    public string D1 { get; set; }
    [field: SerializeField]
    public string D2 { get; set; }
    [field: SerializeField]
    public string D3 { get; set; }
    [field: SerializeField]
    public string EF1 { get; set; }
    [field: SerializeField]
    public string EF2 { get; set; }
    [field: SerializeField]
    public string EF3 { get; set; }
    
}

public class Level_Data : ScriptableObject
{
    [field: SerializeField]
    public int JinLev { get; set; } //이름
    [field: SerializeField]
    public int ReqLev { get; set; } //이름
   
    [field: SerializeField]
    public float hunger { get; set; } //이름
}
public class Npc_datamanager : MonoBehaviour
{
    public List<GameObject> npc_List = new List<GameObject>();
    private int totalNpc;  // 전체 정령 수.
    public GameObject selectedNpc; // 현재 다루고 있는 게임데이터
    // NPC 데이터 증감,감소 때 호출되는 함수.
    public void changed_NpcData(string mode, GameObject _selectedNpc)
    {
        switch (mode)
        {
            // 인게임 정령 수 체크 함수.
            case "Total":
                count_NpcData();
                Debug.Log($"전체 정령수는 : {totalNpc} 입니다.");
                break;

            // 오브젝트 추가.
            case "Add":
                add_NpcData(_selectedNpc);
                Debug.Log($"추가된 정령은 {_selectedNpc} 입니다.");
                break;

            //  오브젝트 제거.
            case "Remove":
                remove_NpcData(_selectedNpc);
                break;


            case "Get":
                break;
        }


    }

    // 정령 인원 체크
    private void count_NpcData()
    {
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("NPC");
        totalNpc = npcObjects.Length;

    }

    // 자기 자신의 하위 오브젝트 수로 체크
    private void count_childObject()
    {
        int childCount = transform.childCount;
        Debug.Log("자신의 하위 오브젝트 수 : " + childCount);
    }

    private void add_NpcData(GameObject _addingNpc)
    {
        npc_List.Add(_addingNpc);
    }

    private void remove_NpcData(GameObject _removeNpc)
    {
        npc_List.Remove(_removeNpc);
    }

    // 정령 데이터 값을 불러오는 함수 //총 정령 갯수와 그 정령들의 List값을 반환함
    public List<GameObject> get_totalNpcValues()
    {
        GameObjectInfoResult result = DisplayGameObjectInfo(npc_List);

        Debug.Log(npc_List.Count); // 게임 오브젝트 총 갯수.

        return npc_List;
        // 현재 필드에 존재하는 오브젝트의 id 값을 List로 받아들임
        // 정령의 id값으로 NPCDatabasaeSO 데이터를 불러들일 수 있음      
    }

    // Npc 초기화 버튼 눌릴시에 정령 유무 체크 함수.
    public GameObjectInfoResult DisplayGameObjectInfo(List<GameObject> _gameObjectList)
    {
        // Npc 태그로 찾은 오브젝트 배열로 받음.
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("Npc");

        // npcObjects 배열의 각 요소를 npc_ObjectList에 추가.
        for (int i = 0; i < npcObjects.Length; i++)
        {
            GameObject npcObj = npcObjects[i];

            // 새로운 오브젝트 있을때마다 리스트에 추가.
            if (!npc_List.Contains(npcObj))
            {
                npc_List.Add(npcObj);
            }
        }

        int objectCount = npc_List.Count;
        List<int> objectInfoList = new List<int>();

        for(int i = 0; i< objectCount; i++) 
        {
            GameObject obj = npc_List[i];
        }

        return new GameObjectInfoResult
        {
            objectCount = objectInfoList.Count,
            objectInfoList = objectInfoList
        };
    }

    // 하위 오브젝트로 설정.
    public void Setaschild(GameObject _gameObject)
    {
        _gameObject.transform.parent = this.transform;
    }
}
