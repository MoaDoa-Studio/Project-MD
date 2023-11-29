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
    private GameObject Building_Info; // �ǹ� ����â UI Info.
    
    public BuildingDatabaseSO buildingDatabase; // ���� DB
    private int ID;
    private bool state = false;
    private string product;
    private int productivity;
    
    public int totalproductivity;
    private int max_productivitydefault; // �ӽ� �ִ� ���귮.
    private Building building;
    private GameObject gameManager;
    private ResourceManager resourceManager;
    private BuilderManager builderManager;
    
    // Builder ���� UI��.
    private Image totalPdBar;
    public GameObject state_icon;
    
    public enum BuildingStat
    {
        Idle,
        Run,
        Stopwork
    }

    private BuildingStat buildingstat = BuildingStat.Run; // �⺻���·� �ʱ�ȭ

    // Start is called before the first frame update
    void Start()
    {
        building = this.GetComponent<Building>();
        resourceManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ResourceManager>();
        builderManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BuilderManager>();   
        totalPdBar = Building_Info.transform.Find("Fill").GetComponent<Image>();
        ID = building.ID; // �������� ID�� DB�� �������� ���� 
        product = buildingDatabase.buildingsData[ID].product; // �����ϴ� �����ڿ�
        productivity = buildingDatabase.buildingsData[ID].productivity; // �ǹ� ���� �ӵ�
        max_productivitydefault = buildingDatabase.buildingsData[ID].max_productivity; // �ִ� ���귮
    }

    // Update is called once per frame
    void Update()
    {
       // Swicth�� ���.

       
        UpdateState();
    }

    public void UpdateState()
    {
        switch(buildingstat)
        {
            case BuildingStat.Idle:
                Debug.Log("Idle ���°� ����Ǳ�� ��");
                break;
            case BuildingStat.Run:
                Debug.Log("Run ���� �����");
                state_icon.SetActive(false);
                Running_build();
                
                break;
            case BuildingStat.Stopwork:
                Debug.Log("StopWork�� �����");
                stoped_build();
                break;
        }
    }

    void Running_build()
    {
        //�����̴� �׻�.
        //totalPdBar.fillAmount = (float)totalproductivity / (float)max_productivitydefault;
        // �� ���귮�� �ʰ������� ������ȯ.
        if (totalproductivity >= max_productivitydefault)
        {
            totalproductivity = 0;
            
            buildingstat = BuildingStat.Stopwork; // �׸� ���ϼ���!

        }

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

                resourceManager.first_Source[resourceID] += max_productivitydefault;

            }
            making_cooltime = reset_cooltime; // ��Ÿ�� �ʱ�ȭ
        }
        making_cooltime -= Time.deltaTime;
    }

    // ���� ���귮 �ʰ��� npc�� ������� ����.
    void stoped_build()
    {   
        // npc ��ü�� ������� ����

        // npc�� �߰��ϰų� 
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
}
