using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Npc_Moving : MonoBehaviour
{

    [HideInInspector]
    public float move_speed = 3f;

   
    // 생성할 자식 GameObject 프리팹
    public GameObject childPrefab;

    // 도착한 wayPoint 저장 변수
    private int wayPointIndex = 0;
    // 이동할 목표 지점
    private Transform target;


    // 이동확정될 wavepoint 저장변수
    private Transform[] target_wavepoint = new Transform[7];

    private void OnEnable()
    {
        // npc의 위치를 초기화시켜야함
         Vector3 this_character_pos = this.transform.position;

        npc_Build();

        
    }

    private void Start()
    {

        // 초기시에 0으로 설정
        wayPointIndex = 0;

        //wayPoint =  
    
    }

    

    // 도착시 다음 포인터의 정보를 가져와서 타겟 설정
    private void GetNextPoint()
    {
        // 종점 판별해서 도착하면 Enemy 오브젝트 Destroy
       // if(wayPointIndex == WayPoint.points.Length)
        {
            // 새로운 길을 만들어내는 함수

        }

        wayPointIndex++;
        
        //target = WayPoint.points[wayPointIndex];

    }

    public void npc_Build()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject newChild = Instantiate(childPrefab, this.gameObject.transform); // 부모 오브젝트의 자식 transform 생성
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(0, 0), Random.Range(-5f, 5f));
            newChild.transform.position = randomPosition;
            Debug.Log(i + "번째 위치는 : " + newChild.transform.position);

        }
    }

}
