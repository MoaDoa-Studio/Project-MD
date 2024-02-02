using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_datamanager : MonoBehaviour
{

    public List<GameObject> npc_List = new List<GameObject>();
    private int totalNpc;  // ��ü ���� ��.
    public GameObject selectedNpc; // ���� �ٷ�� �ִ� ���ӵ�����
    // NPC ������ ����,���� �� ȣ��Ǵ� �Լ�.
    public void changed_NpcData(string mode, GameObject _selectedNpc)
    {
        switch(mode)
        {
            // �ΰ��� ���� �� üũ �Լ�.
            case "Total":
                 count_NpcData();
                Debug.Log($"��ü ���ɼ��� : {totalNpc} �Դϴ�.");
                break;
                
            // ������Ʈ �߰�.
            case "Add":
                add_NpcData(_selectedNpc);
                Debug.Log($"�߰��� ������ {_selectedNpc} �Դϴ�.");
                break;

             //  ������Ʈ ����.
            case "Remove":
                remove_NpcData(_selectedNpc);
                break;


            case "Get":
                break;
        }


    }

    // ���� �ο� üũ
    [SerializeField]
    private void count_NpcData()
    {
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("NPC");
        totalNpc = npcObjects.Length;
        
    }

    // �ڱ� �ڽ��� ���� ������Ʈ ���� üũ
    private void count_childObject()
    {
        int childCount = transform.childCount;
        Debug.Log("�ڽ��� ���� ������Ʈ �� : " + childCount);
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
