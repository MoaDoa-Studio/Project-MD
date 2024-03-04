using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    int randomIndex;

    string slimeName;
    // 생성할 정령 구하는 함수.
    private int GetRandomValue()
    {
        int num = this.GetComponent<Npc_dataLoader>().npcDatas.Count;
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
       
    public void spawnNpc()
    {
        randomIndex = GetRandomValue();
        Debug.Log(randomIndex + "번호이다");
        Debug.Log("Prefabs/Npc/Lv1/" + slimeName);

        GameObject instantiatedPrefab = LoadPrefab("Assets/Prefabs/Npc/Lv1/", slimeName);

        if (instantiatedPrefab != null)
        {
            GameObject npcInstance = Instantiate(instantiatedPrefab, this.transform);
            //Debug.Log(npcInstance + " 새로 생성된 오브젝트 이름이다");
            //Debug.Log("생성된" + randomIndex + "번호이다");
            npcInstance.GetComponent<NpcStat>().Get_Infovalue(randomIndex,slimeName);

            // GameDataManager 딕셔너리에 할당 및 해시 키 받아옴.
            GameDataManager.instance.AddNpcData(npcInstance.GetComponent<NpcStat>());
        }
        //GameObject instantiatedPrefab = Resources.Load<GameObject>("Prefabs/Npc/Lv1/" + slimeName);
        else
        {
            Debug.LogError("프리팹을 찾을 수 없습니다: " + slimeName);
        }
    }

    // 폴더에서 프리팹 로드
    GameObject LoadPrefab(string folderPath, string prefabName)
    {
        // 폴더 내에 있는 모든 프리팹을 검색
        string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab", new string[] { folderPath });

        // 검색된 프리팹 중에서 원하는 이름의 프리팹 찾기
        foreach (var path in prefabPaths)
        {
            string prefabAssetPath = AssetDatabase.GUIDToAssetPath(path);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabAssetPath);

            if (prefab != null && prefab.name == prefabName)
            {
                return prefab;
            }
        }

        return null; // 원하는 이름의 프리팹을 찾지 못한 경우 null 반환
    }
}
