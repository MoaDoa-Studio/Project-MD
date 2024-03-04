using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    int randomIndex;

    string slimeName;
    // ������ ���� ���ϴ� �Լ�.
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
            
        // ���� id���� ����
        return randomIndex;
            
    }
       
    public void spawnNpc()
    {
        randomIndex = GetRandomValue();
        Debug.Log(randomIndex + "��ȣ�̴�");
        Debug.Log("Prefabs/Npc/Lv1/" + slimeName);

        GameObject instantiatedPrefab = LoadPrefab("Assets/Prefabs/Npc/Lv1/", slimeName);

        if (instantiatedPrefab != null)
        {
            GameObject npcInstance = Instantiate(instantiatedPrefab, this.transform);
            //Debug.Log(npcInstance + " ���� ������ ������Ʈ �̸��̴�");
            //Debug.Log("������" + randomIndex + "��ȣ�̴�");
            npcInstance.GetComponent<NpcStat>().Get_Infovalue(randomIndex,slimeName);

            // GameDataManager ��ųʸ��� �Ҵ� �� �ؽ� Ű �޾ƿ�.
            GameDataManager.instance.AddNpcData(npcInstance.GetComponent<NpcStat>());
        }
        //GameObject instantiatedPrefab = Resources.Load<GameObject>("Prefabs/Npc/Lv1/" + slimeName);
        else
        {
            Debug.LogError("�������� ã�� �� �����ϴ�: " + slimeName);
        }
    }

    // �������� ������ �ε�
    GameObject LoadPrefab(string folderPath, string prefabName)
    {
        // ���� ���� �ִ� ��� �������� �˻�
        string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab", new string[] { folderPath });

        // �˻��� ������ �߿��� ���ϴ� �̸��� ������ ã��
        foreach (var path in prefabPaths)
        {
            string prefabAssetPath = AssetDatabase.GUIDToAssetPath(path);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabAssetPath);

            if (prefab != null && prefab.name == prefabName)
            {
                return prefab;
            }
        }

        return null; // ���ϴ� �̸��� �������� ã�� ���� ��� null ��ȯ
    }
}
