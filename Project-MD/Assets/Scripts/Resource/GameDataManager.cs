using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    static public GameDataManager instance = null;

    public Dictionary<int, Building> current_BuildingData = new Dictionary<int, Building>();
    public Dictionary<int, NpcStat> current_NpcData = new Dictionary<int, NpcStat>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    #region ºôµù
    private int CreateRandomBuildingID()
    {
        int result;
        do
        {
            result = Random.Range(0, 1000000);
        }while (current_BuildingData.ContainsKey(result));
        return result;
    }
    public void AddBuildingData(Building building)
    {
        int Id = CreateRandomBuildingID();
        current_BuildingData.Add(Id, building);

        building.GetComponent<Building>().SetPrimaryID(Id);
    }
    public void DeleteBuildingData(int Id)
    {
        current_BuildingData.Remove(Id);
    }
    public Building GetBuildingData(int Id)
    {
        if (!current_BuildingData.ContainsKey(Id))
            return null;
        return current_BuildingData[Id];
    }
    #endregion

    #region NPC
    private int CreateRandomNpcID()
    {
        int result;
        do
        {
            result = Random.Range(0, 1000000);
        } while (current_NpcData.ContainsKey(result));
        return result;
    }
    public void AddNpcData(NpcStat npcStat)
    {
        int Id = CreateRandomNpcID();
        current_NpcData.Add(Id, npcStat);
        
        npcStat.GetComponent<NpcStat>().SetPrimaryID(Id);
    }
    public void DeleteNpcData(int Id)
    {
        current_NpcData.Remove(Id);
    }
    public NpcStat GetNpcData(int Id)
    {
        if (!current_NpcData.ContainsKey(Id))
            return null;
        return current_NpcData[Id];
    }
    #endregion
}
