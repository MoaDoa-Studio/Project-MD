using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Npc_Moving : MonoBehaviour
{

    [HideInInspector]
    public float move_speed = 3f;

   
    // ������ �ڽ� GameObject ������
    public GameObject childPrefab;

    // ������ wayPoint ���� ����
    private int wayPointIndex = 0;
    // �̵��� ��ǥ ����
    private Transform target;


    // �̵�Ȯ���� wavepoint ���庯��
    private Transform[] target_wavepoint = new Transform[7];

    private void OnEnable()
    {
        // npc�� ��ġ�� �ʱ�ȭ���Ѿ���
         Vector3 this_character_pos = this.transform.position;

        npc_Build();

        
    }

    private void Start()
    {

        // �ʱ�ÿ� 0���� ����
        wayPointIndex = 0;

        //wayPoint =  
    
    }

    

    // ������ ���� �������� ������ �����ͼ� Ÿ�� ����
    private void GetNextPoint()
    {
        // ���� �Ǻ��ؼ� �����ϸ� Enemy ������Ʈ Destroy
       // if(wayPointIndex == WayPoint.points.Length)
        {
            // ���ο� ���� ������ �Լ�

        }

        wayPointIndex++;
        
        //target = WayPoint.points[wayPointIndex];

    }

    public void npc_Build()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject newChild = Instantiate(childPrefab, this.gameObject.transform); // �θ� ������Ʈ�� �ڽ� transform ����
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(0, 0), Random.Range(-5f, 5f));
            newChild.transform.position = randomPosition;
            Debug.Log(i + "��° ��ġ�� : " + newChild.transform.position);

        }
    }

}
