using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNPC : MonoBehaviour
{
    [SerializeField]
    List<GameObject> npc = new List<GameObject>();

   public void checknpc()
    {
        GameObject npc_parent = GameObject.FindGameObjectWithTag("NpcP");
        npc = npc_parent.GetComponent<Npc_datamanager>().get_totalNpcValues();
    }
}
