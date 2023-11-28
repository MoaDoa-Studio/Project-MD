using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingState : MonoBehaviour
{
    public GameObject state_icon;
    public Sprite spriteimg;
    public Image totalPdBar;
    private int ID;
    [SerializeField]
    private float making_cooltime = 5f;
    [SerializeField]
    private float reset_cooltime = 5f;
    private bool state = false;
    private string product;
    private int productivity;
    private int totalproductivity;
    private int max_productivitydefault; // �ӽ� default ��
    private Building building;
    private GameObject gameManager;
    private ResourceManager resourceManager;

    public BuildingDatabaseSO buildingDatabase; // DB

    public enum BuildingStat
    {
        Idle,
        Run,
        Stopwork
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
        max_productivitydefault = buildingDatabase.buildingsData[ID].max_productivity; // �ִ� ���귮
        
    }

    // Update is called once per frame
    void Update()
    {
        //�����̴��� �׻� ������
        totalPdBar.fillAmount = totalproductivity / max_productivitydefault;

        UpdateState();
    }

    public void UpdateState()
    {
        switch(buildingstat)
        {
            case BuildingStat.Idle:
                state_icon.SetActive(false);
                Running_build();
                break;
            case BuildingStat.Run:
                
                break;
            case BuildingStat.Stopwork:
                stoped_build();
                break;
        }
    }

    void Running_build()
    {
        // �������϶� ��Ÿ���� sprite image
        // state_icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Icons/cycling");
        
        // �� ���귮�� �ʰ������� ������ȯ
        if(totalproductivity >= max_productivitydefault)
        {
            state_icon.SetActive(true);
            buildingstat = BuildingStat.Stopwork; // �׸� ���ϼ���!

        }

        // ���귮 ��Ÿ�� ����
        if(making_cooltime <= 0)
        {
            // ������ �սô�!! 
            int resourceID = getResourceID(product);
           
            totalproductivity += productivity;
            Debug.Log($"��ü :{max_productivitydefault} // ���� ���� {totalproductivity}��ŭ �׿��ֽ��ϴ�");
            // �ִ� ���귮 �ʰ�
            if(totalproductivity >= max_productivitydefault)
            {
                totalproductivity = max_productivitydefault;
                Debug.Log($"��ü �������� {max_productivitydefault}�� �ʰ��Ͽ� {totalproductivity}��ŭ ����Ǿ����ϴ�");
            }
            making_cooltime = reset_cooltime; // ��Ÿ�� �ʱ�ȭ
        }
        making_cooltime -= Time.deltaTime;
    }

    // ���� ���귮 �ʰ��� npc�� ������� ����
    void stoped_build()
    {   
        // ������ �Ϸ�� sprite image ?? ���� �̱���
        
        // �������� ������ �� �ڿ��� ���� => BuilderManager����
        
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
