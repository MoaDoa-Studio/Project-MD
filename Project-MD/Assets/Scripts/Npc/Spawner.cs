using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    int randomIndex;

    string slimeName;
    // 생성할 정령 구하는 함수.
    private int GetRandomValue()
    {
        int num = this.GetComponent < Npc_dataLoader>().npcDatas.Count;
        randomIndex = UnityEngine.Random.Range(1, num);

        if (randomIndex <= 2)
        {
            slimeName = "Waterslime";
        }
        else if (randomIndex <= 4)
        {
            slimeName = "Flameslime";
        }
        else if(randomIndex <= 6)
        {
            slimeName = "Elecslime";
        }
        else { 
            slimeName = "Gnomeslime";
        }
            
        // 실제 id값을 구함
        return randomIndex;
            
    }
       
    private void spawnNpc()
    {
        GameObject instantiatedPrefab = Resources.Load<GameObject>("Prefabs/Npc/Lv1/" + slimeName);
        instantiatedPrefab.GetComponent<NpcStat>().Get_Infovalue(randomIndex);
    }

}
