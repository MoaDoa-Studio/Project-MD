using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Npc_Select_UI : MonoBehaviour
{
    int number = 0;

    [SerializeField]
    public List<GameObject> npc_ObjectList = new List<GameObject>();
    private List<int> npc_Id = new List<int>();
    private BuildingState buildingState;
    private Button[] npcButtons; // buttonArr ������ ��ư�迭 üũ
    public GameObject Content;
    public GameObject buttonprefab;
    [SerializeField]
    private GameObject selectedNpc; // ���ϴ� ����
    [SerializeField]
    private GameObject buildprefab; // ���õ� �ǹ�

   
    void Update()
    {   // Npc �±׷� ã�� ������Ʈ �迭�� ����.
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("Npc");
       
        // npcObjects �迭�� �� ��Ҹ� npc_ObjectList�� �߰�.
        for(int i = 0; i < npcObjects.Length; i++)
        {
            GameObject npcObj = npcObjects[i];
            // ���ο� ������Ʈ ���������� ����Ʈ�� �߰�.
            if (!npc_ObjectList.Contains(npcObj))
            {
                npc_ObjectList.Add(npcObj);
                Debug.Log("npc_ObjectList �߰��� ���� �̸� : " +  npcObj.GetComponent<NpcStat>().names);
                // �ش� ������ ���缭 Button�� ����.
                if (Content != null)
                {   // �θ��� �ڽ� ������Ʈ�� ����.
                    GameObject button = Instantiate(buttonprefab, Content.transform);
                    button.GetComponentInChildren<Text>().text = npcObj.GetComponent<NpcStat>().names;
                    int index = i;
                    npcButtons = Content.GetComponentsInChildren<Button>();
                    npcButtons[i].onClick.AddListener(() => OnButtonClick(index));  
                }
            }




        
           
        }

        // ���� ��ư�� i���� �������鼭 �ش� npc �������� ������ => �ǹ� ���귮 ������Ʈ / �ʵ忡�� �ش� ������Ʈ ��������ϱ�
    }

    // UI ��ư Ŭ�� �Ǿ����� ��.
    void OnButtonClick(int buttonIndex)
    {
        Debug.Log("Button clicked! Index: " + buttonIndex);
        Debug.Log("���õǾ��� npc�� : " + npc_ObjectList[buttonIndex]);
        selectedNpc = npc_ObjectList[buttonIndex];
        // Ŭ�� ������ ���� ����ȭ
        buildingState = buildprefab.GetComponent<BuildingState>();
        buildingState.get_buildNpcInfo(selectedNpc);
    }
    public void select_Buildsync(GameObject _buildObj)
    {
        buildprefab = _buildObj;
        Debug.Log("select�� �ǹ��� ������ �Ϸ�Ǿ���");
    }
   

}
