using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NpcDatabaseSO : MonoBehaviour
{

    public List<NpcData> npcData;

}

[SerializeField]
public class NpcData
{
    [field: SerializeField]
    public string name { get; private set;} //�̸�
    [field: SerializeField]
    public string hungry { get; private set;} //������
    [field: SerializeField]
    public string exp { get; private set;} //����ġ
   [field: SerializeField]
    public string state { get; private set;} //���� (������, ���ϴ���..)
    [field: SerializeField]
    public string level { get; private set;} //���� ����
    [field: SerializeField]
    public string Affinity_f { get; private set;} // Ư�� �� ģȭ��
    public string Affinity_w { get; private set;} // Ư�� �� ģȭ��
    [field: SerializeField]
    public string Affinity_s { get; private set;} // Ư�� ���� ģȭ��
    [field: SerializeField]
    public string Affinity_g { get; private set;} // Ư�� �� ģȭ��
    [field: SerializeField]
    public GameObject prefab { get; private set;} // Ư�� �� ģȭ��

    


}
