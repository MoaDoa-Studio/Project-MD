using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{

    public int ID; // 캐릭터 고유 ID
    public NpcDatabaseSO npc_database; //DB
    public float move_speed = 300f;
    public GameObject childPrefab;
    private int wayPointIndex = 0;
    private Vector3[] target_wavepoint = new Vector3[7];
    private int selectednpcindex = -1;
    public enum Npcstates
    {
        wandering,   // 배회
        factoryWork, // 자원 원소 공장 일
        rest,        // 자원 공장 혹은 재화 건물 일하고 나서 대기 상태
        eat,         // 음식점에서 밥먹기
        play         // 재화(여가) 건물에서 놀기

    }
    
    // Npc 상태 초기화
    private Npcstates npcstates = Npcstates.wandering;

    public void UpdateState()
    {
        switch(npcstates)
        {
            case Npcstates.wandering:
                wander();
                break;
            case Npcstates.factoryWork: 
                
                break;
                
            case Npcstates.rest:
            
                break;
            
            case Npcstates.eat: 
                
                break;
            
            case Npcstates.play: 
                
                break;

        }
    }
    private void Start()
    {
        npc_Build();
        target_wavepoint[0] = transform.position;
        wayPointIndex++;
    }
    private void Update()
    {
        UpdateState();
    }

    private void wander()
    {
        // 다른 선택지가 주어지면 상태 변환
        // 움직임을 멈추는 상태가 되어야함

        // 마지막처리가
        // npcstates = factoryWork?? 같은 상태


        //[1] 목표지점 도착 판정 : 타겟과 자신과의 거리를 구해 도착판정
        float distance = Vector3.Distance(transform.position, target_wavepoint[wayPointIndex]);

        //Debug.Log("목표까지 남은 거리는 : " + distance);
        if (distance < 0.2f)
        {
            StartCoroutine(waitsforMove());
        }

        //[2] 방향 구하기 - Vector3 : 목표위치 - 현재위치
        Vector3 dir = target_wavepoint[wayPointIndex] - transform.position;
        transform.Translate(dir.normalized * move_speed * Time.deltaTime, Space.World);


        // waypoint 를 끝까지 다 도착했으면 새로운 waypoint 지정해주기
        if (wayPointIndex == 6)
        {
            wayPointIndex = 0;
            for (int i = 0; i < target_wavepoint.Length; i++)
            {
                Vector3 randomise = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(0, 0), Random.Range(-3.5f, 3.5f));
                target_wavepoint[i] = randomise;
            }
        }
    }

    // 캐릭터 정보를 xml로 긁어오는 함수
    public void get_Npcvalues()
    {
        // npc 이름에 따라 원소 친화력 계수, // 현재는 selectedcharacterindex 값에 따라 캐릭터 판별

        // 진화에 필요한 경험치 통 Lv2, Lv3
        // npc 자체의 경험치 통
        // npc 배고픔 통
        
    }
    private void factory()
    {
        // building 에서 일을 함에 따라 소요되는 배고픔 수치 감소
        // 일 완료했고 대기
    }

    // 도착시 다음 포인터의 정보를 가져와서 타겟 설정
    private void GetNextPoint()
    {   // 다음 waypoint로 지정
        wayPointIndex++;
    }
    private void npc_Build()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject newChild = Instantiate(childPrefab, this.gameObject.transform); // 부모 오브젝트의 자식 transform 생성

            Vector3 randomPosition = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(0, 0), Random.Range(-3.5f, 3.5f));
            newChild.transform.position = randomPosition;

            target_wavepoint[i] = newChild.transform.position;

            //Debug.Log(i + "번째 위치는 : " + newChild.transform.position);
            //Debug.Log(i + "번째 target_wavepoint는  : " + target_wavepoint[i]);

        }

    }

    IEnumerator waitsforMove()
    {
        GetNextPoint();
        yield return new WaitForSeconds(7f);

    }
}
