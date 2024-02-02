using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_datamanager : MonoBehaviour
{

    public List<GameObject> npc_List = new List<GameObject>();
    private int totalNpc;  // 전체 정령 수.
    public GameObject selectedNpc; // 현재 다루고 있는 게임데이터
    // NPC 데이터 증감,감소 때 호출되는 함수.
    public void changed_NpcData(string mode, GameObject _selectedNpc)
    {
        switch(mode)
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
    [SerializeField]
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
}
