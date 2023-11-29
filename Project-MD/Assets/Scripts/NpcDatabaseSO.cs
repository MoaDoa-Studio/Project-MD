using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NpcDatabaseSO : ScriptableObject
{
    public List<NpcData> npcData;
}

[Serializable]
public class NpcData
{
    [field: SerializeField]
    public string name { get; private set;} //�̸�
    [field: SerializeField]
    public int ID { get; private set;} // ���� ��ü ��ȣ
    [field: SerializeField]
    public float hungry { get; private set;} //������
    [field: SerializeField]
    public float exp { get; private set;} //����ġ
   [field: SerializeField]
    public string state { get; private set;} //���� (������, ���ϴ���..)
    [field: SerializeField]
    public int level { get; private set;} //���� ����
    [field: SerializeField]
    public float Affinity { get; private set;} // Ư�� �� ģȭ��
    [field: SerializeField]
    public float Affinity_w { get; private set;} // Ư�� �� ģȭ��
    [field: SerializeField]
    public float Affinity_s { get; private set;} // Ư�� ���� ģȭ��
    [field: SerializeField]
    public float Affinity_g { get; private set;} // Ư�� �� ģȭ��
    [field: SerializeField]
    public GameObject prefab { get; private set;} // Ư�� �� ģȭ��

    


}
