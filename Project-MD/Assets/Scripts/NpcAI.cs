using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{

    public float move_speed = 300f;
    public GameObject childPrefab;
    private int wayPointIndex = 0;
    private int wayPointInex = 0;
    private Vector3[] target_wavepoint = new Vector3[7];
    public enum Npcstates
    {
        wandering,   // ��ȸ
        factoryWork, // �ڿ� ���� ���� ��
        eat,         // ���������� ��Ա�
        play        // ��ȭ(����) �ǹ����� ���

    }
    
    // Npc ���� �ʱ�ȭ
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
        // �ٸ� �������� �־����� ���� ��ȯ
        // �������� ���ߴ� ���°� �Ǿ����

        // ������ó����
        // npcstates = factoryWork?? ���� ����


        //[1] ��ǥ���� ���� ���� : Ÿ�ٰ� �ڽŰ��� �Ÿ��� ���� ��������
        float distance = Vector3.Distance(transform.position, target_wavepoint[wayPointIndex]);

        Debug.Log("��ǥ���� ���� �Ÿ��� : " + distance);
        if (distance < 0.2f)
        {
            StartCoroutine(waitsforMove());
        }

        //[2] ���� ���ϱ� - Vector3 : ��ǥ��ġ - ������ġ
        Vector3 dir = target_wavepoint[wayPointIndex] - transform.position;
        transform.Translate(dir.normalized * move_speed * Time.deltaTime, Space.World);


        // waypoint �� ������ �� ���������� ���ο� waypoint �������ֱ�
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

    private void factory()
    {
        
    }

    // ������ ���� �������� ������ �����ͼ� Ÿ�� ����
    private void GetNextPoint()
    {   // ���� waypoint�� ����
        wayPointIndex++;
    }
    private void npc_Build()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject newChild = Instantiate(childPrefab, this.gameObject.transform); // �θ� ������Ʈ�� �ڽ� transform ����

            Vector3 randomPosition = new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(0, 0), Random.Range(-3.5f, 3.5f));
            newChild.transform.position = randomPosition;


            target_wavepoint[i] = newChild.transform.position;

            Debug.Log(i + "��° ��ġ�� : " + newChild.transform.position);
            Debug.Log(i + "��° target_wavepoint��  : " + target_wavepoint[i]);


        }

    }

    IEnumerator waitsforMove()
    {
        GetNextPoint();
        yield return new WaitForSeconds(7f);

    }
}
