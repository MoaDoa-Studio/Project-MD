using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BuilderManager builderManager;    
    public InputManager inputManager;
    public ResourceManager resourceManager;
    public Building_Info_UI buildingInfo;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    
}
