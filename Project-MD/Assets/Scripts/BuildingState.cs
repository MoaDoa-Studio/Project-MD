using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingState : MonoBehaviour
{
    [SerializeField]
    private float making_cooltime = 5f;
    [SerializeField]
    private float reset_cooltime = 5f;
    [SerializeField]
    public GameObject chosen_Npc = null;   // ���忡�� ���ϴ� npc
    public BuildingDatabaseSO buildingDatabase; // ���� DB
    public bool built = false; // �ǹ� ���� �Ǽ���
    private int ID;
    private int productivity;
    private int bonus_productivity; // npc �߰� ȿ��
    public int totalproductivity;
    private int max_productivitydefault; // �ӽ� �ִ� ���귮.
    private string product;
    private Building building;
    private Building_Info_UI buildInfo;
    private GameObject Building_Info; // �ǹ� ����â UI Info.
    private GameObject gameManager;
    private ResourceManager resourceManager;
    private BuilderManager builderManager;
    private Npc_Select_UI npcSelect;
    public delegate void OnNpcChangedHandler(GameObject newNpc, GameObject oldNpc);
    public event OnNpcChangedHandler onNpcChanged; // delegate ����
    // Builder ���� UI��.
    private Image totalPdBar;
    public GameObject state_icon; // ���� ����, ����Ϸ� ������.
    public GameObject select_UI;
    
    public enum BuildingStat
    {
        RunwithNpc,
        Run,
        Stopwork
    }

    private BuildingStat buildingstat = BuildingStat.Run; // �⺻���·� �ʱ�ȭ

    void Start()
    {
        building = this.GetComponent<Building>();
        npcSelect = this.GetComponent<Npc_Select_UI>();
        buildInfo = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Building_Info_UI>();
        resourceManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ResourceManager>();
        builderManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BuilderManager>();   
        ID = building.ID; // �������� ID�� DB�� �������� ���� 
        product = buildingDatabase.buildingsData[ID].product; // �����ϴ� �����ڿ�
        productivity = buildingDatabase.buildingsData[ID].productivity; // �ǹ� ���� �ӵ�
        max_productivitydefault = buildingDatabase.buildingsData[ID].max_productivity; // �ִ� ���귮

    }

    void Update()
    {
       // Swicth�� ���.

        UpdateState();
    }

    public void UpdateState()
    {
        switch(buildingstat)
        {
            case BuildingStat.RunwithNpc:
                Debug.Log("Run && Npc ����");
                // �� ���귮 �ʰ�
                finishBuild();
                run_Build();

                break;
            case BuildingStat.Run:
                Debug.Log("Run ���� �����");
                Debug.Log("���� �������� ���ϰ� �ִ� ������ �̸��� : " + chosen_Npc);           
                finishBuild(); // ���� ����
                if(chosen_Npc != null)
                   buildingstat = BuildingStat.RunwithNpc;  
               else
                   run_Build();
 
                break;
            case BuildingStat.Stopwork:
                Debug.Log("StopWork�� �����");
                
                break;
        }
    }

    void run_Build()
    {
        //�����̴� �׻�.
        //totalPdBar.fillAmount = (float)totalproductivity / (float)max_productivitydefault;
        
        // ���귮 ��Ÿ�� ����.
        if(making_cooltime <= 0)
        {
            // ������ �սô�!! 
            int resourceID = getResourceID(product);
           
            totalproductivity += productivity;
            Debug.Log($"��ü :{max_productivitydefault} // ���� ���� {totalproductivity}��ŭ �׿��ֽ��ϴ�");
            // �ִ� ���귮 �ʰ�.
            if(totalproductivity >= max_productivitydefault)
            {
                totalproductivity = max_productivitydefault;
                Debug.Log($"��ü �������� {max_productivitydefault}�� �ʰ��Ͽ� {totalproductivity}��ŭ ����Ǿ����ϴ�");

            }
            making_cooltime = reset_cooltime; // ��Ÿ�� �ʱ�ȭ
        }
        making_cooltime -= Time.deltaTime;
    }

    // ���� ���귮 �ʰ��� npc �����.
    void finishBuild()
    {
        if (totalproductivity >= max_productivitydefault)
        {
            buildingstat = BuildingStat.Stopwork; // �׸� ���ϼ���!

        }

    }  // select UI���� ȣ��.
    public void get_buildNpcInfo(GameObject _npcObject)
    {
        GameObject oldNpc = chosen_Npc; // ���� NPC ����
        chosen_Npc = _npcObject;    // ���ο� NPC�� ������Ʈ
        Debug.Log("Chosen_npc�� : " +  chosen_Npc);

        // �̺�Ʈ ȣ�� (Npc �����)

        if (onNpcChanged != null)
        {
            onNpcChanged.Invoke(chosen_Npc, oldNpc);
            
        }
        else
            onNpcChanged = OnNpcUpdated;
    }

    private void OnNpcUpdated(GameObject newNpc, GameObject oldNpc)
    {

    }

    private int getResourceID(string str)
    {
        // ���ҽ��� ���Ƿ� ������ ID ���� �Լ�.
        switch (str)
        {
            case "Fire":
                return 0;
            case "Water":
                return 1;
            case "Spark":
                return 2;
            case "Ground":
                return 3;
        }
        return -1;
    }

    // Build_Info UI ����
    private void OnMouseDown()
    {
        Debug.Log("build UI�� ���Ƚ��ϴ�!");
        buildInfo.get_Values(buildingDatabase.buildingsData[ID].name, buildingDatabase.buildingsData[ID].level,
        buildingDatabase.buildingsData[ID].ID, buildingDatabase.buildingsData[ID].type,
        buildingDatabase.buildingsData[ID].product, buildingDatabase.buildingsData[ID].
        productivity, buildingDatabase.buildingsData[ID].max_productivity,
        buildingDatabase.buildingsData[ID].size, buildingDatabase.buildingsData[ID].prefab, this.gameObject);
    }
}
