using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingState : MonoBehaviour
{
    private int ID;
    [SerializeField]
    private float making_cooltime = 5f;
    [SerializeField]
    private float reset_cooltime = 5f;
    private bool state = false;
    private string product;
    private int productivity;
    private float max_productivity;
    private Building building;
    private GameObject gameManager;
    private ResourceManager resourceManager;

    public BuildingDatabaseSO buildingDatabase; // DB

    public enum BuildingStat
    {
        Idle,
        Run,
        Stop
    }

    public BuildingStat buildingstat = BuildingStat.Run; // �⺻���·� �ʱ�ȭ

    // Start is called before the first frame update
    void Start()
    {
        building = this.GetComponent<Building>();
        resourceManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ResourceManager>(); 
        ID = building.ID; // �������� ID�� DB�� �������� ���� 
        product = buildingDatabase.buildingsData[ID].product; // �����ϴ� �����ڿ�
        productivity = buildingDatabase.buildingsData[ID].productivity; // �ǹ� ���� �ӵ�
        max_productivity = buildingDatabase.buildingsData[ID].max_productivity; // �ִ� ���귮
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    public void UpdateState()
    {
        switch(buildingstat)
        {
            case BuildingStat.Idle:
                Running_build();
                break;
            case BuildingStat.Run:
                
                break;
            case BuildingStat.Stop:
             
                break;
        }
    }

    void Running_build()
    {
        if(making_cooltime <= 0)
        {
            // ������ �սô�!! //value ���� ������ product, productivity �ְ� 
            resourceManager.add_First_ResourceValue(product, productivity);
            Debug.Log($"���귮�� {productivity}, �̸� �����ϴ� ������ {product}�Դϴ�");
            making_cooltime = reset_cooltime; // ��Ÿ�� �ʱ�ȭ
        }
        making_cooltime -= Time.deltaTime;
    }
}
