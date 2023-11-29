using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc_Select_UI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> npc_ObjectList = new List<GameObject>();
    private List<int> npc_Id = new List<int>();
    private List<GameObject> buttonArr = new List<GameObject>(); // buttonArr ������ ��ư�迭 üũ
    public GameObject Content;
    public GameObject buttonprefab;
    void Update()
    {   // Npc �±׷� ã�� ������Ʈ �迭�� ����.
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("Npc");
       
        // npcObjects �迭�� �� ��Ҹ� npc_ObjectList�� �߰�.
        foreach (GameObject npcObj in npcObjects)
        {   
            // ���ο� ������Ʈ ���������� ����Ʈ�� �߰�.
            if (!npc_ObjectList.Contains(npcObj))
            {
                npc_ObjectList.Add(npcObj);
                Debug.Log("npc_ObjectList �߰��� ���� �̸� : " +  npcObj.GetComponent<NpcStat>().names);
                // �ش� ������ ���缭 Button�� ����.
                if (Content != null)
                {   // �θ��� �ڽ� ������Ʈ�� ����.
                    GameObject newButton = Instantiate(buttonprefab, Content.transform);
                    buttonArr.Add(newButton); //
                    newButton.GetComponentInChildren<Text>().text = npcObj.GetComponent<NpcStat>().names;
                }
            }
        }

        // ���� ��ư�� i���� �������鼭 �ش� npc �������� ������ => �ǹ� ���귮 ������Ʈ / �ʵ忡�� �ش� ������Ʈ ��������ϱ�
    }

}
