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
    public string name { get; private set; } // 이름
    [field: SerializeField]
    public int ID { get; private set; } // 건물 ID
    [field: SerializeField]
    public int type { get; private set; } // 건물 타입.
    [field: SerializeField]
    public string product { get; private set; } // 건물 생산품.
    [field: SerializeField]
    public string productivity { get; private set; } // 건물 생산 속도.
    [field: SerializeField]
    public Vector3Int size { get; private set; } = Vector3Int.one;
    [field: SerializeField]
    public GameObject prefab { get; private set; }
}
