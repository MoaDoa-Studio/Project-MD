using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;

public class Npc_Moving : MonoBehaviour
{

    
    public float move_speed = 300f;


    // ������ �ڽ� GameObject ������
    
    public GameObject childPrefab;

    // ������ wayPoint ���� ����
    private int wayPointIndex = 0;

    // �̵��� ��ǥ ����
    private Transform target;

    // �̵�Ȯ���� wavepoint ���庯��
    
    private Vector3[] target_wavepoint = new Vector3[7];

    private void Awake()
    {
        npc_Build();

    }
    private void Start()
    {
        target_wavepoint[0] = transform.position;
        Debug.Log("�� ��ġ �ʱ�ȭ");
        wayPointIndex++;
    }

    private void Update()
    {
     
            //[1] ���� ���ϱ� - Vector3 : ��ǥ��ġ - ������ġ
           Vector3 dir = target_wavepoint[wayPointIndex] - transform.position;

            Debug.Log(dir + " �������� �̵����Դϴ�");
            
            transform.Translate(dir.normalized * move_speed * Time.deltaTime, Space.World);
            
        //[2] ��ǥ���� ���� ���� : Ÿ�ٰ� �ڽŰ��� �Ÿ��� ���� ��������
           float distance = Vector3.Distance(transform.position, target_wavepoint[wayPointIndex]);

            Debug.Log("��ǥ���� ���� �Ÿ��� : " + distance);
            if(distance < 0.2f)
            {
                GetNextPoint();
            }

        }
    

    // ������ ���� �������� ������ �����ͼ� Ÿ�� ����
    private void GetNextPoint()
    {
        // ���� �Ǻ��ؼ� �����ϸ� Enemy ������Ʈ Destroy
       // if(wayPointIndex == WayPoint.points.Length)
        {
            // ���ο� ���� ������ �Լ�

        }
        Debug.Log("�θ� ������Ʈ�� ������ �پ��ֽ��ϴ�");
        wayPointIndex++;
       }

    public void npc_Build()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject newChild = Instantiate(childPrefab, this.gameObject.transform); // �θ� ������Ʈ�� �ڽ� transform ����
            
            Vector3 randomPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(0, 0), Random.Range(-4f, 4f));
            newChild.transform.position = randomPosition;
            

            target_wavepoint[i] = newChild.transform.position;
            
            Debug.Log(i + "��° ��ġ�� : " + newChild.transform.position);
            Debug.Log(i + "��° target_wavepoint��  : " + target_wavepoint[i]);

        }
    }

}
