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
    public string type { get; private set; } // �ǹ� Ÿ��.
    [field: SerializeField]
    public int product { get; private set; } // �ǹ� ����.
    [field: SerializeField]
    public int production_Speed { get; private set; } // �ǹ� ���� �ӵ�.
    [field: SerializeField]
    public int max_productivity { get; private set; } // �ǹ� �ִ� ���귮.
    [field: SerializeField]
    public Vector3Int size { get; private set; } = Vector3Int.one; // �ǹ� ������.    
    [field: SerializeField]
    public GameObject prefab { get; private set; }
    [field: SerializeField]
    public Sprite sprite { get; private set; }
}
