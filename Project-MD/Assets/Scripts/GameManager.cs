using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BuilderManager builderManager;    
    public InputManager inputManager;
    public ResourceManager resourceManager;
    public Npc_datamanager npc_Datamanager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    // ���� �ǹ��� Ű ��� ��. For Debug.
    public Building building1;
    public void Start()
    {
        GameDataManager.instance.AddBuildingData(building1);
    }
}
