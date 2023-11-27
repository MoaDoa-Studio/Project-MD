using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;

public class Npc_Moving : MonoBehaviour
{

    
    public float move_speed = 300f;


    // 생성할 자식 GameObject 프리팹
    
    public GameObject childPrefab;

    // 도착한 wayPoint 저장 변수
    private int wayPointIndex = 0;

    // 이동할 목표 지점
    private Transform target;

    // 이동확정될 wavepoint 저장변수
    
    private Vector3[] target_wavepoint = new Vector3[7];

    private void Awake()
    {
        npc_Build();

    }
    private void Start()
    {
        target_wavepoint[0] = transform.position;
        Debug.Log("내 위치 초기화");
        wayPointIndex++;
    }

    private void Update()
    {
     
        //[2] 목표지점 도착 판정 : 타겟과 자신과의 거리를 구해 도착판정
           float distance = Vector3.Distance(transform.position, target_wavepoint[wayPointIndex]);

            Debug.Log("목표까지 남은 거리는 : " + distance);
            if(distance < 0.2f)
            {
                StartCoroutine(waitsforMove());
            }

            //[1] 방향 구하기 - Vector3 : 목표위치 - 현재위치
           Vector3 dir = target_wavepoint[wayPointIndex] - transform.position;

            Debug.Log(dir + " 방향으로 이동중입니다");
            
            transform.Translate(dir.normalized * move_speed * Time.deltaTime, Space.World);
            

            // waypoint 를 끝까지 다 도착했으면 새로운 waypoint 지정해주기
            if(wayPointIndex == 6)
            {

                wayPointIndex = 0;
            
                for(int i = 0; i < target_wavepoint.Length; i++)
                {
                    Vector3 randomise = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(0, 0), Random.Range(-3.5f, 3.5f));

                    target_wavepoint[i] = randomise;
                }
            }
        }
    

    // 도착시 다음 포인터의 정보를 가져와서 타겟 설정
    private void GetNextPoint()
    {
        // 종점 판별해서 도착하면 Enemy 오브젝트 Destroy
       // if(wayPointIndex == WayPoint.points.Length)
        {
            // 새로운 길을 만들어내는 함수

        }
        Debug.Log("부모 오브젝트와 가까이 붙어있습니다");
        wayPointIndex++;
       }

    public void npc_Build()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject newChild = Instantiate(childPrefab, this.gameObject.transform); // 부모 오브젝트의 자식 transform 생성
            
            Vector3 randomPosition = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(0, 0), Random.Range(-3.5f, 3.5f));
            newChild.transform.position = randomPosition;
            

            target_wavepoint[i] = newChild.transform.position;
            
            Debug.Log(i + "번째 위치는 : " + newChild.transform.position);
            Debug.Log(i + "번째 target_wavepoint는  : " + target_wavepoint[i]);

       
        }

    }

    IEnumerator waitsforMove()
    {
        GetNextPoint();
        yield return new WaitForSeconds(7f);
        
    }
}
