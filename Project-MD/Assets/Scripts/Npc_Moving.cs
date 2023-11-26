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
    
    // �̵�Ȯ���� wavepoint ���庯��
    private Vector3[] target_wavepoint = new Vector3[7];

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
            
            if (i == 0)
                newChild.transform.position = this.transform.position;
            else
            {
                Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(0, 0), Random.Range(-5f, 5f));
                newChild.transform.position = randomPosition;
            }

            target_wavepoint[i] = newChild.transform.position;

            Debug.Log(i + "��° ��ġ�� : " + newChild.transform.position);

        }
    }

}
