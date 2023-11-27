using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingDatabaseSO : ScriptableObject
{
    public List<BuildingData> buildingsData;
}

[Serializable]
public class BuildingData
{
    [field: SerializeField]
    public string name { get; private set; } // �̸�
    [field: SerializeField]
    public int ID { get; private set; } // �ǹ� ID
    [field: SerializeField]
    public int type { get; private set; } // �ǹ� Ÿ��.
    [field: SerializeField]
    public string product { get; private set; } // �ǹ� ����ǰ.
    [field: SerializeField]
    public string productivity { get; private set; } // �ǹ� ���� �ӵ�.
    [field: SerializeField]
    public Vector3Int size { get; private set; } = Vector3Int.one;
    [field: SerializeField]
    public GameObject prefab { get; private set; }
}
