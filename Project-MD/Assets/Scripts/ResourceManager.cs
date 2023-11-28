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
    public int[] first_Source = new int[4]; // 1차 가공 원소 저장 배열
  
    private void Awake()
    {
        // 1차 가공원소 자원을 0개로 초기화
       for(int i = 0; i < first_Source.Length; i++)
       {
           first_Source[i] = 0; // 0으로 초기화
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
        // 2차 원소 창이 켜져 있으면 끄고, 꺼져 있으면 켜고.
        Secondary_Resource_UI.SetActive(!Secondary_Resource_UI.activeSelf);
    }

    public void set_First_ResourceValue(string str, int num)
    {
        // 원소 값을 설정하는 함수.
        // 리소스 아이디 구해와서.
        int resourceID = getResourceID(str);
        if (resourceID == -1)
            return;

        // 초기화.
        First_Resource[resourceID].text = num.ToString();
        resourceID = -1;
    }

    public void add_First_ResourceValue(string str, int num)
    {
        // 원소 값에 num 값을 더해주는 함수.
        // 리소스 아이디 구해와서.
        int resourceID = getResourceID(str);
        if (resourceID == -1)
            return;

        Debug.Log($"resourceID는 {resourceID} 입니다");
        // 초기화.        
        //int origin = int.Parse(First_Resource[resourceID].text);
        First_Resource[resourceID].text = (num).ToString();

        resourceID = -1;
    }
    public void set_Secondary_ResourceValue(string str, int num)
    {
        // 2차 원소 값 세팅 함수.
        // 아직 구현 x.
    }
    
    private int getResourceID(string str)
    {
        // 리소스에 임의로 배정된 ID 리턴 함수.
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

    // 임시
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
