using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc_Select_UI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> npc_ObjectList = new List<GameObject>();
    private List<int> npc_Id = new List<int>();
    private List<GameObject> buttonArr = new List<GameObject>(); // buttonArr 생성된 버튼배열 체크
    public GameObject Content;
    public GameObject buttonprefab;
    void Update()
    {   // Npc 태그로 찾은 오브젝트 배열로 받음.
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("Npc");
       
        // npcObjects 배열의 각 요소를 npc_ObjectList에 추가.
        foreach (GameObject npcObj in npcObjects)
        {   
            // 새로운 오브젝트 있을때마다 리스트에 추가.
            if (!npc_ObjectList.Contains(npcObj))
            {
                npc_ObjectList.Add(npcObj);
                Debug.Log("npc_ObjectList 추가된 정령 이름 : " +  npcObj.GetComponent<NpcStat>().names);
                // 해당 갯수에 맞춰서 Button을 생성.
                if (Content != null)
                {   // 부모의 자식 오브젝트로 생성.
                    GameObject newButton = Instantiate(buttonprefab, Content.transform);
                    buttonArr.Add(newButton); //
                    newButton.GetComponentInChildren<Text>().text = npcObj.GetComponent<NpcStat>().names;
                }
            }
        }

        // 눌린 버튼의 i값을 가져오면서 해당 npc 정보값을 가져옴 => 건물 생산량 업데이트 / 필드에서 해당 오브젝트 사라지게하기
    }

}
