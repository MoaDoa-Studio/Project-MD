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
    private Button[] npcButtons; // buttonArr 생성된 버튼배열 체크
    public GameObject Content;
    public GameObject buttonprefab;
    [SerializeField]
    private GameObject selectedNpc; // 일하는 정령
    [SerializeField]
    private GameObject buildprefab; // 선택된 건물

   
    void Update()
    {   // Npc 태그로 찾은 오브젝트 배열로 받음.
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("Npc");
       
        // npcObjects 배열의 각 요소를 npc_ObjectList에 추가.
        for(int i = 0; i < npcObjects.Length; i++)
        {
            GameObject npcObj = npcObjects[i];
            // 새로운 오브젝트 있을때마다 리스트에 추가.
            if (!npc_ObjectList.Contains(npcObj))
            {
                npc_ObjectList.Add(npcObj);
                Debug.Log("npc_ObjectList 추가된 정령 이름 : " +  npcObj.GetComponent<NpcStat>().names);
                // 해당 갯수에 맞춰서 Button을 생성.
                if (Content != null)
                {   // 부모의 자식 오브젝트로 생성.
                    GameObject button = Instantiate(buttonprefab, Content.transform);
                    button.GetComponentInChildren<Text>().text = npcObj.GetComponent<NpcStat>().names;
                    int index = i;
                    npcButtons = Content.GetComponentsInChildren<Button>();
                    npcButtons[i].onClick.AddListener(() => OnButtonClick(index));  
                }
            }




        
           
        }

        // 눌린 버튼의 i값을 가져오면서 해당 npc 정보값을 가져옴 => 건물 생산량 업데이트 / 필드에서 해당 오브젝트 사라지게하기
    }

    // UI 버튼 클릭 되어졌을 때.
    void OnButtonClick(int buttonIndex)
    {
        Debug.Log("Button clicked! Index: " + buttonIndex);
        Debug.Log("선택되어진 npc는 : " + npc_ObjectList[buttonIndex]);
        selectedNpc = npc_ObjectList[buttonIndex];
        // 클릭 누르기 전에 동기화
        buildingState = buildprefab.GetComponent<BuildingState>();
        buildingState.get_buildNpcInfo(selectedNpc);
    }
    public void select_Buildsync(GameObject _buildObj)
    {
        buildprefab = _buildObj;
        Debug.Log("select와 건물이 연결이 완료되었음");
    }
   

}
