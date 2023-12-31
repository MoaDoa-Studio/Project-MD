using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcAI : MonoBehaviour
{
    
    public int ID; // 캐릭터 고유 ID.
    public float move_speed = 300f;
    public GameObject childPrefab;
    public bool factoryWork = false; // 작업 상태.
    private int wayPointIndex = 0;
    private Npc_Info_UI npcInfo;
    private Vector3[] target_wavepoint = new Vector3[7];
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
    private void Start()
    {
        npcInfo = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Npc_Info_UI>();
        npc_Build();
        target_wavepoint[0] = transform.position;
        wayPointIndex++;
    }

    public void UpdateState()
    {
        switch(npcstates)
        {
            case Npcstates.wandering:
                walking();
                break;
            case Npcstates.factoryWork:
                factory_Mode();
                break;
                
            case Npcstates.rest:
            
                break;
            
            case Npcstates.eat: 
                
                break;
            
            case Npcstates.play: 
                
                break;

        }
    }
    private void Update()
    {
        UpdateState();
    }


    // 공장에서 일하는 형태로 전환.
     public void factory_Mode()
    {
        // building 에서 일을 함에 따라 소요되는 배고픔 수치 감소.
        // npc state에서 배고픔 감소, 경험치 증가, 정령 상태 초기화
        
        // 일 완료했고 대기.
    }

    // 걷는 상태.
    private void walking()
    {
        // 다른 선택지가 주어지면 상태 변환
       
        // 버튼이 눌리면 잠시 멈추는 상태가 되어야함
        if(factoryWork == true)
        {
            npcstates = Npcstates.factoryWork;
        }
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
    // 도착시 다음 포인터의 정보를 가져와서 타겟 설정.
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

    // 목적지 도착 후 잠시 쉬기.
     IEnumerator waitsforMove()
    {
        GetNextPoint();
        yield return new WaitForSeconds(7f);

    }
}
