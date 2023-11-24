using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Moving : MonoBehaviour
{

    [HideInInspector]
    public float move_speed = 3f;

    public float  startSpeed = 7f;

    // ������ wayPoint ���� ����
    private int wayPointIndex = 0;
    // �̵��� ��ǥ ����
    private Transform target;

    private void Start()
    {
        // �ʱ�ÿ� 0���� ����
        wayPointIndex = 0;

        // npc�� ��ġ�� �ʱ�ȭ���Ѿ���
         Vector3 this_character_pos = this.transform.position;
        // npc�� ��ġ�� �������� wayPoint 0���� ����
        WayPoint.points[0].transform.position = this_character_pos;
         
        // ������ ���ɰ��� ��ġ�� �������� 0���� �����ϰ� �ؾ���
        target = WayPoint.points[0];
    
    }

    private void Update()
    {
        // [1] ���� ���ϱ� - Vector3 : ��ǥ��ġ - ������ġ    
        Vector3 dir = target.position - this.transform.position;

        Debug.Log("�ٶ󺸰� �ִ� ������ :" + dir + " �Դϴ�");
    }




    // ������ ���� �������� ������ �����ͼ� Ÿ�� ����
    private void GetNexxtPoint()
    {
        // ���� �Ǻ��ؼ� �����ϸ� Enemy ������Ʈ Destroy
        if(wayPointIndex == WayPoint.points.Length)
        {
            // ���ο� ���� ������ �Լ�

        }

        wayPointIndex++;
        
        target = WayPoint.points[wayPointIndex];

    }


}
