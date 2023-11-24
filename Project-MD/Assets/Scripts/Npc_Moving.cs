using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Moving : MonoBehaviour
{

    [HideInInspector]
    public float move_speed = 3f;

    public float  startSpeed = 7f;

    // 도착한 wayPoint 저장 변수
    private int wayPointIndex = 0;
    // 이동할 목표 지점
    private Transform target;

    private void Start()
    {
        // 초기시에 0으로 설정
        wayPointIndex = 0;

        // npc의 위치를 초기화시켜야함
         Vector3 this_character_pos = this.transform.position;
        // npc의 위치를 기준으로 wayPoint 0으로 설정
        WayPoint.points[0].transform.position = this_character_pos;
         
        // 생성된 정령값의 위치를 기준으로 0으로 생성하게 해야함
        target = WayPoint.points[0];
    
    }

    private void Update()
    {
        // [1] 방향 구하기 - Vector3 : 목표위치 - 현재위치    
        Vector3 dir = target.position - this.transform.position;

        Debug.Log("바라보고 있는 방향이 :" + dir + " 입니다");
    }




    // 도착시 다음 포인터의 정보를 가져와서 타겟 설정
    private void GetNexxtPoint()
    {
        // 종점 판별해서 도착하면 Enemy 오브젝트 Destroy
        if(wayPointIndex == WayPoint.points.Length)
        {
            // 새로운 길을 만들어내는 함수

        }

        wayPointIndex++;
        
        target = WayPoint.points[wayPointIndex];

    }


}
