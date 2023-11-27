using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public GameObject spawn_NPC;
    // Start is called before the first frame update
    void Start()
    {
        GameObject newNPC = Instantiate(spawn_NPC, transform);
        newNPC.transform.SetParent(null);
    }

}
