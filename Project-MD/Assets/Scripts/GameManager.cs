using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BuilderManager builderManager;    
    public InputManager inputManager;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    
}
