using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ������ Ŭ����
public class GameObjectInfoResult
{
    public int objectCount;
    public List<int> objectInfoList;
}

public class Npc_Data : ScriptableObject
{  
    [field: SerializeField]
    public string name { get; set; } //�̸�
    [field: SerializeField]
    public int Jin_id { get;  set; } // ���� ��ü ��ȣ
    [field: SerializeField]
    public string personality { get; set; } // ����
    [field: SerializeField]
    public float hunger { get; set; } //������
    [field: SerializeField]
    public float exp { get; set; } //����ġ
    [field: SerializeField]
    public string state { get; set; } //���� (������, ���ϴ���..)
    [field: SerializeField]
    public int JinLev { get; set; } //���� ����

    [field: SerializeField]
    public int ReqLev { get; set; } //���� �ʿ����ġ
    [field: SerializeField]
    public float F_affinity { get; set; } // Ư�� �� ģȭ��
    [field: SerializeField]
    public float W_affinity { get; set; } // Ư�� �� ģȭ��
    [field: SerializeField]
    public float E_affinity { get; set; } // Ư�� ���� ģȭ��
    [field: SerializeField]
    public float G_affinity { get; set; } // Ư�� �� ģȭ��
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
    public int JinLev { get; set; } //�̸�
    [field: SerializeField]
    public int ReqLev { get; set; } //�̸�
   
    [field: SerializeField]
    public float hunger { get; set; } //�̸�
}
public class Npc_datamanager : MonoBehaviour
{
    public List<GameObject> npc_List = new List<GameObject>();
    private int totalNpc;  // ��ü ���� ��.
    public GameObject selectedNpc; // ���� �ٷ�� �ִ� ���ӵ�����
    // NPC ������ ����,���� �� ȣ��Ǵ� �Լ�.
    public void changed_NpcData(string mode, GameObject _selectedNpc)
    {
        switch (mode)
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

    // ���� ������ ���� �ҷ����� �Լ� //�� ���� ������ �� ���ɵ��� List���� ��ȯ��
    public List<GameObject> get_totalNpcValues()
    {
        GameObjectInfoResult result = DisplayGameObjectInfo(npc_List);

        Debug.Log(npc_List.Count); // ���� ������Ʈ �� ����.

        return npc_List;
        // ���� �ʵ忡 �����ϴ� ������Ʈ�� id ���� List�� �޾Ƶ���
        // ������ id������ NPCDatabasaeSO �����͸� �ҷ����� �� ����      
    }

    // Npc �ʱ�ȭ ��ư �����ÿ� ���� ���� üũ �Լ�.
    public GameObjectInfoResult DisplayGameObjectInfo(List<GameObject> _gameObjectList)
    {
        // Npc �±׷� ã�� ������Ʈ �迭�� ����.
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("Npc");

        // npcObjects �迭�� �� ��Ҹ� npc_ObjectList�� �߰�.
        for (int i = 0; i < npcObjects.Length; i++)
        {
            GameObject npcObj = npcObjects[i];

            // ���ο� ������Ʈ ���������� ����Ʈ�� �߰�.
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

    // ���� ������Ʈ�� ����.
    public void Setaschild(GameObject _gameObject)
    {
        _gameObject.transform.parent = this.transform;
    }
}
