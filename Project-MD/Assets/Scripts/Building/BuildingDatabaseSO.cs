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
    public string type { get; private set; } // 건물 타입.
    [field: SerializeField]
    public int product { get; private set; } // 건물 생산.
    [field: SerializeField]
    public int production_Speed { get; private set; } // 건물 생산 속도.
    [field: SerializeField]
    public int max_productivity { get; private set; } // 건물 최대 생산량.
    [field: SerializeField]
    public Vector3Int size { get; private set; } = Vector3Int.one; // 건물 사이즈.    
    [field: SerializeField]
    public GameObject prefab { get; private set; }
    [field: SerializeField]
    public Sprite sprite { get; private set; }
}
