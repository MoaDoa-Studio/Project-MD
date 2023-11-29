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
    public string name { get; private set;} //이름
    [field: SerializeField]
    public int ID { get; private set;} // 고유 객체 번호
    [field: SerializeField]
    public float hungry { get; private set;} //만복도
    [field: SerializeField]
    public float exp { get; private set;} //경험치
   [field: SerializeField]
    public string state { get; private set;} //상태 (쉬는중, 일하는중..)
    [field: SerializeField]
    public int level { get; private set;} //정령 레벨
    [field: SerializeField]
    public float Affinity { get; private set;} // 특성 불 친화력
    [field: SerializeField]
    public float Affinity_w { get; private set;} // 특성 물 친화력
    [field: SerializeField]
    public float Affinity_s { get; private set;} // 특성 전기 친화력
    [field: SerializeField]
    public float Affinity_g { get; private set;} // 특성 땅 친화력
    [field: SerializeField]
    public GameObject prefab { get; private set;} // 특성 땅 친화력

    


}
