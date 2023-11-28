using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] First_Resource;
    [SerializeField]
    private TextMeshProUGUI[] Secondary_Resource;
    [SerializeField]
    private GameObject Secondary_Resource_UI;
    [SerializeField]
    public int[] first_Source = new int[4]; // 1�� ���� ���� ���� �迭
  
    private void Awake()
    {
        // 1�� �������� �ڿ��� 0���� �ʱ�ȭ
       for(int i = 0; i < first_Source.Length; i++)
       {
           first_Source[i] = 0; // 0���� �ʱ�ȭ
       }

    }

    private void Update()
    {
        for(int i = 0; i < first_Source.Length; i++)
        {
            First_Resource[i].text = first_Source[i].ToString();
        }
    }
    public void click_ResourceBar()
    {
        // 2�� ���� â�� ���� ������ ����, ���� ������ �Ѱ�.
        Secondary_Resource_UI.SetActive(!Secondary_Resource_UI.activeSelf);
    }

    public void set_First_ResourceValue(string str, int num)
    {
        // ���� ���� �����ϴ� �Լ�.
        // ���ҽ� ���̵� ���ؿͼ�.
        int resourceID = getResourceID(str);
        if (resourceID == -1)
            return;

        // �ʱ�ȭ.
        First_Resource[resourceID].text = num.ToString();
        resourceID = -1;
    }

    public void add_First_ResourceValue(string str, int num)
    {
        // ���� ���� num ���� �����ִ� �Լ�.
        // ���ҽ� ���̵� ���ؿͼ�.
        int resourceID = getResourceID(str);
        if (resourceID == -1)
            return;

        Debug.Log($"resourceID�� {resourceID} �Դϴ�");
        // �ʱ�ȭ.        
        //int origin = int.Parse(First_Resource[resourceID].text);
        First_Resource[resourceID].text = (num).ToString();

        resourceID = -1;
    }
    public void set_Secondary_ResourceValue(string str, int num)
    {
        // 2�� ���� �� ���� �Լ�.
        // ���� ���� x.
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

    // �ӽ�
    public void initialize_Value()
    {
        set_First_ResourceValue("Fire", 0);
        set_First_ResourceValue("Water", 0);
        set_First_ResourceValue("Spark", 0);
        set_First_ResourceValue("Ground", 0);
    }

    public void add_Value(int num)
    {
        add_First_ResourceValue("Fire", num);
        add_First_ResourceValue("Water", num);
        add_First_ResourceValue("Spark", num);
        add_First_ResourceValue("Ground", num);
    }
}
